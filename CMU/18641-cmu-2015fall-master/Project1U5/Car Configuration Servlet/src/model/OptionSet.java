/**
 * 
 */
package model;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.LinkedHashMap;
/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the class for different options. Implements the Serializable 
 * interface so as to be save and load correctly.
 */
public class OptionSet implements Serializable{
	// instance variables
	private ArrayList<Option> options;
	private String name;
	private Option choiceOption;
	
	// static variables
	private static final long serialVersionUID = 124656345542354L;
	
	// constructor
	protected OptionSet (String name){
		this.name = name;
		options = new ArrayList<Option>();
	}
	
	protected OptionSet(){
		
	}
	
	// getter/setter
	
	protected LinkedHashMap<String, Float> getAllOptionLHM() {
		LinkedHashMap<String, Float> optionsetmap = new LinkedHashMap<String,Float>();
		for(Option op : options){
			optionsetmap.put(op.getName(), op.getPrice());
		}
		return optionsetmap;
	}
	
	// above is the method for project 1 unit 5
	
	protected int getOptionListSize() {
		return options.size();
	}
	
	protected void setChoice(int index) {
		choiceOption = getOption(index);
	}
	
	protected Option getOption(int index){
		return options.get(index);
	}
	
	protected String getOptionSetName() {
		return name;
	}
	
	// above is the method updated for p1u4
	
	protected String getName(){
		return name;
	}
	
	protected void setName(String n){
		name = n;
	}
	
	protected int getOptionSize(){
		return options.size();
	}
	
	// get option 
	protected Option getOption(String name){
		for (Option op : options) {
			if (op.getName().equals(name)) {
				return op;
			}
		}
		return null;
	}
	
	// set option 
	
	protected void setOption(String name, float price) {
		options.add(new Option(name, price));
	}
	
	// delete
	protected void deleteOption(String name) {
		for (Option op : options) {
			if (op.getName().equals(name)) {
				options.remove(op);
				return;
			}
		}
	}
	
	// update 
	protected void updateOptionPrice(String name,float price) {
		if (getOption(name) == null) {
			return;
		} else {
			getOption(name).setPrice(price);
		}
	}

	
	protected void updateOptionName(String name,String newName) {
		if (getOption(name) == null) {
			return;
		} else {
			getOption(name).setName(newName);
		}
	}
	
	// choice part
	protected void setChoice(String optionName) {
		choiceOption = getOption(optionName);
	}
	
	protected String getChoiceName() {
		return choiceOption.getName();
	}
	
	protected float getChoicePrice() {
		return choiceOption.getPrice();
	}
	
	// instance methods
	protected void printAllOptions(){
		int i = 0;
		for (Option op : options) {
			System.out.println(i + " " + op.getName() + ":Price "
					+ String.format("%.2f", op.getPrice()));
			i++;
		}
	}
	
	// static methods
	protected class Option implements Serializable{
		
		// instance variables
		private String name;
		private float price;
		
		// static variables
		private static final long serialVersionUID = 54643216846842L;
		
		// constructor
		protected Option(){
			
		}
		
		protected Option(String n, float p){
			name = n;
			price = p;
		}
		
		// getter/setter
		protected void setName(String n){
			name = n;
		}
		
		protected String getName(){
			return name;
		}
		
		protected void setPrice(float p){
			price = p;
		}
		
		protected float getPrice(){
			return price;
		}
		
		// instance methods
		// static methods
		
	}
}
