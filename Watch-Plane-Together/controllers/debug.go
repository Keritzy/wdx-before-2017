package controllers

import (
  "github.com/astaxie/beego"
  _ "github.com/go-sql-driver/mysql"
  "database/sql"
  "strconv"
  "fmt"
  "time"
  "net/url"
  "net/http"
  "io/ioutil"
  "github.com/bitly/go-simplejson"
  "strings"
)

type DebugController struct {
  beego.Controller
}

func (c *DebugController) Get() {
  c.Ctx.WriteString("连接成功")
}

func (c *DebugController) DebugPosition() {
  // 这里不进行删除，而是标识一位 online 位

  uidstr := c.Input().Get("uid")
  uid, err := strconv.Atoi(uidstr)
  checkErr(err, "用户 id 解析错误")

  longitudestr := c.Input().Get("longitude")
  longitude, err := strconv.ParseFloat(longitudestr, 64)
  checkErr(err, "经度 Longitude 解析错误")

  latitudestr := c.Input().Get("latitude")
  latitude, err := strconv.ParseFloat(latitudestr, 64)
  checkErr(err, "经度 latitude 解析错误")

  altitudestr := c.Input().Get("altitude")
  altitude, err := strconv.ParseFloat(altitudestr, 64)
  checkErr(err, "经度 altitude 解析错误")

  fmt.Printf("[DEBUG] uid:%d 经度:%f 纬度:%f 高度:%f\n", uid, longitude, latitude, altitude)

  db, err := sql.Open("mysql", "root:AyQnuWucFNX2EtKK@tcp(121.43.181.146:20097)/usutm")
  checkErr(err, "无法连接到数据库")
  defer db.Close();

  timestamp := time.Now().Unix()

  /*
  在向表中插入数据的时候，经常遇到这样的情况：
  1、首先判断数据是否存在；
  2、如果不存在，则插入；
  3、如果存在，则更新。
  用 Replace Into 就可以完美处理这种情况
  */
  stmt, err := db.Prepare("REPLACE INTO current (uid, latitude, longitude, altitude, timestamp, region, online) VALUES(?, ?, ?, ?, ?, ?, ?)")
  checkErr(err, "插入语句无效")
  defer stmt.Close()

  _, err = stmt.Exec(uid, latitude, longitude, altitude, timestamp, 654321, 1)
  checkErr(err, "插入失败")

  stmt, err = db.Prepare("INSERT INTO history (actionid, location, timestamp) VALUES(?, ?, ?)")
  checkErr(err, "插入语句无效")
  defer stmt.Close()

  _, err = stmt.Exec(uidstr + strconv.FormatInt(timestamp, 10), latitudestr+","+longitudestr+","+altitudestr, timestamp)
  checkErr(err, "插入失败")

  //rows, err := rows.
  c.Ctx.WriteString("位置已上传")

}

func (c *DebugController) DebugWeather() {
  content, err := getWeather("22.540225", "113.947556")
  checkErr(err, "天气查询失败")
  c.Ctx.WriteString(content + "\n天气服务已连接")
}

func (c *DebugController) DebugFlight() {
  c.Ctx.WriteString("航班服务已连接")
}

func (c *DebugController) DebugUpload() {

  fmt.Println(string(c.Ctx.Input.RequestBody))

  c.Ctx.WriteString(string(c.Ctx.Input.RequestBody))
}

