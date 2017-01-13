/**
 * 
 */
package model;

import java.util.LinkedHashMap;

import exception.AutoException;
import exception.CustomExceptionEnumerator;
import util.FileIO;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the class for multiple instances of Automobile which will be stored
 * in a LinkedHashMap
 */
public class CarShop {
	private static LinkedHashMap<String, Automobile> automobileList;
	
	public CarShop() {
		automobileList = new LinkedHashMap<String, Automobile>();
	}
	
	// setter/getter
	public void setAutomobile(Automobile automobile){
		automobileList.put(automobile.getName(), automobile);
	}
	
	public void setAutomobile(String filename) throws AutoException{
		Automobile automobile = new FileIO().buildAutoObject(filename);
		automobileList.put(automobile.getName(), automobile);
	}
	
	public Automobile getAutomobile(String name){
		return automobileList.get(name);
	}
	
	public void updateOptionSetName(String name, String optionSetName,
			String newName) throws AutoException{
		Automobile automobile = automobileList.get(name);
		if (automobile != null) {
			automobile.updateOptionSetName(optionSetName, newName);
			automobileList.put(name, automobile);
		} else {
			throw new AutoException(CustomExceptionEnumerator.CarModelNotFound);
		}
	}
	
	public void updateOptionPrice(String name, String optionName,
			String option, float newPrice) throws AutoException {
		Automobile auto = automobileList.get(name);
		if (auto != null) {
			auto.updateOptionPrice(optionName, option, newPrice);
			automobileList.put(name, auto);
		} else {
			throw new AutoException(CustomExceptionEnumerator.CarModelNotFound);
		}
	}
	
	public void deleteAutomobile(String name){
		automobileList.remove(name);
	}
}
