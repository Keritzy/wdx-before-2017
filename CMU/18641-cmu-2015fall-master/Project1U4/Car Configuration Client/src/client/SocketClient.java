/**
 * 
 */
package client;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.Properties;

import model.Automobile;
import util.FileIO;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * This is the entrance to the Client, combined with connection code
 */
public class SocketClient {
	public static final int PORT = 8888;
	public static final String fileList = "fileList.txt";
	
	public static void main(String args[]){
		String localHost = "";
		// get local IP address
		try {
			localHost = InetAddress.getLocalHost().getHostAddress();
		} catch (UnknownHostException e) {
			System.err.println("Unable to find local host");
		}
		// start this client
		System.out.println("Starting Client...");
		SocketClient client = new SocketClient(localHost);
		client.run();
		System.out.println("Client Started...\nWaiting for operations...");		
		
	}
	
	private Socket clientSocket;
	private String serverIP;
	
	ObjectOutputStream objectOutputStream = null; // for all output 
	ObjectInputStream objectInputStream = null; // for all input
	
	
	public SocketClient(String serverIP) {
		setServerIP(serverIP);
	}

	public void setServerIP(String serverIP) {
		this.serverIP = serverIP;
	}
	
	// run
	public void run() {
		if (openConnection()) {
			handleSession();
			closeSession();
		}
	}
	
	
	
	public boolean openConnection() {
		try {
			clientSocket = new Socket(serverIP, PORT); // connected to server
		} catch (IOException socketError) {
			System.err.println("Unable to connect to " + serverIP);
			return false;
		}
		try {
			// open input and output stream
			objectInputStream = new ObjectInputStream(
					clientSocket.getInputStream());
			objectOutputStream = new ObjectOutputStream(
					clientSocket.getOutputStream());
		} catch (Exception e) {
			System.err.println(
					"Unable to obtain stream to/from " + serverIP);
			return false;
		}
		return true;
	}
	
