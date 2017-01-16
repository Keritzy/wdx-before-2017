// @APIVersion 1.0.0
// @Title UTM Server API
// @Description This is an auto-generated API Document for UTM Server Project
// @Contact da.wang@dji.com
// @TermsOfServiceUrl http://to.be.decided
// @License To Be Decided
// @LicenseUrl http://to.be.decided
package routers

/*
------------------------
US UTM System Design
------------------------

*/

import (
	"utm-server/controllers"

	"github.com/astaxie/beego"
)

func init() {
	ns := beego.NewNamespace("/v1",
		beego.NSNamespace("/status",
			beego.NSInclude(
				&controllers.StatusController{},
			),
		),
		beego.NSNamespace("/weather",
			beego.NSInclude(
				&controllers.WeatherController{},
			),
		),
		beego.NSNamespace("/near",
			beego.NSInclude(
				&controllers.NearController{},
			),
		),
		beego.NSNamespace("/debug",
			beego.NSInclude(
				&controllers.DebugController{},
			),
		),
		beego.NSNamespace("/simulator",
			beego.NSInclude(
				&controllers.SimulatorController{},
			),
		),
	)
	beego.AddNamespace(ns)
}
