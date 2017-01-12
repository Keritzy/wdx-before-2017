package main

import(
  "bufio"
  "fmt"
  "math"
  "os"
  "runtime"
)

type polar struct{
  radius float64
  theta float64
}

type cartesian struct {
  x float64
  y float64
}

var prompt = "输入一个弧长 radius 和角度 theta，例如 12.5 90，或 %s 来退出"

func init() {
  if runtime.GOOS == "windows" {
    prompt = fmt.Sprintf(prompt, "Ctrl+Z, Enter")
  } else {
    prompt = fmt.Sprintf(prompt, "Ctrl+D")
  }
}

func main() {
  questions := make(chan polar)
  defer close(questions)
  answers := createSolver(questions)
  defer close(answers)
  interact(questions, answers)
}

func createSolver(questions chan polar) chan cartesian {
  answers := make(chan cartesian)
  go func() {
    for {
      polarCoord := <-questions
      theta := polarCoord.theta * math.Pi / 180.0
      x := polarCoord.radius * math.Cos(theta)
      y := polarCoord.radius * math.Sin(theta)
      answers <- cartesian{x, y}
    }
  }()
  return answers
}

const result = "Polar radius=%.02f theta=%.02f degree -> Cartesian x=%.02f y=%.02f\n"

func interact(questions chan polar, answers chan cartesian) {
  reader := bufio.NewReader(os.Stdin)
  fmt.Println(prompt)
  for {
    fmt.Printf("Radius and angle: ")
    line, err := reader.ReadString('\n')

    if err != nil {
      break
    }
    var radius, theta float64
    if _, err := fmt.Sscanf(line, "%f %f", &radius, &theta); err != nil {
      fmt.Println(os.Stderr, "invalid input")
      continue
    }
    questions <- polar{radius, theta}
    coord := <-answers
    fmt.Printf(result, radius, theta, coord.x, coord.y)
  }
  fmt.Println()
}
