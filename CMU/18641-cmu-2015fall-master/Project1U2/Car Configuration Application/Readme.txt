# Project1 Unit2

Part A and Part B combined (The changes from part A to part B is listed
below)

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
    + ModelS.txt: The description of the Tesla car
    + input_format: The input format of the data
    + auto.ser: The serialized data for test
    + exception_log: log file for exceptions
+ Readme

## Run the project

1. Import the project into Eclipse
2. Select Driver class and run

## Code Design

Basically I follow the instruction given by the assignment.

There are five packages in this project, they are:

1. (package)driver: main method class, run the project with the requirements
stated in the assignment.
2. (package)model: classes for CarShop(contains the hashmap for Automobile),
Automobile and OptionSet. The option class is the inner class of OptionSet.
The classes have all the methods that required by the assignment.
3. (package)util: class for loading the data file, serialize the class and
deserialize the data file.
4. (package)adapter: interfaces and abstract class for better OOP design
5. (package)exception: custom exception enum as well as exception handler

The method of each class can be seen in the class diagram.

1. This code combines part A and part B (The changes will be listed below)
2. Implement and Test the design and requirements from the document

I've also tested the setter/getter/update/delete methods in these classes to
test if everything works well here.

Detailed output result can be found in test_output.txt file.

Detailed exception log can be found in exception_log.txt file

## Changes from part A to part B

The main change is to use LinkedHashMap to store multiple car models. At first
I use the data structure straight inside ProxyAutomobile but as the code becomes
more and more, the ProxyAutomobile become too complex to easily locate the
functions and variables. So I create a new class just for the LinkedHashMap and
name it as CarShop because one CarShop may have multiple car models for the
customer.

After several modification for the new class, I can transform the design in
part A to part B.
