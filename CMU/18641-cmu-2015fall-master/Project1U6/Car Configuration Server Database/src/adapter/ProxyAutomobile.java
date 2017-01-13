/**
 * 
 */
package adapter;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.Properties;

import exception.AutoException;
import exception.CustomExceptionEnumerator;
import exception.FixHelper;
import model.*;
import scale.EditOption;
import scale.EditOption.EditOptionEnumerator;
import util.DatabaseIO;
import util.FileIO;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This class will contain all the implementation of any method declared in the
 * interface
 */
public abstract class ProxyAutomobile {
	private static LinkedHashMap<String, Automobile> automobileList = 
			new LinkedHashMap<String, Automobile>();
	private static int threadID = 0;
	
	// Three distinct ID for database table
	private static int AutoID = 0; // record the current ID of car;
	private static int OptionSetID = 0; // record the current option set ID;
	private static int OptionID = 0; // record the current option ID;
	
	public void buildDBAuto(String filename){
		Automobile auto = null;
		try {
			auto = new FileIO().buildAutoObject(filename);
		} catch (AutoException autoException) {
			fix(autoException.getExceptionIndex());
		}
		
		// if already in the LHM, then return 
		if (automobileList.get(auto.getName()) != null) {
			return;
		} else {
			automobileList.put(auto.getName(), auto);
			
			// refresh the current ID for new input to make all ID in table distinct
			int[] newIDs = new DatabaseIO().addToDatabase(auto, AutoID, OptionSetID, OptionID);
			AutoID++;
			OptionSetID = newIDs[0];
			OptionID = newIDs[1];
		}
	}
	
	public void deleteDBAuto(String autoName){
		if (automobileList.get(autoName) != null) {
			automobileList.remove(autoName);
			new DatabaseIO().deleteAutoInDatabase(autoName);
			// if it also exist in LHM, delete it
		} else{
			System.out.println("[Error] " + autoName + " Not Found");
		}
	}
	
	public void updateDBAutoPrice(String autoName, float newPrice){
		Automobile auto = automobileList.get(autoName);
		if (auto != null) {
			auto.setBasePrice(newPrice);
			new DatabaseIO().updateAutoBasePrice(autoName, newPrice);
		}else{
			System.out.println("[Error] " + autoName + " Not Found");
		}
	}
	
	
	// above is for project 1 unit 6
	
	public void buildAuto(String filename) {
		try {
			Automobile auto = new FileIO().buildAutoObject(filename);
			automobileList.put(auto.getName(), auto);
		} catch (AutoException autoException) {
			fix(autoException.getExceptionIndex());
		}
	}

	public void buildAutoFromProperty(Properties properties) {
		Automobile auto = new FileIO().loadAutomobileProperty(properties);
		automobileList.put(auto.getName(), auto);
	}


	public ArrayList<String> getModelList() {
		ArrayList<String> autoNameList = new ArrayList<String>();
		for (String key : automobileList.keySet()) {
			autoNameList.add(key);
		}
		return autoNameList;
	}

	public void sendModel(ObjectOutputStream objectOutputStream, 
			String modelName) throws IOException {
		Automobile selectedAuto = automobileList.get(modelName);
		objectOutputStream.writeObject(selectedAuto);
	}
	
	
	
	/**
	 * Print the information of the car 
	 * @param name of the car
	 */
	public void printAuto(String name) {
		Automobile auto = automobileList.get(name);
		try {
			if (auto == null) {
				throw new AutoException(CustomExceptionEnumerator.CarModelNotFound);
			}
			auto.print();
		} catch (AutoException ae) {
			System.out.println("Error -- " + ae.toString());
		}

	}

	/**
	 * Delete the corresponding car 
	 * @param name of the car
	 */
	public void deleteAuto(String name) {

		Automobile auto = automobileList.get(name);
		try {
			if (auto == null) {
				throw new AutoException(CustomExceptionEnumerator.CarModelNotFound);
			}
			automobileList.remove(name);
		} catch (AutoException ae) {
			System.out.println("Error -- " + ae.toString());
		}

	}

	/**
	 * Update the OptionSet name of the car
	 * @param name of the car
	 * @param optionSetName of the Option set
	 * @param newName of the Option set
	 */
	public void updateOptionSetName(String name, String optionSetName,
			String newName) {
		Automobile auto = automobileList.get(name);
		try {
			auto.updateOptionSetName(optionSetName, newName);
		} catch (AutoException ae) {
			System.out.println("Error -- " + ae.toString());
		}

	}

	/**
	 * Update Option price
	 * @param name of car
	 * @param optionSetName of the option set
	 * @param option name
	 * @param newPrice of the option
	 */
	public void updateOptionPrice(String name, String optionSetName,
			String option, float newPrice) {
		Automobile auto = automobileList.get(name);
		try {
			auto.updateOptionPrice(optionSetName, option, newPrice);
		} catch (AutoException ae) {
			System.out.println("Error -- " + ae.toString());
		}
	}


	/**
	 * Serialize the car model
	 * @param name
	 */
	public void saveCarModel(String name) {
		Automobile auto = automobileList.get(name);
		try {
			if (auto == null) {
				throw new AutoException(
						CustomExceptionEnumerator.SerialCarFileNotFound);
			}
			new FileIO().serializeAuto(auto);
		} catch (AutoException ae) {
			System.out.println("Error -- " + ae.toString());
		}
	}

	/**
	 * Load the car model from the serialization file
	 * @param name
	 */
	public void loadCarModel(String name) throws AutoException {
		StringBuffer sb = new StringBuffer();
		sb.append(name);
		sb.append(".ser");

		Automobile auto = new FileIO().deserializeAuto(sb.toString());
		automobileList.put(auto.getName(), auto);

	}

	/**
	 * fix the exceptions
	 * @param errno index of exceptions
	 */
	public void fix(int errno) {
		FixHelper fixHelper = new FixHelper();
		
		/**
		 * FileNotFound(1),
		 * FileNoBasePrice(2),
		 * FileNoOptionPrice(3),
		 * CarModelNotFound(4),
		 * OptionSetNotFound(5),
		 * OptionNotFound(6),
		 * SerialCarFileNotFound(7);
		 */	
		switch (errno) {
		case 1:
			fixHelper.fixFileNotFound(automobileList);
			break;
		case 7:
			fixHelper.fixSerialCarFileNotFound(automobileList);
			break;
		default:
			break;
		}
	}
	
	/**
	 * edit API for Build Auto
	 * @param editOptionEnumerator of different type of editing
	 * @param args of the editing operation
	 */
	public void edit(EditOptionEnumerator editOptionEnumerator, String[] args) {
		Automobile auto = null;
		auto = automobileList.get(args[0]);// args 0 is model name
		EditOption edit = new EditOption(++threadID, auto, editOptionEnumerator,
				args);
		edit.start();
	}
}
