package controllers

import (
	"fmt"
	"utm-server/models"
	"utm-server/utils"

	"github.com/astaxie/beego"
	"gopkg.in/redis.v4"
)

// StatusController : Status related operations
type StatusController struct {
	beego.Controller
}

// Get method description
// @Title Show Position Controller Welcome Page
// @Description Basic information about position controller
// @Success 200 Position Controller Welcome Page
// @Failure 404 Template not found
// @router / [get]
func (c *StatusController) Get() {
	c.Data["Email"] = "da.wang@dji.com"
	c.Data["Message"] = "Position Controller Test"
	c.TplName = "index.tpl"
}

// UploadStatus description
// @Title Upload Current Flying Status
// @Description Receive data from DJI GO or Simulator
// @Param body body string true "example {"records" : [{"latitude": 43.332432, "longitude": 107.231000, "altitude": 0.437714, "yaw": 25, "speedX": 13, "speedY": 9, "speedZ": 2, "flightTime": 440, "droneType": "p4", "droneID": "test010", "currentTime": 1472818462, "userInfo": "User Info Field", "orderID": "7f5de270-702c-11e6-8f5a-075bc1a783de", "status": 0, "platform": "ios"}]}"
// @Success 200 Success
// @Failure 500 Server Error
// @router /upload [post]
func (c *StatusController) UploadStatus() {
	flyrecord, err := models.CreateFlyRecordFromBytes(c.Ctx.Input.RequestBody)
	utils.CheckErr(err, "Json Parse Error")
	if err != nil {
		c.Ctx.WriteString("Json Parse Error")
		return
	}

	fmt.Println(flyrecord.String())
	resultRedis := models.AddToRedis(flyrecord)
	resultMySQL := models.AddToFlyRecordsMySQL(flyrecord)
	c.Ctx.WriteString(resultRedis + "." + resultMySQL)
}

// TestRedisConnection description
// @Title Redis Health Check
// @Description Will be removed on product version
// @Success 200 Success
// @Failure 500 Server Error
// @router /testredis [get]
func (c *StatusController) TestRedisConnection() {
	client := redis.NewClient(&redis.Options{
		Addr:     "localhost:6379",
		Password: "",
		DB:       0,
	})

	pong, err := client.Ping().Result()
	if err != nil {
		c.Ctx.WriteString("Redis Connection Error")
		return
	}

	c.Ctx.WriteString(pong)
}
