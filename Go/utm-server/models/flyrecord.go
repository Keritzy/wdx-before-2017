package models

import (
	"bytes"
	"database/sql"
	"fmt"
	"math/rand"
	"strconv"
	"strings"
	"time"
	"utm-server/utils"

	"github.com/bitly/go-simplejson"
	"gopkg.in/redis.v4"
	// for mysql connection
	_ "github.com/go-sql-driver/mysql"
	"github.com/wdxtub/geohash"
)

/*
-----------------------
Storage Design Pattern
-----------------------

The whole UTM system needs to handle two kinds of drone data: online drones and
their history data.

Online drones data processing should be highly efficient to provide better user
experience. History drones data should be stored for a mount of time for querying
and searching.

According to these requirements, the storage design pattern is as follows:

1. Online drones data will be stored in Redis with the geo feature support to
   provide nearby drones info as well as conflict warning
2. History data will be stored in MySQL. We'll use goroutine to insert the data
   into MySQL asynchronously so as to return the response to the client asap

-----------------------
Redis Schema Design
-----------------------

For Geo Support: When each fly status record arrived, the system will do the
following things(Use Redis command to illustrate):

1. Add drone to geo list: GEOADD locations [latitude] [longitude] [droneid]
2. Update drone info(Use Hash): HMSET d:[droneid] k1 v1 k2 v2 k3 v3 (according
   to the flying status data) and set Expire time (now we use 60 seconds)
3. Get the nearby drones list: GEORADIUS locations [latitude] [longitude] 1000 m
   (now we use 1000 m as the threshold)
4. Check the validation of the drones, if the specific drone with key d:[droneid]
   expired, it should be removed from geo list. Geo list can also be the online
   list.


-----------------------
MySQL Schema Design
-----------------------

For History Data: When each fly status record arrived, the system will transform
each record into one structured line in the database. It should be easy to use
and convinient to integrated with other tools(e.g hadoop, spark)

As the limitation of database capacity, we cannot store all the data in it for
all the time. Records will be arranged by date and will be moved to S3(or other
bucket storage)

There are lots of redundancy in current records so we need to split them to
reduce the amount of space to store all the data. The following SQL Statement
can be used.

One more thing, as inserting into MySQL can be a time-consuming operation(may be
longer than 100ms), the system will use goroutine to finish the job without blocking
the main action.

# frid = flyrecord id = droneid : timestamp
CREATE TABLE `FlyRecords` (
	`frid` CHAR(32) NOT NULL,
	`speedX` INT,
    `speedY` INT,
    `speedZ` INT,
    `altitude` DOUBLE,
    `latitude` DOUBLE,
    `longitude` DOUBLE,
    `yaw` INT,
    `flighttime` INT,
	`dronetype` CHAR(32),
	`userinfo` CHAR(255),
    `orderid` CHAR(64),
    `status` TINYINT,
    `platform` CHAR(32),
    `region` CHAR(20),
	PRIMARY KEY (`frid`)
);

CREATE TABLE `DroneList` (
	`droneid` CHAR(32) NOT NULL,
	`dronetype` CHAR(32),
	`timestamp` INT(10),
	`lastupdate` INT(10),
	PRIMARY KEY (`droneid`)
);


# Old version
----------------------------
CREATE TABLE `OnlineDrone` (
    `droneid` CHAR(32) NOT NULL,
    `speedX` INT,
    `speedY` INT,
    `speedZ` INT,
    `altitude` DOUBLE,
    `latitude` DOUBLE,
    `longitude` DOUBLE,
    `yaw` INT,
    `flighttime` INT,
    `dronetype` CHAR(32),
    `timestamp` INT(10),
    `userinfo` CHAR(255),
    `orderid` CHAR(64),
    `status` TINYINT,
    `platform` CHAR(32),
    `region` CHAR(20),
    PRIMARY KEY (`droneid`)
);

*/

const (
	// Unknown condition for Drone
	Unknown = iota
	// Legal condition for Drone
	Legal
	// Illegal condition for Drone
	Illegal
)

