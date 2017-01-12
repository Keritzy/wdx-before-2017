package main

import(
  "fmt"
  "strings"
)

type BitFlag int
const (
  Active BitFlag = 1 << iota // 1 << 0 == 1
  Send // 隐式设置为 1 << iota，所以 1 << 1 == 2
  Receive // 隐式设置为 1 << iota，所以 1 << 2 == 4
)

func (flag BitFlag) String() string {
  var flags []string
  if flag & Active == Active {
    flags = append(flags, "Active")
  }
  if flag & Send == Send {
    flags = append(flags, "Send")
  }
  if flag & Receive == Receive {
    flags = append(flags, "Receive")
  }
  if len(flags) > 0 { // 这里，int(flag) 用于防止无限循环，至关重要
    return fmt.Sprintf("%d(%s)", int(flag), strings.Join(flags, "|"))
  }
  return "0()"
}


func main() {
  var flag = Active | Send
  fmt.Println(BitFlag(0), Active, Send, flag, Receive, flag|Receive)
}
