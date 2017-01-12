package main

import(
  "fmt"
  "bufio"
  "os"
)

func main() {
  reader := bufio.NewReader(os.Stdin)
  fmt.Printf("Type something: ")
  if line, err := reader.ReadString('\n'); err != nil {
    fmt.Println("Something Wrong")
  } else {
    fmt.Println("You just typed:", line)
  }
}
