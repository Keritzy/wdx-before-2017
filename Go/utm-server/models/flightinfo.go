package models

import "fmt"

// FlightInfo is a struct to store flight information
type FlightInfo struct {
	CurrentTime int64
	UserInfo    string
	OrderID     string
	Status      int    // 1 - finished, 0 - running
	Platform    string // ios, android
}

func (f *FlightInfo) String() string {
	return fmt.Sprintf("[FLIGHTINFO] CurrentTime:%d, UserInfo:%s, OrderID:%s, Status:%d, Platform: %s",
		f.CurrentTime, f.UserInfo, f.OrderID, f.Status, f.Platform)
}

// JSONStringStrip is a method to generate JSON string
func (f *FlightInfo) JSONStringStrip() string {
	return fmt.Sprintf("\"currentTime\": %d, \"userInfo\": \"%s\", \"orderID\": \"%s\", \"status\": %d, \"platform\": \"%s\"",
		f.CurrentTime, f.UserInfo, f.OrderID, f.Status, f.Platform)
}
