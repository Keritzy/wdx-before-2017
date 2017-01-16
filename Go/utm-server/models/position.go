package models

import "fmt"

// Position is a struct to store speed detail
type Position struct {
	Altitude  float64
	Latitude  float64 // -90 to 90
	Longitude float64 // -180 to 180
	Yaw       int     // Direction north - 0, east - 90, west - 270
}

func (p *Position) String() string {
	return fmt.Sprintf("[POSITION] Altitude:%f, Latitude:%f, Longitude:%f, Yaw:%d",
		p.Altitude, p.Latitude, p.Longitude, p.Yaw)
}

// JSONStringStrip is a method to generate JSON string
func (p *Position) JSONStringStrip() string {
	return fmt.Sprintf("\"latitude\": %f, \"longitude\": %f, \"altitude\": %f, \"yaw\": %d",
		p.Latitude, p.Longitude, p.Altitude, p.Yaw)
}