func (c *DebugController) DebugNear() {
  uidstr := c.Input().Get("uid")
  uid, err := strconv.Atoi(uidstr)
  checkErr(err, "用户 id 解析错误")

  longitudestr := c.Input().Get("longitude")
  longitude, err := strconv.ParseFloat(longitudestr, 64)
  checkErr(err, "经度 Longitude 解析错误")

  latitudestr := c.Input().Get("latitude")
  latitude, err := strconv.ParseFloat(latitudestr, 64)
  checkErr(err, "经度 latitude 解析错误")

  altitudestr := c.Input().Get("altitude")
  altitude, err := strconv.ParseFloat(altitudestr, 64)
  checkErr(err, "经度 altitude 解析错误")

  fmt.Printf("[DEBUG] uid:%d 经度:%f 纬度:%f 高度:%f\n", uid, longitude, latitude, altitude)

  db, err := sql.Open("mysql", "user:password@tcp(host:port)/db")
  checkErr(err, "无法连接到数据库")
  defer db.Close();

  // 此处 region 需要根据经纬度来进行计算
  rows, err := db.Query("SELECT * FROM current WHERE region=654321 AND online=1")
  checkErr(err, "查询失败")

  nearbyCount := 0
  s := []string{}
  for rows.Next() {
    var uid int
    var lon float64
    var lat float64
    var alt float64
    var times int64
    var reg int
    var online int
    err = rows.Scan(&uid, &lon, &lat, &alt, &times, &reg, &online)
    checkErr(err, "获取数据错误")
    nearbyCount++
    s = append(s, strconv.Itoa(uid))
  }


  c.Ctx.WriteString("附近的人数: " + strconv.Itoa(nearbyCount) + "\n他们是: " + strings.Join(s[:], ",") )
}

/*
  Forecast information about wind. Attributes:
chill: wind chill in degrees (integer)
direction: wind direction, in degrees (integer)
speed: wind speed, in the units specified in the speed attribute of the yweather:units element (mph or kph). (integer)

*/


func getWeather(latitudestr string, longitudestr string) (info string, err error){
  // Parse query to url
  Url, _ := url.Parse("https://query.yahooapis.com/v1/public/yql")
  parameters := url.Values{}
  parameters.Add("q", "select units,wind,atmosphere,astronomy from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"guangzhou\") and u=\"c\"")
  parameters.Add("format", "json")
  Url.RawQuery = parameters.Encode()
  urlParsed := Url.String()
  fmt.Println(urlParsed)

  // make the query
  resp, err := http.Get(urlParsed)
  checkErr(err, "连接错误")
  defer resp.Body.Close()

  body, err := ioutil.ReadAll(resp.Body)
  checkErr(err, "无法读取")

  json, err := simplejson.NewJson(body)
  checkErr(err, "解析 Json 错误")
  fmt.Println(json)

  humiditystr, _ := json.Get("query").Get("results").Get("channel").Get("atmosphere").Get("humidity").String()
  pressurestr, _ := json.Get("query").Get("results").Get("channel").Get("atmosphere").Get("pressure").String()
  risingstr, _ := json.Get("query").Get("results").Get("channel").Get("atmosphere").Get("rising").String()
  visibilitystr, _ := json.Get("query").Get("results").Get("channel").Get("atmosphere").Get("visibility").String()

  chillstr, _ := json.Get("query").Get("results").Get("channel").Get("wind").Get("chill").String()
  directionstr, _ := json.Get("query").Get("results").Get("channel").Get("wind").Get("direction").String()
  speedstr, _ := json.Get("query").Get("results").Get("channel").Get("wind").Get("speed").String()

  sunrisestr, _ := json.Get("query").Get("results").Get("channel").Get("astronomy").Get("sunrise").String()
  sunsetstr, _ := json.Get("query").Get("results").Get("channel").Get("astronomy").Get("sunset").String()

  info = "humidity: " + humiditystr + ", pressure: " + pressurestr + "\n"
  info += "rising:" + risingstr + ", visibility: " + visibilitystr + "\n"
  info += "chill: " + chillstr + ", direction: " + directionstr + ", speed: " + speedstr + "\n"
  info += "sunrise: " + sunrisestr + ", sunset:" + sunsetstr

  return info, err
}

func checkErr(err error, info string){
  if err != nil {
    fmt.Println(err)
    fmt.Println(info)
    panic(err)
  }
}


