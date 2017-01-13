# Project1 Unit5

## Platform

+ Operating System: OS X El Capitan 10.11
+ Eclipse Version: Java EE IDE Mars Release(4.5.0)
+ Java Version: JavaSE-1.7

## How to Run the project

1. Import these two projects into Eclipse
2. Run the server first(server.Server.java)
    + This is for storing all the car models
3. Then run the client (client.SocketClient.java)
    + Use this to upload several car model so that the servlet has something to present.
4. Shut down the client
5. Then run the servlet (servlet.SelectModel.java)
    + The page will be shown in a browser in Eclipse
6. After all is done, shut down the server.

Almost all the output can be seen from the terminal of the Client, including:

1. Function Selection: 1 for upload, 2 for configure
2. For upload, just choose the index from the file list
3. For configuration
    3.1 First choose a model
    3.2 Then the program will iterate from the first optionset to the last
    3.3 Just choose by inputing the index
4. After configuration there will be a complete figure to show your choices.


Run Servlet on Tomcat and visit the following page: http://localhost:8080/Car_Configuration_Servlet/SelectModel

And you can see the pages that show the information about the car models which are just uploaded by the client to the server.

More detail and screenshots will be in the attached pdf file.

Shut down the server will cause client encounting errors, so just close the client first.

The test output can be seen in separate project folders. More detail descriptions are in the client/server folder. This readme just gives you a glance of the project.

## Important

The main focus of the project are:

1. SelectModel.java is the first servlet for you to choose a model.
2. SelectOption.java is the second servlet for you to configure a model.
3. Result.jsp is the final page for your configure result.

And the code from the server side as well as the client side need to make some modification for supporting servlet features.
