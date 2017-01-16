package controllers

import (
	"github.com/astaxie/beego"
)

// WeatherController : weather related operations
type WeatherController struct {
	beego.Controller
}

// Get method description
// @Title Show Weather Controller Welcome Page
// @Description Basic information about weather controller
// @Success 200 WeatherController Welcome Page
// @Failure 404 Template not found
// @router / [get]
func (c *WeatherController) Get() {
	c.Data["Email"] = "da.wang@dji.com"
	c.Data["Message"] = "Weather Controller Test"
	c.TplName = "index.tpl"
}
