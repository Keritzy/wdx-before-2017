package controllers

import (
	"fmt"
	"strconv"
	"utm-server/models"
	"utm-server/utils"

	"github.com/astaxie/beego"
)

// DebugController : Operations for Debugging Usage
type DebugController struct {
	beego.Controller
}

// Get method description
// @Title Show DebugController Welcome Page
// @Description Several basic information about this backend api
// @Success 200 Debug Controller Welcome Page
// @Failure 404 Template not found
// @router / [get]
func (c *DebugController) Get() {
	c.Data["Email"] = "da.wang@dji.com"
	c.Data["Message"] = "Debug Controller Test"
	c.TplName = "index.tpl"
}

// DebugUploadStatus description
// @Title Upload Current Flying Status
// @Description Receive data from DJI GO or Simulator
// @Param body body string true "example {"records" : [{"latitude": 43.332432, "longitude": 107.231000, "altitude": 0.437714, "yaw": 25, "speedX": 13, "speedY": 9, "speedZ": 2, "flightTime": 440, "droneType": "p4", "droneID": "test010", "currentTime": 1472818462, "userInfo": "User Info Field", "orderID": "7f5de270-702c-11e6-8f5a-075bc1a783de", "status": 0, "platform": "ios"}]}"
// @Success 200 Success
// @Failure 500 Server Error
// @router /uploadstatus [post]
func (c *DebugController) DebugUploadStatus() {
	flyrecord, err := models.CreateFlyRecordFromBytes(c.Ctx.Input.RequestBody)
	utils.CheckErr(err, "Json Parse Error")
	if err != nil {
		c.Ctx.WriteString("Json Parse Error")
		return
	}

	fmt.Println(flyrecord.String())
	c.Ctx.WriteString(models.AddToOnlineListMySQL(flyrecord))
}

// DebugWeather description
// @Title Get Current Weather
// @Description Fetch the weather condition for specific location
// @Success 200 The weather condition
// @Failure 500 Server Error
// @router /weather [post]
func (c *DebugController) DebugWeather() {
	weather, err := utils.GetWeather(29.7814, 119.65720)
	utils.CheckErr(err, "Get Weather Error")
	c.Ctx.WriteString(weather)
}

// DebugNear description
// @Title Find the Nearby Drones
// @Description Given droneID, latitude and longitude, and find the nearby drones
// @Param droneid formData string true "The ID of the Simulated Drone"
// @Param latitude formData string true "The latitude of current position"
// @Param longitude formData string  true "The longitude of current position"
// @Success 200 success
// @Failure 400 no enought input parameter
// @Failure 500 record conflict TODO
// @router /near [post]
func (c *DebugController) DebugNear() {

	droneid := c.Input().Get("droneid")

	longitudestr := c.Input().Get("longitude")
	longitude, err := strconv.ParseFloat(longitudestr, 64)
	utils.CheckErr(err, "Longitude Parse Error")

	latitudestr := c.Input().Get("latitude")
	latitude, err := strconv.ParseFloat(latitudestr, 64)
	utils.CheckErr(err, "Latitude Parse Error")

	if err != nil {
		c.Ctx.WriteString("Parameter Error")
		return
	}

	c.Ctx.WriteString(models.FindNearbyDronesMySQL(droneid, latitude, longitude))
}
