package routers

import (
	"github.com/astaxie/beego"
)

func init() {

	beego.GlobalControllerRouter["utm-server/controllers:DebugController"] = append(beego.GlobalControllerRouter["utm-server/controllers:DebugController"],
		beego.ControllerComments{
			Method: "Get",
			Router: `/`,
			AllowHTTPMethods: []string{"get"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:DebugController"] = append(beego.GlobalControllerRouter["utm-server/controllers:DebugController"],
		beego.ControllerComments{
			Method: "DebugUploadStatus",
			Router: `/uploadstatus`,
			AllowHTTPMethods: []string{"post"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:DebugController"] = append(beego.GlobalControllerRouter["utm-server/controllers:DebugController"],
		beego.ControllerComments{
			Method: "DebugWeather",
			Router: `/weather`,
			AllowHTTPMethods: []string{"post"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:DebugController"] = append(beego.GlobalControllerRouter["utm-server/controllers:DebugController"],
		beego.ControllerComments{
			Method: "DebugNear",
			Router: `/near`,
			AllowHTTPMethods: []string{"post"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:NearController"] = append(beego.GlobalControllerRouter["utm-server/controllers:NearController"],
		beego.ControllerComments{
			Method: "Get",
			Router: `/`,
			AllowHTTPMethods: []string{"get"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:SimulatorController"] = append(beego.GlobalControllerRouter["utm-server/controllers:SimulatorController"],
		beego.ControllerComments{
			Method: "Get",
			Router: `/`,
			AllowHTTPMethods: []string{"get"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:SimulatorController"] = append(beego.GlobalControllerRouter["utm-server/controllers:SimulatorController"],
		beego.ControllerComments{
			Method: "SingleFlyRecord",
			Router: `/single`,
			AllowHTTPMethods: []string{"post"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:SimulatorController"] = append(beego.GlobalControllerRouter["utm-server/controllers:SimulatorController"],
		beego.ControllerComments{
			Method: "MultipleDiffIDFlyRecord",
			Router: `/multiplediff`,
			AllowHTTPMethods: []string{"post"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:StatusController"] = append(beego.GlobalControllerRouter["utm-server/controllers:StatusController"],
		beego.ControllerComments{
			Method: "Get",
			Router: `/`,
			AllowHTTPMethods: []string{"get"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:StatusController"] = append(beego.GlobalControllerRouter["utm-server/controllers:StatusController"],
		beego.ControllerComments{
			Method: "UploadStatus",
			Router: `/upload`,
			AllowHTTPMethods: []string{"post"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:StatusController"] = append(beego.GlobalControllerRouter["utm-server/controllers:StatusController"],
		beego.ControllerComments{
			Method: "TestRedisConnection",
			Router: `/testredis`,
			AllowHTTPMethods: []string{"get"},
			Params: nil})

	beego.GlobalControllerRouter["utm-server/controllers:WeatherController"] = append(beego.GlobalControllerRouter["utm-server/controllers:WeatherController"],
		beego.ControllerComments{
			Method: "Get",
			Router: `/`,
			AllowHTTPMethods: []string{"get"},
			Params: nil})

}
