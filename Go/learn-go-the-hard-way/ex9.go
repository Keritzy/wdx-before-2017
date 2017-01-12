package main

import  (
  "fmt"
)

func main() {
  massForPlanet := make(map[string]float64)
  massForPlanet["Mercury"] = 0.06
  massForPlanet["Venus"] = 0.82
  massForPlanet["Earth"] = 1.00
  massForPlanet["Mars"] = 0.11
  fmt.Println(massForPlanet)
  massForPlanet["Jupiter"] = 317.82
  massForPlanet["Saturn"] = 95.16
  massForPlanet["Uranus"] = 14.371
  massForPlanet["Neptune"] = 17.147
  fmt.Println(massForPlanet)
  big := "Jupiter"
  fmt.Println(big, massForPlanet[big])
}