// FlyRecord is a struct to store single fly records
type FlyRecord struct {
	Position   Position
	Speed      Speed
	DroneInfo  DroneInfo
	FlightInfo FlightInfo
}

// AddToRedis is a method to add a fly record to redis database
// for online drone information
func AddToRedis(f *FlyRecord) string {
	client := redis.NewClient(&redis.Options{
		Addr:     "localhost:6379",
		Password: "",
		DB:       0,
	})

	err := client.GeoAdd("locations", &redis.GeoLocation{
		Longitude: f.Position.Longitude,
		Latitude:  f.Position.Latitude,
		Name:      f.DroneInfo.DroneID,
	}).Err()
	if err != nil {
		fmt.Println(err)
		return "Geo Insertion Error." + err.Error()
	}

	// update drone info
	err = client.HMSet(
		f.DroneInfo.DroneID,
		map[string]string{
			"altitude":   strconv.FormatFloat(f.Position.Altitude, 'f', 6, 64),
			"speedx":     strconv.Itoa(f.Speed.X),
			"speedy":     strconv.Itoa(f.Speed.Y),
			"speedz":     strconv.Itoa(f.Speed.Z),
			"flighttime": strconv.Itoa(f.DroneInfo.FlightTime),
			"dronetype":  f.DroneInfo.DroneType,
			"userinfo":   f.FlightInfo.UserInfo,
			"orderid":    f.FlightInfo.OrderID,
			"platform":   f.FlightInfo.Platform,
		},
	).Err()
	if err != nil {
		return "Drone Information Insertion Error"
	}

	err = client.PExpire(f.DroneInfo.DroneID, 1*time.Minute).Err()
	if err != nil {
		return "Set Expire Time Error"
	}

	// find near
	result, err := client.GeoRadius(
		"locations",
		f.Position.Longitude,
		f.Position.Latitude,
		&redis.GeoRadiusQuery{
			Radius:   100,
			Unit:     "km",
			WithDist: true,
			Sort:     "ASC",
		}).Result()
	if err != nil {
		return "Query Error"
	}

	for i, loc := range result {
		if loc.Name == f.DroneInfo.DroneID {
			continue
		}
		r, err := client.HMGet(loc.Name, "altitude", "speedx", "speedy", "speedz", "dronetype", "flighttime").Result()
		if err != nil {
			fmt.Println(err, "No such key.")
		}
		// No Corresponding content, need to be removed from the online list
		if r[0] == nil {
			err = client.ZRem("locations", loc.Name).Err()
			if err != nil {
				return "Error Removing item in Online List"
			}

		} else {
			fmt.Println(i, loc.Name, r)
		}

	}
	set, err := client.ZRange("locations", 0, -1).Result()
	if err != nil {
		return "Error Getting Online List"
	}
	fmt.Println("[Online List] ", set)

	return "Record Uploaded Success"
}

// AddToFlyRecordsMySQL description
// Add FlyRecords As the History to MySQL
func AddToFlyRecordsMySQL(f *FlyRecord) string {
	db, err := sql.Open("mysql", "root:AyQnuWucFNX2EtKK@tcp(121.43.181.146:20097)/usutm")
	utils.CheckErr(err, "Fail to Connect to the Database")
	defer db.Close()

	stmt, err := db.Prepare("INSERT INTO FlyRecords (speedX, speedY, speedZ, altitude, latitude, longitude, yaw, flighttime, dronetype, frid, userinfo, orderid, status, platform, region) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")
	utils.CheckErr(err, "Invalid Statement")

	precision := 6
	hash, box := geohash.Encode(f.Position.Latitude, f.Position.Longitude, precision)
	fmt.Printf("[REGION] %s\n", hash)
	fmt.Printf("[BOX] %f, %f, %f, %f\n", box.MinLat, box.MaxLat, box.MinLng, box.MaxLng)

	_, err = stmt.Exec(
		f.Speed.X,
		f.Speed.Y,
		f.Speed.Z,
		f.Position.Altitude,
		f.Position.Latitude,
		f.Position.Longitude,
		f.Position.Yaw,
		f.DroneInfo.FlightTime,
		f.DroneInfo.DroneType,
		f.DroneInfo.DroneID+":"+strconv.FormatInt(f.FlightInfo.CurrentTime, 10),
		f.FlightInfo.UserInfo,
		f.FlightInfo.OrderID,
		f.FlightInfo.Status,
		f.FlightInfo.Platform,
		hash)
	utils.CheckErr(err, "Statement Execusion Error")
	defer stmt.Close()

	if err != nil {
		return "Insertion Error"
	}

	return "Record Uploaded to History Success"
}