	@SuppressWarnings("unchecked")
	public void handleSession() {
		String fromServer = "";
		String fromUser = "";
		BufferedReader stdIn = new BufferedReader(
				new InputStreamReader(System.in));

		System.out.println("Handling session with " + serverIP + ":" + PORT);

		// 0 display the successful connection
		try {
			fromServer = (String) objectInputStream.readObject();
			System.out.println("Server: " + fromServer);
		} catch (ClassNotFoundException | IOException e) {
			e.printStackTrace();
		}
		
		while (true) {
			// print list for what you could do
			printMenu();

			// input what you could do and transfer to server
			try {
				fromUser = stdIn.readLine();
			} catch (IOException e) {
				continue;
			}

			if (fromUser.equals("0")) {
				// sent your quit to server
				try {
					objectOutputStream.writeObject(fromUser);
				} catch (IOException e) {
					e.printStackTrace();
				}
				break;
			} else if (fromUser.equals("1") || fromUser.equals("2")) {

			} else {
				System.out.println("Illegal Input");
				continue;
			}

			// sent your option to server
			try {
				objectOutputStream.writeObject(fromUser);
			} catch (IOException e) {
				e.printStackTrace();
			}
			
			
			//1. Upload
			//2. Configure
			while (true) {// inner while
				if (fromUser.equals("1")) { // if upload
					try {
						// get successful operation reply
						fromServer = (String) objectInputStream.readObject();
						System.out.println("Server: " + fromServer);
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}
					// get file name and choose function to build car in server
					String[] autoFileList = printFileList();
					try {
						fromUser = stdIn.readLine();
						while (!fromUser.matches("[0-9]+")
								|| Integer.parseInt(fromUser) < 0
								|| Integer.parseInt(fromUser) > autoFileList.length - 1) {
							System.out.println("please input a legal number");
							fromUser = stdIn.readLine();
						}
					} catch (IOException e1) {
						e1.printStackTrace();
					}

					int fileIndex = Integer.parseInt(fromUser);

					String fileName = autoFileList[fileIndex];
					// send the file name to server
					try {
						objectOutputStream.writeObject(fileName);
						// get file name received reply
						fromServer = (String) objectInputStream.readObject();
						System.out.println("Server: " + fromServer);
					} catch (IOException | ClassNotFoundException e) {
						e.printStackTrace();
					}
					
					Properties prop = new Properties();

					try {
						// upload the car file in client
						FileInputStream fileInputStream = new FileInputStream(
								fileName);
						prop.load(fileInputStream);
						fileInputStream.close();
						// write the properties object to server
						objectOutputStream.writeObject(prop);

					} catch (IOException e) {
						e.printStackTrace();
					}

					// get the success reply
					try {
						fromServer = (String) objectInputStream
								.readObject();
						System.out.println("Server: " + fromServer);
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}
				} // if upload
				
				else {// "2" configure
					try {
						// get successful operation reply
						fromServer = (String) objectInputStream.readObject();
						System.out.println("Server: " + fromServer);
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}

					// get auto name list from stream
					ArrayList<String> autoNameList = null;
					try {
						autoNameList = (ArrayList<String>) objectInputStream
								.readObject();
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}

					// get transfer name list back success reply
					try {
						fromServer = (String) objectInputStream.readObject();
						System.out.println("Server: " + fromServer);
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}

					// if no saved car, break as the client and please
					// upload a car first by any terminal
					if (autoNameList.size() == 0) {
						System.out.println("Empty List in Server, "
								+ "please upload a car first");
						break;
					}

					// print all available list
					System.out.println("Auto Model Options :");
					for (int i = 0; i < autoNameList.size(); i++) {
						System.out.println("Model " + i + " : "
								+ autoNameList.get(i));
					}
					// choose a car to configure
					System.out.println("Select a number of model to configure");
					try {
						// read in a automobile name index
						fromUser = stdIn.readLine();
					} catch (IOException e) {
						e.printStackTrace();
					}

					while (!fromUser.matches("[0-9]+")
							|| Integer.parseInt(fromUser) < 0
							|| Integer.parseInt(fromUser) > autoNameList.size() - 1) {
						System.out.println("please input a legal number");
						try {
							fromUser = stdIn.readLine();
						} catch (IOException e) {
							e.printStackTrace();
						}
					}
					int configureAutoIndex = Integer.parseInt(fromUser);
					String modelName = autoNameList.get(configureAutoIndex);

					// write the required model name to server
					try {
						objectOutputStream.writeObject(modelName);
					} catch (IOException e) {
						e.printStackTrace();
					}

					// get selected automobile from server
					Automobile selectedAuto = null;
					try {
						selectedAuto = (Automobile) objectInputStream
								.readObject();
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}

					// get transfer successful reply
					try {
						fromServer = (String) objectInputStream.readObject();
						System.out.println("Server: " + fromServer);
					} catch (ClassNotFoundException | IOException e) {
						e.printStackTrace();
					}

					
					// start configure your car in SelectCarOptionClass
					System.out.println("Start Configure the Car");
					new CarOption().configureCarChoice(selectedAuto);

					// get the configured car and print all your choice and price
					System.out.println("Configured Car For Your Choice");
					System.out.println(selectedAuto.getName());
					selectedAuto.printChoice();
					System.out.println();
				}
				
				break;

			}// inner while
		}// outer while

	}
	
	public void closeSession() {
		try {
			clientSocket.close();
			objectOutputStream.close();
			objectInputStream.close();
			System.out.println("closed!");
		} catch (IOException e) {
			System.err.println("Error closing socket to " + serverIP);
		}
	}
	
	public void printMenu() {
		System.out.println("Select Function: ");
		System.out.println("1.Upload");
		System.out.println("2.Configure");
		System.out.println("0.Quit");
		System.out.print("Your Choice: ");
	}
	
	public String[] printFileList() {	
		String[] list = new FileIO().getAutoFileList(fileList);
		for (int i = 0; i < list.length; i++) {
			System.out.println(i + " " + list[i]);
		}
		return list;
	}
	
}
