package utils

import (
	"fmt"
	"io/ioutil"
	"net/http"
	"net/url"

	"github.com/bitly/go-simplejson"
)

// CheckErr is a method to check err and print info
func CheckErr(err error, info string) {
	if err != nil {
		fmt.Println(err)
		fmt.Println(info)
	}
}

// GetWeather is a method to get weather according to location
func GetWeather(latitude float64, longitude float64) (info string, err error) {
	// Parse query to url
	URL, _ := url.Parse("https://query.yahooapis.com/v1/public/yql")
	parameters := url.Values{}
	parameters.Add("q", "select units,wind,atmosphere,astronomy from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"guangzhou\") and u=\"c\"")
	parameters.Add("format", "json")
	URL.RawQuery = parameters.Encode()
	urlParsed := URL.String()
	fmt.Println(urlParsed)

	// make the query
	resp, err := http.Get(urlParsed)
	CheckErr(err, "连接错误")
	defer resp.Body.Close()

	body, err := ioutil.ReadAll(resp.Body)
	CheckErr(err, "无法读取")

	json, err := simplejson.NewJson(body)
	CheckErr(err, "解析 Json 错误")
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