// CreateFlyRecordFromBytes description
// Load Flyrecords from the API data
func CreateFlyRecordFromBytes(reqeustBody []byte) (*FlyRecord, error) {
	json, err := simplejson.NewJson(reqeustBody)
	utils.CheckErr(err, "Json Parse Error")
	if err != nil {
		return nil, err
	}
	jsonobj := json.Get("records").GetIndex(0)
	flyrecord := FlyRecord{
		Speed: Speed{
			X: jsonobj.Get("speedX").MustInt(),
			Y: jsonobj.Get("speedY").MustInt(),
			Z: jsonobj.Get("speedZ").MustInt()},
		Position: Position{
			Altitude:  jsonobj.Get("altitude").MustFloat64(),
			Latitude:  jsonobj.Get("latitude").MustFloat64(),
			Longitude: jsonobj.Get("longitude").MustFloat64(),
			Yaw:       jsonobj.Get("yaw").MustInt()},
		DroneInfo: DroneInfo{
			FlightTime: jsonobj.Get("flightTime").MustInt(),
			DroneType:  jsonobj.Get("droneType").MustString(),
			DroneID:    jsonobj.Get("droneID").MustString()},
		FlightInfo: FlightInfo{
			CurrentTime: jsonobj.Get("currentTime").MustInt64(),
			UserInfo:    jsonobj.Get("userInfo").MustString(),
			OrderID:     jsonobj.Get("orderID").MustString(),
			Status:      jsonobj.Get("status").MustInt(),
			Platform:    jsonobj.Get("platform").MustString()}}

	return &flyrecord, err
}

// GenerateFlyRecordJSONString description
// This function will generate a fly record using droneID arount a specific
// location given by the parameter
func GenerateFlyRecordJSONString(droneID string, latitude float64, longitude float64) string {
	speed := Speed{
		// Assume the fastest speed is 20m/s then
		// For x and y axis, the max speed is sqrt(20^2 / 2) = 14.14
		// For z, the max speed is 5m/s
		X: rand.Intn(14),
		Y: rand.Intn(14),
		Z: rand.Intn(5)}

	position := Position{
		// We assume the Yaw is positive and < 180
		// The biggest altitude is 500 feet aka 150 meters
		Altitude:  rand.Float64() * 100,
		Latitude:  latitude + rand.Float64(),
		Longitude: longitude - rand.Float64(),
		Yaw:       rand.Intn(180)}

	droneinfo := DroneInfo{
		// Random flight time around 900 seconds(15 min)
		// For Drone Type, we assume all are phantom 4
		FlightTime: rand.Intn(900),
		DroneType:  "p4",
		DroneID:    droneID}

	flightinfo := FlightInfo{
		// Order Id is UUID
		// Fro Platform, we assume all are ios
		// Status
		CurrentTime: time.Now().Unix(),
		UserInfo:    "User Info Field",
		OrderID:     "7f5de270-702c-11e6-8f5a-075bc1a783de",
		Status:      0,
		Platform:    "ios"}

	flyrecord := FlyRecord{
		Position:   position,
		Speed:      speed,
		DroneInfo:  droneinfo,
		FlightInfo: flightinfo}

	return flyrecord.JSONString()
}

