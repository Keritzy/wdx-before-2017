package controllers

import (
	"bytes"
	"fmt"
	"io/ioutil"
	"net/http"
	"strconv"

	"github.com/astaxie/beego"

	"utm-server/models"
	"utm-server/utils"
)

// SimulatorController : Simulate all kinds of Drone actions
type SimulatorController struct {
	beego.Controller
}

// Get method description
// @Title Show Simulator Controller Welcome Page
// @Description Basic information about simulator controller
// @Success 200 Simulator Controller Welcome Page
// @Failure 404 Template not found
// @router / [get]
func (c *SimulatorController) Get() {
	c.Data["Email"] = "da.wang@dji.com"
	c.Data["Message"] = "Simulator Controller Test"
	c.TplName = "index.tpl"
}

// SingleFlyRecord description
// @Title Simulate a single Fly Record
// @Description Given droneID, latitude and longitude, and generate a single fly record
// @Param droneid formData string true "The ID of the Simulated Drone"
// @Param latitude formData string true "The latitude of current position"
// @Param longitude formData string  true "The longitude of current position"
// @Success 200 success
// @Failure 400 no enought input parameter
// @Failure 500 record conflict TODO
// @router /single [post]
func (c *SimulatorController) SingleFlyRecord() {
	c.Ctx.WriteString(fmt.Sprintf("%s", generateDiffIDFlyRecord(c, 1)))
}

// MultipleDiffIDFlyRecord description
// @Title Simulate multiple Fly Records with different droneids
// @Description Given droneID, latitude and longitude, and generate multiple fly records with different droneids
// @Param count formData string true "The count of the fly records"
// @Param droneid formData string true "The prefix ID of the Simulated Drone"
// @Param latitude formData string true "The latitude of current position"
// @Param longitude formData string  true "The longitude of current position"
// @Success 200 success
// @Failure 400 no enought input parameter
// @Failure 500 record conflict TODO
// @router /multiplediff [post]
func (c *SimulatorController) MultipleDiffIDFlyRecord() {
	countstr := c.Input().Get("count")
	count, err := strconv.ParseInt(countstr, 10, 64)
	utils.CheckErr(err, "Count Parse Error")
	c.Ctx.WriteString(fmt.Sprintf("%s", generateDiffIDFlyRecord(c, int(count))))
}

// Private method
// Generate flyrecords with different Ids
// count is the number of records that needs to be generated
// for example, if the droneid is test, count is 10
// it will generate 10 records with droneid test0, test1, ..., test9
func generateDiffIDFlyRecord(c *SimulatorController, count int) string {
	droneid := c.Input().Get("droneid")

	longitudestr := c.Input().Get("longitude")
	longitude, err := strconv.ParseFloat(longitudestr, 64)
	utils.CheckErr(err, "Longitude Parse Error")

	latitudestr := c.Input().Get("latitude")
	latitude, err := strconv.ParseFloat(latitudestr, 64)
	utils.CheckErr(err, "Latitude Parse Error")

	if err != nil {
		return "Parameter Error"
	}

	var result []byte
	for i := 0; i < count; i++ {
		body := bytes.NewBuffer([]byte(models.GenerateFlyRecordJSONString(droneid+strconv.Itoa(i), latitude, longitude)))
		res, err := http.Post("http://localhost:12345/v1/status/upload",
			"application/json;charset=utf-8",
			body)
		utils.CheckErr(err, "Send Simulated Data Error")
		if err != nil {
			return "Send Simulated Data Error"
		}

		result, err = ioutil.ReadAll(res.Body)
		res.Body.Close()
	}

	return fmt.Sprintf("%s", result)
}
