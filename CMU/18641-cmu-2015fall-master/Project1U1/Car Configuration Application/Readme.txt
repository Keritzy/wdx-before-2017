# Project1 Unit1

## Platform

+ Operating System: OS X El Capitan 10.11
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
    + Focus.txt: The description of the Ford car
    + input_format: The input format of the data
    + auto.ser: The serialized data for test
+ Readme

## Run the project

1. Import the project into Eclipse
2. Select Driver class and run

## Code Design

Basically I follow the instruction given by the assignment.

There are three packages in this project, they are:

1. (package)driver: main method class, run the project with the requirements
stated in the assignment.
2. (package)model: classes for Automotive and OptionSet. The option class is 
the inner class of OptionSet. The Automotive class has all the methods that
required by the assignment.
3. (package)util: class for loading the data file, serialize the class and 
deserialize the data file.

The method of each class can be seen in the class diagram.

1. All the methods in class OptionSet and Option are protected to hidden the 
data so that other class can only access specific data. 
2. All the variables in class Automotive, OptionSet and Option are private, just
as the document stated.

I've also tested the setter/getter/update/delete methods in these classes to 
test if everything works well here.

Detailed output result can be found in test_output.txt file.