// FindNearbyDronesMySQL is a method to find nearby drones according to location
// Along with old AddToOnlineListMySQL Method
func FindNearbyDronesMySQL(droneID string, latitude float64, longitude float64) string {
	db, err := sql.Open("mysql", "root:AyQnuWucFNX2EtKK@tcp(121.43.181.146:20097)/usutm")
	utils.CheckErr(err, "Fail to Connect to the Database")
	defer db.Close()

	// Use latitude and longitude to calculate region
	// Currently We only consider the drones in the same region
	// Not the neighboring 8 regions
	precision := 6
	region, box := geohash.Encode(latitude, longitude, precision)
	fmt.Printf("[REGION] %s\n", region)
	fmt.Printf("[BOX] %f, %f, %f, %f\n", box.MinLat, box.MaxLat, box.MinLng, box.MaxLng)

	var buffer bytes.Buffer
	buffer.WriteString("region='" + region + "' ")

	neighbors := geohash.GetNeighbors(latitude, longitude, precision)
	fmt.Print("[NEIGHBOR] ")
	for _, hash := range neighbors {
		fmt.Print(hash, " ")
		buffer.WriteString("OR region='" + hash + "' ")
	}
	fmt.Print("\n")

	sql := "SELECT droneid FROM OnlineDrone WHERE (" + buffer.String() + ") AND status = 0"
	fmt.Println("[SQL] " + sql)

	rows, err := db.Query(sql)
	utils.CheckErr(err, "Query Error")

	nearbyCount := 0
	s := []string{}
	for rows.Next() {
		var droneid string
		err = rows.Scan(&droneid)
		utils.CheckErr(err, "Retriving Data Error")
		nearbyCount++
		s = append(s, droneid)
	}

	return "Nearby Drones Count: " + strconv.Itoa(nearbyCount) + "\nThey are: " + strings.Join(s[:], ",")
}

// AddToOnlineListMySQL is a method to add a fly record to mysql database
func AddToOnlineListMySQL(f *FlyRecord) string {
	db, err := sql.Open("mysql", "root:AyQnuWucFNX2EtKK@tcp(121.43.181.146:20097)/usutm")
	utils.CheckErr(err, "Fail to Connect to the Database")
	defer db.Close()

	stmt, err := db.Prepare("REPLACE INTO OnlineDrone (speedX, speedY, speedZ, altitude, latitude, longitude, yaw, flighttime, dronetype, droneid, timestamp, userinfo, orderid, status, platform, region) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")
	utils.CheckErr(err, "Invalid Statement")

	precision := 6
	hash, box := geohash.Encode(f.Position.Latitude, f.Position.Longitude, precision)
	fmt.Printf("[REGION] %s\n", hash)
	fmt.Printf("[BOX] %f, %f, %f, %f\n", box.MinLat, box.MaxLat, box.MinLng, box.MaxLng)

	_, err = stmt.Exec(
		f.Speed.X,
		f.Speed.Y,
		f.Speed.Z,
		f.Position.Altitude,
		f.Position.Latitude,
		f.Position.Longitude,
		f.Position.Yaw,
		f.DroneInfo.FlightTime,
		f.DroneInfo.DroneType,
		f.DroneInfo.DroneID,
		f.FlightInfo.CurrentTime,
		f.FlightInfo.UserInfo,
		f.FlightInfo.OrderID,
		f.FlightInfo.Status,
		f.FlightInfo.Platform,
		hash)
	utils.CheckErr(err, "Statement Execusion Error")
	defer stmt.Close()

	if err != nil {
		return "Insertion Error"
	}

	return "Record Uploaded Success"
}

func (f *FlyRecord) String() string {
	return fmt.Sprintf("%s\n%s\n%s\n%s",
		f.Position.String(), f.Speed.String(),
		f.DroneInfo.String(), f.FlightInfo.String())
}

// JSONString is a method to generate JSON string
func (f *FlyRecord) JSONString() string {
	return fmt.Sprintf("{\"records\" : [{%s, %s, %s, %s}]}",
		f.Position.JSONStringStrip(),
		f.Speed.JSONStringStrip(),
		f.DroneInfo.JSONStringStrip(),
		f.FlightInfo.JSONStringStrip())
}
