/**
 * 
 */
package model;

import java.io.Serializable;
import java.util.ArrayList;

import exception.AutoException;
import exception.CustomExceptionEnumerator;
import model.OptionSet.Option;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the class for Automobile which stores Optionsets
 */
public class Automobile implements Serializable{

	// instance variables
	private String name;
	private String make;
	private String model;
	private float basePrice;
	private ArrayList<OptionSet> optionSets;
	private ArrayList<Option> choice;
	
	// static variables
	private static final long serialVersionUID = 1798260372953772151L;
	
	// constructor
	public Automobile(){
		
	}
	
	/**
	 * New Constructor for Project1 Unit2
	 * @param make of the car
	 * @param model of the car
	 * @param baseprice of the car
	 */
	public Automobile(String make, String model, float baseprice){
		this.name = make + model;
		this.model = model;
		this.make = make;
		this.basePrice = baseprice;
		this.optionSets = new ArrayList<OptionSet>();
		this.choice = new ArrayList<Option>();
	}
	
	// getter/setter
	public String getName(){
		return name;
	}
	
	public void setName(String n){
		name = n;
	}
	
	public String getMake(){
		return make;
	}
	
	public void setMake(String make){
		this.make = make;
	}
	
	public String getModel(){
		return model;
	}
	
	public void setModel(String model){
		this.model = model;
	}
	
	public float getBasePrice(){
		return basePrice;
	}
	
	public void setBasePrice(float bp){
		basePrice = bp;
	}
	
	
	// OptionSet part
	public int getOptionSetSize(){
		return optionSets.size();
	}
	
	// get option set name
	// for thread testing
	public String getOptionSetName(String optionSetName) throws AutoException {
		if (getOptionSet(optionSetName) == null) {
			throw new AutoException(CustomExceptionEnumerator.OptionSetNotFound);
		}
		else{
			return getOptionSet(optionSetName).getName();
		}
		
	}
	
	// get option set
	protected OptionSet getOptionSet(String name){
		for (OptionSet opset : optionSets) {
			if (opset.getName().equals(name)) {
				return opset;
			}
		}
		return null;
	}
	
	// set option set
	public void setOptionSet(String name) {
		optionSets.add(new OptionSet(name));
	}
	
	// delete option set
	public void deleteOptionSet(String name) {
		for (OptionSet opset : optionSets) {
			if (opset.getName().equals(name)) {
				optionSets.remove(opset);
				return;
			}
		}
	}
	
	// update option set name	
	public void updateOptionSetName(String name, String newName) throws AutoException {
		boolean optionSetFound = false;
		for (OptionSet opset : optionSets) {
			if (opset.getName().equals(name)) {
				opset.setName(newName);
				optionSetFound = true;
				return;

			}
		}
		if (!optionSetFound) {
			throw new AutoException(CustomExceptionEnumerator.OptionSetNotFound);
		}
	}
	
	// Option Part
	protected Option getOption(String setName, String optionName) {
		if (getOptionSet(setName) != null) {
			return getOptionSet(setName).getOption(optionName);
		}
		return null;
	}
	
	// get option price
	public float getOptionPrice(String setName, String optionName) {
		if (getOptionSet(setName) != null) {
			return getOption(setName, optionName).getPrice();
		}
		return 0;
	}

	// set option 
	public void setOption(String setName, String optionName, float price) {
		if (getOptionSet(setName) != null) {
			getOptionSet(setName).setOption(optionName, price);
		}
	}

	// delete option
	public void deleteOption(String setName, String optionName) {
		if (getOptionSet(setName) != null) {
			getOptionSet(setName).deleteOption(optionName);
		}
	}

	// update option price
	public void updateOptionPrice(String setName, String optionName, float price) throws AutoException{
		if (getOptionSet(setName) != null) {
			getOptionSet(setName).updateOptionPrice(optionName, price);
		} else {
			throw new AutoException(CustomExceptionEnumerator.OptionSetNotFound);
		}	
	}
	
	// Choice part
	// get option choice
	public String getOptionChoice(String setName) {
		return getOptionSet(setName).getChoiceName();
	}
	
	// set option choice
	public void setOptionChoice(String setName, String optionName) {
		OptionSet opset = getOptionSet(setName);
		choice.add(opset.getOption(optionName));
		opset.setChoice(optionName);
	}
	
	// get option price
	public float getOptionChoicePrice(String setName) {
		return getOptionSet(setName).getChoicePrice();
	}
	
	// get total price
	public float getTotalPrice() {
		float sum = basePrice;
		for (Option op : choice) {
			sum += op.getPrice();
		}
		return sum;
	}
	
	// instance methods
	public void printBasicInfo(){
		System.out.println(getName());
		System.out.println("Base Price :$"
				+ String.format("%.2f", getBasePrice()));
		System.out.println();
	}
	
	public void printAllOptionSet() {
		for (OptionSet opset : optionSets) {
			System.out.println(opset.getName() + ":");
			opset.printAllOptions();
			System.out.println();
		}
	}
	
	public void printChoice() {
		for (Option op : choice) {
			System.out.println("Option : " + op.getName() + "Price : "
					+ op.getPrice());
		}
	}
	
	public void printTotalPrice() {
		System.out.println("Total price : " + getTotalPrice());
	}
	
	public void print() {
		this.printBasicInfo();
		this.printAllOptionSet();
	}
	
	// static methods
}
