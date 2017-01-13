/**
 * 
 */
package server;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * This is the main entrance to start the server
 */
public class Server {
	/*
	public static void main(String[] args) {
		Server server = new Server();
		server.runServer();
	}
	*/
	
	private ServerSocket serverSocket = null;
	private final int PORT = 8888;
	
	public Server(){
		System.out.println("Starting Server...");
		try {
			// open this server
			serverSocket = new ServerSocket(PORT);
		} catch (IOException e) {
			e.printStackTrace();
			System.err.println("Could not listen on port:" + PORT);
			System.exit(1);
		}	
		System.out.println("Listening on port " + PORT);
		System.out.println("Server Started...\nWaiting for connection...");
	}
	
	public void runServer(){
		DefaultSocketClient defaultClientSocket = null;
		try {
			while(true) {
				// accept the client
				Socket clientSocket = serverSocket.accept();
				// use a defaultClientSocket to handle the session
				defaultClientSocket = new DefaultSocketClient(clientSocket);
	            defaultClientSocket.start();
			}
        } catch (IOException e) {
        		e.printStackTrace();
        		System.err.println("Accept failed");
    			System.exit(1);
        }
	}

}
