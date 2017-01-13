/**
 * 
 */
package util;

import java.io.*;
import java.sql.*;
import java.util.*;

import model.Automobile;


/** Class for insert/update/delete items in database
 * @author dawang
 * @andrewid dawang
 */
public class DatabaseIO {
	private static final String URL = "jdbc:mysql://localhost:3306";
	private static final String USER_NAME = "root";
	private static final String PASSWORD = "123456";

	
	/**
	 * Test if the program can connect to the database
	 */
	public void testConnection() {
		try{
			Connection connection = null; 
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			if (connection != null){
				System.out.println("[SUCCESS] Connected to Database");
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally {
			
		}
	}
	
	/**
	 * Create the database for the server
	 */
	public void createDatabase(){
		Statement statement = null;
		try {
			// get connection
			Connection connection = null;
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			if (connection != null) {
				System.out.println("[SUCCESS] Connected to Database");
			}
			statement = connection.createStatement();
			String command = null;

			// delete the database with name "Automobile" to avoid exceptions
			try {
				command = "DROP DATABASE AutomobileDB;";
				statement.executeUpdate(command);
				System.out.println("[SUCCESS] DATABASE Dropped: AutomobileDB");
			} catch (Exception e) {
				
			}


			// create Database and corresponding tables
			String[] setupCommands = {
					"CREATE DATABASE AutomobileDB;",
					"USE AutomobileDB;",
					"CREATE TABLE Automobile(AutoId INT NOT NULL,AutoName varchar(255) NOT NULL,BasePrice FLOAT NOT NULL,PRIMARY KEY (AutoId));",
					"CREATE TABLE OptionSet(OptionSetId INT NOT NULL,OptionSetName varchar(255) NOT NULL,AutoId INT NOT NULL,PRIMARY KEY (OptionSetID),FOREIGN KEY (AutoID) REFERENCES Automobile(AutoID));",
					"CREATE TABLE AutoOption(OptionId INT NOT NULL,OptionName varchar(255) NOT NULL,OptionPrice FLOAT NOT NULL,OptionSetId INT NOT NULL,AutoID INT NOT NULL,PRIMARY KEY (OptionId),FOREIGN KEY (OptionSetId) REFERENCES OptionSet(OptionSetId));"
			};
			
			for (int i = 0; i < setupCommands.length; i++){
				statement.executeUpdate(setupCommands[i]);
			}
			System.out.println("[SUCCESS] DATABASE Created. TABLES created.");
			
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			if (statement != null) {
				try {
					statement.close();
				} catch (SQLException e) {
					e.printStackTrace();
				}
			}
		}
	}
	
	
	/**
	 * Add an automobile to the database
	 * @param auto : the automobile
	 * @param autoID : the automobile ID
	 * @param optSetIDStart : the option set ID index
	 * @param optionIDStart : the option ID index
	 * @return
	 */
	public int[] addToDatabase(Automobile auto, int autoID, int optSetIDStart, int optionIDStart) {
		int[] newIDs = new int[2]; // [0] for option set id, [1] for option
		
		int optionSetID = optSetIDStart;
		int optionID = optionIDStart;
		
		LinkedHashMap<String, Float> options = null;

		String[] optionSetName = { "Color", "Transmission", "Brakes/Traction Control", "Side Impact Airbags",
				"Power Moonroof" };

		try {
			// get connection
			Connection connection = null;
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			if (connection != null) {
				System.out.println("[SUCCESS] Connected to Database");
			}
			
			String addAutoStr = "INSERT INTO AutomobileDB.Automobile VALUES (?,?,?);";
			
			// insert auto name and base price in Automobile table
			PreparedStatement ps = connection.prepareStatement(addAutoStr);
			ps.setInt(1, autoID);
			ps.setString(2, auto.getName());
			ps.setFloat(3, auto.getBasePrice());
			ps.executeUpdate();

			// insert option set name into OptionSet table
			
			String addOpSetStr = "INSERT INTO AutomobileDB.OptionSet VALUES (?,?,?);";
			
			for (int i = 0; i < optionSetName.length; i++) {
				ps = connection.prepareStatement(addOpSetStr);
				ps.setInt(1, optionSetID);
				ps.setString(2, optionSetName[i]);
				ps.setInt(3, autoID);
				ps.executeUpdate();

				
				String addOpStr = "INSERT INTO AutomobileDB.AutoOption VALUES (?,?,?,?,?);";
				
				// insert options into AutoOption table
				options = auto.getOptionSetMap(optionSetName[i]);
				for (String option : options.keySet()) {

					ps = connection.prepareStatement(addOpStr);
					ps.setInt(1, optionID);
					ps.setString(2, option);
					ps.setFloat(3, options.get(option));
					ps.setInt(4, optionSetID);
					ps.setInt(5, autoID);
					ps.executeUpdate();

					optionID++;// add option id
				}

				optionSetID++;// add option set id
			}
			// set the new starter optioset id and option id
			newIDs[0] = optionSetID;
			newIDs[1] = optionID;

			System.out.println("[SUCCESS] " + auto.getName() + " Added to Database");
			// close connection
			ps.close();
			connection.close();

		} catch (SQLException e1) {
			e1.printStackTrace();
		}
		return newIDs;
	}
	
	
	/**
	 * Delete automobile from database with name
	 * @param AutoName
	 */
	public void deleteAutoInDatabase(String AutoName) {
		Connection connection = null;
		try {
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			if (connection != null) {
				System.out.println("[SUCCESS] Connected to Database");
			}

			String findAutoStr = "SELECT AutoId FROM AutomobileDB.Automobile WHERE (AutoName=?);";
			
			// get the AutoId for this AutoName
			PreparedStatement ps = connection.prepareStatement(findAutoStr);
			ps.setString(1, AutoName);
			ResultSet rs = ps.executeQuery();
			rs.next();
			int AutoID = Integer.parseInt(rs.getString("AutoID"));

			String delOpStr = "DELETE FROM AutomobileDB.AutoOption WHERE (AutoId=?);";
			
			// delete all the options of this AutoId
			ps=connection.prepareStatement(delOpStr);
			ps.setInt(1, AutoID);
			ps.executeUpdate();

			System.out.println("[SUCCESS] "  + AutoName + " Option Deleted");
			
			String delOpSetStr = "DELETE FROM AutomobileDB.OptionSet WHERE (AutoId=?);";
			
			// delete all the option set of this AutoId
			ps=connection.prepareStatement(delOpSetStr);
			ps.setInt(1, AutoID);
			ps.executeUpdate();

			System.out.println("[SUCCESS] "  + AutoName + " OptionSet Deleted");
			
			String delAutoStr = "DELETE FROM AutomobileDB.Automobile WHERE (AutoId=?);";
			
			// delete the automobile of this AutoId
			ps=connection.prepareStatement(delAutoStr);
			ps.setInt(1, AutoID);
			ps.executeUpdate();

			System.out.println("[SUCCESS] "  + AutoName + " Deleted");

			// close the connection
			ps.close();
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}

	}
	
	public void updateAutoBasePrice(String AutoName, float newBasePrice) {
		Connection connection = null;

		try {
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			if (connection != null) {
				System.out.println("[SUCCESS] Connected to Database");
			}

			String updAutoPriStr = "UPDATE AutomobileDB.Automobile SET BasePrice =? WHERE (AutoName=?);";
			
			// update the base price
			PreparedStatement ps = connection.prepareStatement(updAutoPriStr);
			ps.setFloat(1,newBasePrice);
			ps.setString(2, AutoName);
			ps.executeUpdate();

			System.out.println("[SUCCESS] " + AutoName + " BasePrice Updated");

			// close the connection
			ps.close();
			connection.close();
		} catch (SQLException e) {
			System.out.println("[ERROR]Automobile: " + AutoName + " not in the database");
		}
	}
}
