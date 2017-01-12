package main

import "fmt"

func main() {
  fmt.Println("Here are some basic fact:")
  fmt.Println("2 hour has", 2*60 , "seconds", )
  fmt.Println("Averge days in a month is", 365.0 / 12)
  fmt.Println("Is it true that 3 + 2 < 5 - 7?", (3 + 2) < (5 - 7))
  name := "wdxtub.com"
  fmt.Println("The address for variable 'name' is", &name)
}
