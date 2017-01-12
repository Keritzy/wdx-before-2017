package routers

import (
	"watch-plane-together/controllers"
	"github.com/astaxie/beego"
)

func init() {
    beego.Router("/", &controllers.MainController{})
    beego.Router("/debug/", &controllers.DebugController{})
    beego.Router("/debug/position", &controllers.DebugController{}, "post:DebugPosition")
    beego.Router("/debug/weather", &controllers.DebugController{}, "get:DebugWeather")
    beego.Router("/debug/flight", &controllers.DebugController{}, "get:DebugFlight")
    beego.Router("/debug/near", &controllers.DebugController{}, "post:DebugNear")
    beego.Router("/debug/upload", &controllers.DebugController{}, "post:DebugUpload")
}
