package models

import "fmt"

// Speed is a struct to store speed detail
type Speed struct {
	X int
	Y int
	Z int
}

// JSONStringStrip is a method to generate JSON string
func (s *Speed) JSONStringStrip() string {
	return fmt.Sprintf("\"speedX\": %d, \"speedY\": %d, \"speedZ\": %d",
		s.X, s.Y, s.Z)
}

func (s *Speed) String() string {
	return fmt.Sprintf("[SPEED] X:%d, Y:%d, Z:%d",
		s.X, s.Y, s.Z)
}
