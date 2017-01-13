# Project1 Unit5 - Servlet

## Platform

+ Operating System: OS X El Capitan 10.11
+ Eclipse Version: Java EE IDE Mars Release(4.5.0)
+ Java Version: JavaSE-1.7

## File Description

+ Eclipse Project
    + build(folder)
    + src(folder)
    + WebContent(folder)
+ Class Diagram
    + class_diagram.jpg
+ Test Output
    + test_output.txt
+ Data Files
    + Focus.Properties: The property file of the Ford car
    + ModelS.Properties: The property file of the Tesla car
    + fileList.txt: The file list of model property files
    + Sample_Car.Properties: The property file of the sample car
+ Readme

## Run the project

The running detail of the project is in the readme.txt from the parent folder. This readme.txt file is just the descrpition for the servlet itself.

## Code Design

Basically I follow the instruction given by the assignment.

There are five packages in this project, they are:

1. (package)client: classes for the client
2. (package)model: classes for CarShop(contains the hashmap for Automobile),
Automobile and OptionSet. The option class is the inner class of OptionSet.
The classes have all the methods that required by the assignment.
3. (package)util: class for loading the data file, serialize the class and
deserialize the data file.
4. (package)exception: custom exception enum as well as exception handler
5. (package)servlet: two servlets for showing the car model information on the webpage

The method of each class can be seen in the class diagram.

Implement and Test the design and requirements from the document

I've also tested the setter/getter/update/delete methods in these classes to
test if everything works well here.

Detailed output result can be found in test_output.txt file.
