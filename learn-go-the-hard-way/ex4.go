package main

import (
  "fmt"
  "os"
  "strings"
)

func main() {
  name := "dawang" // 设置默认名字
  if len(os.Args) > 1 {
    // os.Args[0] 是程序名，后面才是自带的参数
    name = strings.Join(os.Args[1:], " ") // 把所有除程序名之外的内容用空格拼起来
  }
  fmt.Println("Hello", name)
}
