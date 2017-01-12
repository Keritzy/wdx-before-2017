package main

import (
  "bufio"
  "fmt"
  "io"
  "log"
  "os"
  "path/filepath"
)

func main() {
  infileName, outfileName, err := loadFromCmd()
  if err != nil {
    // 如果从命令行中读取参数错误，则打印错误信息并退出程序
    fmt.Println(err)
    os.Exit(1)
  }

  // 默认接收标准输入输出
  infile, outfile := os.Stdin, os.Stdout
  if infileName != "" {
    // 处理输入文件不存在的情况
    if infile, err = os.Open(infileName); err != nil {
      log.Fatal(err)
    }
    defer infile.Close()
  }
  if outfileName != "" {
    // 处理输出文件
    if outfile, err = os.Create(outfileName); err != nil {
      log.Fatal(err)
    }
    defer outfile.Close()
  }

  // 具体进行处理
  if err = process(infile, outfile); err != nil {
    log.Fatal(err)
  }
}

// 从命令行读取参数
func loadFromCmd() (infileName, outfileName string, err error) {
  if len(os.Args) > 1 && (os.Args[1] == "-h" || os.Args[1] == "--help"){
    err = fmt.Errorf("usage: %s [<] infile.txt [>] outfile.txt", filepath.Base(os.Args[0]))
    return "", "", err
  }
  if len(os.Args) > 1 {
    infileName = os.Args[1]
    if len(os.Args) > 2 {
      outfileName = os.Args[2]
    }
  }
  if infileName != "" && infileName == outfileName {
    log.Fatal("won't overwrite the infile")
  }
  return infileName, outfileName, nil
}

// 处理文件
func process(infile io.Reader, outfile io.Writer)(err error) {
  reader := bufio.NewReader(infile)
  writer := bufio.NewWriter(outfile)
  defer func() {
    if err == nil {
      err = writer.Flush()
    }
  }()

  eof := false
  for !eof {
    var line string
    line, err = reader.ReadString('\n')
    if err == io.EOF {
      err = nil
      eof = true
    } else if err != nil {
      return err
    }
    if _, err = writer.WriteString(line); err != nil {
      return err
    }
  }
  return nil
}
