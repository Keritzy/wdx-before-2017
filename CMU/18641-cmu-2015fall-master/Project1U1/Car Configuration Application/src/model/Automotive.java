/**
 * 
 */
package model;

import java.io.Serializable;

import model.OptionSet.Option;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 */
public class Automotive implements Serializable{

	// instance variables
	private String name;
	private float basePrice;
	private OptionSet[] opset;
	
	// static variables
	private static final long serialVersionUID = 1798260372953772151L;
	
	// constructor
	public Automotive(){
		
	}
	
	public Automotive(String n, float bp, int size){
		this.name = n;
		basePrice = bp;
		opset = new OptionSet[size];
		for (int i = 0; i < opset.length; i++){
			opset[i] = new OptionSet(); 
		}
	}
	
	// getter/setter
	public String getName(){
		return name;
	}
	
	public void setName(String n){
		name = n;
	}
	
	public float getBasePrice(){
		return basePrice;
	}
	
	public void setBasePrice(float bp){
		basePrice = bp;
	}
	
	
	// OptionSet part
	public int getOptionSetSize(){
		return opset.length;
	}
	
	// get option set
	protected OptionSet getOptionSet(int index){
		if (index < opset.length && index >= 0) {
			if (opset[index] != null) {
				return opset[index];
			}
		}
		return null;
	}
	
	// find option set
	protected OptionSet findOptionSet(String name) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(name)) {
					return opset[i];
				}
			}
		}
		return null;
	}
	
	// set option set
	public void setOptionSet(int index, String name, int size) {
		opset[index] = new OptionSet(name, size);
	}
	
	// find the first empty option in the set and fill the information
	public void setOptionSet(String name, int size) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] == null) {
				opset[i] = new OptionSet(name, size);
				return;
			}
		}
	}
	
	// delete option set
	public void deleteOptionSet(int index) {
		if (index < opset.length && index >= 0) {
			if (opset[index] != null) {
				opset[index] = null;
			}
		}
	}
	
	public void deleteOptionSet(String name) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(name)) {
					opset[i] = null;
				}
			}
		}
	}
	
	// update option set
	public void updateOptionSet(int index, String name, int size) {
		if (index < opset.length && index >= 0) {
			opset[index] = new OptionSet(name, size);
		}
	}
	
	public void updateOptionSet(String name, int size) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(name)) {
					opset[i] = new OptionSet(name, size);
					return;
				}
			}
		}
	}
	
	// update option set name
	public void updateOptionSetName(int index, String newName) {
		if (index < opset.length && index >= 0) {
			if (opset[index] != null) {
				opset[index].setName(newName);
			}
		}
	}
	
	public void updateOptionSetName(String name, String newName) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(name)) {
					opset[i].setName(newName);
					return;
				}
			}
		}
	}
	
	// Option Part
	// get option
	protected Option findOption(String setName, String optionName) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(setName)) {
					return opset[i].getOption(optionName);
				}
			}
		}
		return null;
	}
	
	protected Option getOption(int setIndex, String optionName) {
		if (setIndex < opset.length && setIndex >= 0) {
			if (opset[setIndex] != null) {
				return opset[setIndex].getOption(optionName);
			}
		}
		return null;
	}
	
	// get option price
	public float getOptionPrice(String setName, String optionName) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(setName)) {
					return opset[i].getOption(optionName).getPrice();
				}
			}
		}
		return 0;
	}

	public float getOptionPrice(int setIndex, String optionName) {
		if (setIndex < opset.length && setIndex >= 0) {
			if (opset[setIndex] != null) {
				return opset[setIndex].getOption(optionName).getPrice();
			}
		}
		return 0;
	}


	// set option 
	public void setOption(String setName, String optionName, float price) {
		if (findOptionSet(setName) != null) {
			findOptionSet(setName).setOption(optionName, price);
		}

	}

	public void setOption(int setIndex, int optionIndex, String optionName,
			float price) {
		if (getOptionSet(setIndex) != null) {
			opset[setIndex].setOption(optionIndex, optionName, price);
		}
	}

	// delete option
	public void deleteOption(String setName, String optionName) {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				if (opset[i].getName().equals(setName)) {
					opset[i].deleteOption(optionName);
				}
			}
		}
	}

	public void deleteOption(int setIndex, String optionName) {
		if (setIndex < opset.length && setIndex >= 0) {
			if (opset[setIndex] != null) {
				opset[setIndex].deleteOption(optionName);
			}
		}
	}

	// update option name
	public void updateOptionName(String setName, String optionName,
			String newOptionName) {
		if (findOptionSet(setName) != null) {
			findOptionSet(setName).updateOptionName(optionName, newOptionName);
		}
	}

	public void updateOptionName(String setName, int optionIndex,
			String newOptionName) {
		if (findOptionSet(setName) != null) {
			findOptionSet(setName).updateOptionName(optionIndex, newOptionName);
		}
	}

	// update option price
	public void updateOptionPrice(String setName, String optionName, float price) {
		if (findOptionSet(setName) != null) {
			findOptionSet(setName).updateOptionPrice(optionName, price);
		}
	}

	public void updateOptionPrice(int setIndex, String optionName, float price) {
		if (getOptionSet(setIndex) != null) {
			getOptionSet(setIndex).updateOptionPrice(optionName, price);
		}
	}
	
	// instance methods
	public void printBasicInfo(){
		System.out.println(getName());
		System.out.println("Base Price :$"
				+ String.format("%.2f", getBasePrice()));
		System.out.println();
	}
	
	public void printAllOptionSet() {
		for (int i = 0; i < opset.length; i++) {
			if (opset[i] != null) {
				System.out.println("[" + opset[i].getName() + "]");
				opset[i].printAllOptions();
				System.out.println();
			}
			else{
				System.out.println("Option Set Not Exist or Deleted");
				System.out.println();
			}
			
		}
	}
	
	public void print() {
		this.printBasicInfo();
		this.printAllOptionSet();
	}
	
	// static methods
}
