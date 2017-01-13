# Assignment2 - Object Relation and File IO

## Platform

+ Operating System: OS X Yosemite 10.10.5
+ Eclipse Version: Java EE IDE Mars Release(4.5.0)
+ Java Version: JavaSE-1.7

## File Description

+ Eclipse Project
    + bin(folder)
    + src(folder)
+ Class Diagram
    + class_diagram.jpg
+ Test Output
    + test_output.txt
+ Data Files
    + student_sample.txt: the same as the sample data given by the assignment
    + student.txt: the test data given by the assignment
    + student_45.txt: data with more than 40 students to trigger exception
+ Readme

## Run the project

1. Import the project into Eclipse
2. Select Driver class and run

## Code Design

Basically I follow the instruction given by the assignment.

There are five packages in this project, they are:

1. (package)dawang.assignment2: main method class, run the project with 3 different testcases
2. (package)dawang.assignment2.exception: classes for custom exception
3. (package)dawang.assignment2.model: classes for Student and Statistics, they both implement the interface IPrinter
4. (package)dawang.assignment2.prototype: abstract class and interface
5. (package)dawang.assignment2.util: util function for loading text file and transfer to Student/Statistics object

I design 3 different test cases to trigger the exception when there are more than 40 records in the text file. They are wrapped in a single funciton so that it will be easy to modify different testcases.

'Student' class inherits the abstract 'People' class and implements the interface defined in 'IPrinter'.

For the 'Statistics' class, the whole calculating process will be done in the same function to accelarate the computation. Also, it implements the interface defined in 'IPrinter' to show the result.

Detailed output result can be found in test_output.txt file.
