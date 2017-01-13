/**
 * 
 */
package server;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Properties;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * This is the socket client class to be used in the server class
 */
public class DefaultSocketClient extends Thread{
	private final int PORT = 8888;
	private Socket clientSocket = null;

	private ObjectInputStream objectInputStream = null;
	private ObjectOutputStream objectOutputStream = null;
	
	public DefaultSocketClient(Socket clientSocket){
		this.clientSocket = clientSocket;
	}
	
	public void run(){
		if (openConnection()){
			handleSession();
		}
	}
	
	public boolean openConnection() {
		try {
			objectOutputStream = new ObjectOutputStream(
					clientSocket.getOutputStream());
			objectInputStream = new ObjectInputStream(clientSocket.getInputStream());
		} catch (Exception e) {
			System.err.println("Unable to obtain stream to/from Port"
						+ PORT);
			return false;
		}
		return true;
	}
	
	public void handleSession(){
		String input;
		System.out.println("Starting working on Port " + PORT);
		try {
			objectOutputStream.writeObject("Connected to Port " + PORT);
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		BuildCarModelOptions buildCarModelOptions = new BuildCarModelOptions();
		
		while(true){
			try {
				// get option
				input = (String)objectInputStream.readObject();
			} catch (ClassNotFoundException | IOException e) {
				continue;
			}
			
			if (input.equals("0")){
				// 0.1 get quit here
				break;
			} else if(input.equals("1") || input.equals("2")){
				
			}else{
				System.out.println("Illegal Input, only 0, 1, 2 are available");
				continue;
			}
			
			while(true){
				if (input.equals("1")) {
					// send get request reply
					try {
						objectOutputStream.writeObject(
								"Get upload automobile request, Please input "
								+ "the .properties file number");
					} catch (IOException e) {
						e.printStackTrace();
					}
								
					// get file name and parse it
					String fileName = null;
					try {
						fileName = (String)objectInputStream.readObject();
						// write back the file name
						objectOutputStream.writeObject("Get File Name: " + fileName);
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
						fileName = "null";
					}
					
					Properties prop = null;
					try {
						prop = (Properties) objectInputStream.readObject();
					} catch (ClassNotFoundException | IOException e1) {
						e1.printStackTrace();
					}
					// add car in linked hash map
					buildCarModelOptions.buildAutoFromProperty(prop);
					
					//reply build automobile successful
					try {
						objectOutputStream.writeObject(
								"build Auto successfully");
					} catch (IOException e) {
						e.printStackTrace();
					}

					break;	
				} // upload
				else {
					// get configure request, send get request reply
					try {
						objectOutputStream.writeObject("Get configure request");
					} catch (IOException e) {
						e.printStackTrace();
					}
					
					// get all auto name in server and send back
					ArrayList<String> autoNameList = 
							buildCarModelOptions.getModelList();
					try {
						objectOutputStream.writeObject(autoNameList);
					} catch (IOException e) {
						e.printStackTrace();
					}
					// send autolist in the server to client
					try {
						objectOutputStream.writeObject(
								"send AutoList successfully");
					} catch (IOException e) {
						e.printStackTrace();
					}
					// if no saved car, break
					if (autoNameList.size() == 0) {
						break;
					}

					// get model name from server
					String modelName = null;
					try {
						modelName = (String)objectInputStream.readObject();
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}
					
					// send the selected auto mobile to client
					try {
						buildCarModelOptions.sendModel(
								objectOutputStream, modelName);
					} catch (IOException e1) {
						e1.printStackTrace();
					}
					
					//send the successful reply to client
					try {
						objectOutputStream.writeObject(
								"Transfer automobile succesfully");
					} catch (IOException e) {
						e.printStackTrace();
					}
			
					break;
				}// 2 configure
			}
		}
	}
}
