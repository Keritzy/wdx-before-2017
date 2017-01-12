package main

import(
  "fmt"
)

func main() {
  // 表达式 switch
  var suffix = ".gz"
  var result string
  switch suffix {
  case ".gz":
    result = "This is a Gzip file"
  case ".tar", ".tar.gz", ".tgz":
    result = "This is a Tar file"
  case ".zip":
    result = "This is a Zip file"
  }
  fmt.Println(result)

  // 类型 switch
  classifier(5, -17.9, "LOL", nil, true, complex(1, 2))

}

func classifier(items...interface{}) {
  for i, x := range items {
    switch x.(type) {
    case bool:
      fmt.Printf("Param #%d is a bool\n", i)
    case float64:
      fmt.Printf("Param #%d is a float64\n", i)
    case int, int8, int16, int32, int64:
      fmt.Printf("Param #%d is a int\n", i)
    case uint, uint8, uint16, uint32, uint64:
      fmt.Printf("Param #%d is an unsigned int\n", i)
    case nil:
      fmt.Printf("Param #%d is nil\n", i)
    case string:
      fmt.Printf("Param #%d is a string\n", i)
    default:
      fmt.Printf("Param #%d is unknown type\n", i)
    }
  }
}
