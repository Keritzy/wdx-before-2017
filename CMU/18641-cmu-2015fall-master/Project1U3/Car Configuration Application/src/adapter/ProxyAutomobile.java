/**
 * 
 */
package adapter;

import exception.AutoException;
import exception.CustomExceptionEnumerator;
import exception.FixHelper;
import model.*;
import scale.EditOption;
import scale.EditOption.EditOptionEnumerator;
import util.FileIO;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This class will contain all the implementation of any method declared in the
 * interface
 */
public abstract class ProxyAutomobile {
	private static CarShop automobileList = new CarShop();
	private static int threadID = 0;
	
	public void buildAuto(String filename) {
		try {
			automobileList.setAutomobile(filename);
		} catch (AutoException autoException) {
			fix(autoException.getExceptionIndex());
		}
	}

	/**
	 * Print the information of the car 
	 * @param name of the car
	 */
	public void printAuto(String name) {
		Automobile auto = automobileList.getAutomobile(name);
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

		Automobile auto = automobileList.getAutomobile(name);
		try {
			if (auto == null) {
				throw new AutoException(CustomExceptionEnumerator.CarModelNotFound);
			}
			automobileList.deleteAutomobile(name);
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
		try {
			automobileList.updateOptionSetName(name, optionSetName, newName);
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
		try {
			automobileList.updateOptionPrice(name, optionSetName, option, newPrice);
		} catch (AutoException ae) {
			System.out.println("Error -- " + ae.toString());
		}
	}


	/**
	 * Serialize the car model
	 * @param name
	 */
	public void saveCarModel(String name) {
		Automobile auto = automobileList.getAutomobile(name);
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
		automobileList.setAutomobile(auto);

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
		auto = automobileList.getAutomobile(args[0]);// args 0 is model name
		EditOption edit = new EditOption(++threadID, auto, editOptionEnumerator,
				args);
		edit.start();
	}
}
