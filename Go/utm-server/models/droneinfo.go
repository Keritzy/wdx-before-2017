package models

import "fmt"

// DroneInfo is a struct to store drone information
type DroneInfo struct {
	FlightTime int
	DroneType  string
	DroneID    string
}

func (d *DroneInfo) String() string {
	return fmt.Sprintf("[DRONEINFO] FlightTime:%d, DroneType:%s, DroneID:%s",
		d.FlightTime, d.DroneType, d.DroneID)
}

// JSONStringStrip is a method to generate JSON string
func (d *DroneInfo) JSONStringStrip() string {
	return fmt.Sprintf("\"flightTime\": %d, \"droneType\": \"%s\", \"droneID\": \"%s\"",
		d.FlightTime, d.DroneType, d.DroneID)
}
