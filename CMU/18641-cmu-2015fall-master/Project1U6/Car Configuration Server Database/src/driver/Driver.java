/**
 * 
 */
package driver;

import java.sql.*;

import adapter.BuildAuto;
import adapter.DatabaseAuto;
import util.DatabaseIO;

/** Class for testing all the functions
 * @author dawang
 * @andrewid dawang
 */
public class Driver {
	private static final String URL = "jdbc:mysql://localhost:3306";
	private static final String USER_NAME = "root";
	private static final String PASSWORD = "123456";
	
	/** Test different functions
	 * @param args
	 */
	public static void main(String[] args) {
		System.out.println("      Automobile to Database Test Program");
		System.out.println("-------------------------------------------------");
		
		System.out.println("     Create Database(with cleaning old data)");
		System.out.println("- - - - - - - - - - - - - - - - - - - - - - - - -");
		new DatabaseIO().createDatabase();
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		
		System.out.println("          Add Automobile to Database");
		System.out.println("- - - - - - - - - - - - - - - - - - - - - - - - -");
		DatabaseAuto autos = new BuildAuto();
		autos.buildDBAuto("Focus.txt");
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		autos.buildDBAuto("ModelS.txt");
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		autos.buildDBAuto("Toyota.txt");
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		System.out.println("         AutomobileDB.Automobile Table");
		showAutomobileTable();
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		System.out.println("          AutomobileDB.OptionSet Table");
		showOptionSetTable();
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		System.out.println("         AutomobileDB.AutoOption Table");
		showOptionTable();
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		
		
		System.out.println("         Delete Automobile from Database");
		System.out.println("- - - - - - - - - - - - - - - - - - - - - - - - -");
		autos.deleteDBAuto("Focus Wagon ZTW");
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		autos.deleteDBAuto("ModelS Tesla");
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		System.out.println("         AutomobileDB.Automobile Table");
		showAutomobileTable();
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		
		System.out.println("            Update Automobile Record");
		System.out.println("- - - - - - - - - - - - - - - - - - - - - - - - -");
		autos.updateDBAutoPrice("Prius Toyota", 99999);
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		System.out.println("         AutomobileDB.Automobile Table");
		showAutomobileTable();
		System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
		
		System.out.println("             Test Program Completed");
		System.out.println("-------------------------------------------------");
	}

	
	public static void showAutomobileTable(){
		Connection connection = null;
		Statement statement = null;
		ResultSet rs;
		try {			
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);

			String getAutoTStr = "SELECT * FROM AutomobileDB.Automobile;";
			
			statement = connection.createStatement();
			rs = statement.executeQuery(getAutoTStr);
			
			System.out.format("%8s%18s%18s\n", "AutoId", "AutoName", "BasePrice");
			System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
			while (rs.next()) {
				System.out.format("%5s%22s%14s\n", rs.getString("AutoId"), rs.getString("AutoName"),
						rs.getString("BasePrice"));
			}
			
			// close
			statement.close();
			rs.close();
			connection.close();
			
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
	}
	
	public static void showOptionSetTable(){
		Connection connection = null;
		Statement statement = null;

		ResultSet rs;
		try {
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			statement = connection.createStatement();
			
			String getOpSetStr = "SELECT * FROM AutomobileDB.OptionSet;";
			
			statement = connection.createStatement();
			rs = statement.executeQuery(getOpSetStr);
			
			System.out.format("%8s%20s%18s\n", "OpSetID", "OpSetName", "AutoId");
			System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
			while (rs.next()) {
				System.out.format("%4s%28s%12s\n", rs.getString("OptionSetId"), rs.getString("OptionSetName"),
						rs.getString("AutoId"));

			}
			
			// close
			statement.close();
			rs.close();
			connection.close();

		} catch (SQLException e) {
			e.printStackTrace();
		} 
	}
	
	public static void showOptionTable(){
		Connection connection = null;
		Statement statement = null;
		ResultSet rs;
		try {
			connection = DriverManager.getConnection(URL, USER_NAME, PASSWORD);
			statement = connection.createStatement();
			
			String getOpStr = "SELECT * FROM AutomobileDB.AutoOption;";
				
			statement = connection.createStatement();
			rs = statement.executeQuery(getOpStr);
			
			// print title
			System.out.format("%-5s%15s%26s%9s%9s\n", "OpID", "OpName", "OpPrice","OpSetID","AutoID");
			System.out.println("-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -");
			
			// select each line
			while (rs.next()) {
				System.out.format("%-5s%-37s%-9s%-9s%2s\n", rs.getString("OptionId"), rs.getString("OptionName"),
						rs.getString("OptionPrice"), rs.getString("OptionSetId"), rs.getString("AutoID"));
			}
			
			// close
			statement.close();
			rs.close();
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		} 
		
	}
}
