# Project1 Unit6

## Platform

+ Operating System: OS X El Capitan 10.11
+ Eclipse Version: Java EE IDE Mars Release(4.5.0)
+ Java Version: JavaSE-1.7
+ MySQL Version: 5.6.27

## Files Description

+ Lessons_Learned.pdf: 50 answers for what I have learnt
+ Readme.txt: How to run the project

## How to Run the project

1. Import the Server Database Project into Eclipse
2. Run the local Database
3. Run Driver Class

Note: You need to change the username and password for connecting to the database. There are two places where you need to modify the code(Driver.java(package driver) & DatabaseIO.java(package util)) so that it can connect to the local database correctly.

Most output can be seen from test_output.txt in the Server Database Project folder. I tested add/delete/update method for the database and show the result.

## Important

The main focus of the project are:

1. DatabaseAuto is the interface for all database related method and declared in BuildAuto, implemented in proxyAutomobile.java
2. jdbc jar is also included in the lib folder.


