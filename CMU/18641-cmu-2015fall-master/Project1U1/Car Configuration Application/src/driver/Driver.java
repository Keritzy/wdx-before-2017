/**
 * 
 */
package driver;

import model.Automotive;
import util.FileIO;

/**
 * @author Da Wang
 * @andrew_id dawang
 * This is the class for testing all the design and has the main function
 */
public class Driver {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		System.out.println("Car Configuration Application");
		System.out.println("-------------------------------------");
		String filename = "focus.txt";
		System.out.println("Loading " + filename);
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		FileIO io = new FileIO();
		Automotive ford = io.buildAutoObject(filename);
		ford.print();
		System.out.println("-------------------------------------");
		System.out.println("Serializating " + ford.getName());
		io.serializeAuto(ford);
		System.out.println("Serialization done, saved to auto.ser");
		System.out.println("-------------------------------------");
		System.out.println("Deserializating " + ford.getName());
		Automotive newFord = io.deserializeAuto("auto.ser");
		System.out.println("Deserialization done");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		newFord.print();
		System.out.println("-------------------------------------");
		System.out.println("Testing the setter and getter");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("Test getOptionSetSize()");
		System.out.println("Option set list size is " + ford.getOptionSetSize());
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("Test setOptionSet()");
		ford.setOptionSet("Color",0);
		ford.setOptionSet(1, "Test Set Method", 0);
		ford.printAllOptionSet();
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("Test deleteOptionSet()");
		ford.deleteOptionSet(4);
		ford.deleteOptionSet("Power Moonroof");
		ford.printAllOptionSet();
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		ford.updateOptionSet("Color",5);
		ford.updateOptionSet(2,"Side Impact Airbags",0);
		ford.updateOptionSetName(1, "Transmission");
		ford.updateOptionSetName("Power Moonroof", "This is just for testing");
		ford.printAllOptionSet();
		System.out.println("Test Success");

	}

}
