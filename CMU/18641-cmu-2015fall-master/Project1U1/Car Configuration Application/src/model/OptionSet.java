/**
 * 
 */
package model;

import java.io.Serializable;

import javax.swing.text.html.Option;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the class for different options. Implements the Serializable 
 * interface so as to be save and load correctly.
 */
public class OptionSet implements Serializable{
	// instance variables
	private Option opt[];
	private String name;
	
	// static variables
	private static final long serialVersionUID = 124656345542354L;
	
	// constructor
	protected OptionSet (String n, int size){
		opt = new Option[size];
		for (int i = 0; i < opt.length; i++){
			opt[i] = new Option();
		}
		name = n;
	}
	
	protected OptionSet(){
		
	}
	
	// getter/setter
	protected String getName(){
		return name;
	}
	
	protected void setName(String n){
		name = n;
	}
	
	protected int getOptionSize(){
		return opt.length;
	}
	
	// get option 
	protected Option getOption(int index){
		if (index < opt.length && index >= 0){
			if (opt[index] != null){
				return opt[index];
			}
		}
		return null;
	}
	
	protected Option getOption(String name){
		for (int i = 0; i < opt.length; i++){
			if (opt[i] != null){
				if (opt[i].getName().equals(name)){
					return opt[i];
				}
			}
		}
		return null;
	}
	
	// set option 
	protected void setOption(int index, String name, float price){
		if (index < opt.length && index >= 0){
			opt[index] = new Option(name, price);
		}
	}
	
	protected void setOption(String name, float price) {
		for (int i = 0; i < opt.length; i++) {
			if (opt[i] == null) {
				opt[i] = new Option(name, price);
				return;
			}
		}
	}
	
	// delete
	protected void deleteOption(String name) {
		for (int i = 0; i < opt.length; i++) {
			if (opt[i] != null) {
				if (opt[i].getName().equals(name)) {
					opt[i] = null;
				}
			}
		}
	}
	protected void deleteOption(int index) {
		if (index < opt.length && index >= 0) {
			if (opt[index] != null) {
				opt[index] = null;
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

	protected void updateOptionPrice(int index,float price) {
		if (getOption(index) == null) {
			return;
		} else {
			getOption(index).setPrice(price);
		}
	}
	protected void updateOptionName(String name,String newName) {
		if (getOption(name) == null) {
			return;
		} else {
			getOption(name).setName(newName);
		}
	}

	protected void updateOptionName(int index,String newName) {
		if (getOption(index) == null) {
			return;
		} else {
			getOption(index).setName(newName);
		}
	}
	
	// instance methods
	protected void printAllOptions(){
		for (int i = 0; i < opt.length; i++) {
			if (opt[i] != null) {
				System.out.println(opt[i].getName() + ": $" 
						+ String.format("%.2f", opt[i].getPrice()));
			}
			else{
				System.out.println("Option Not Exist or Deleted");
			}
			
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
