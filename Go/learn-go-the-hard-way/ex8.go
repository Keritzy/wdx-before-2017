package main

import(
  "fmt"
)

func main() {
  // 声明数组
  cities := [...]string{"Shanghai", "Guangzhou", "Shenzhen", "Beijing"}
  fmt.Printf("%-8T %2d %q\n", cities, len(cities), cities)

  // 声明切片
  s := []string{"A", "B", "C", "D", "E", "F", "G"}
  t := s[:5]
  u := s[3:len(s)-1]
  fmt.Println(s, t, u)
  u[1] = "x"
  fmt.Println(s, t, u)

  // 遍历切片
  for i, letter := range s {
    fmt.Println(i, letter)
  }
}
