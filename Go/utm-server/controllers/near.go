package controllers

import (
	"github.com/astaxie/beego"
)

// NearController : Get Nearby Drones and Information
type NearController struct {
	beego.Controller
}

// Get method description
// @Title Show Near Controller Welcome Page
// @Description Basic information about near controller
// @Success 200 Position Near Welcome Page
// @Failure 404 Template not found
// @router / [get]
func (c *NearController) Get() {
	c.Data["Email"] = "da.wang@dji.com"
	c.Data["Message"] = "Near Controller Test"
	c.TplName = "index.tpl"
}
