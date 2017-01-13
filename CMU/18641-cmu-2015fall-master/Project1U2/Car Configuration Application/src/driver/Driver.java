/**
 * 
 */
package driver;

import adapter.BuildAuto;
import exception.AutoException;
import model.Automobile;
import util.FileIO;

/**
 * @author Da Wang
 * @andrew_id dawang
 * This is the class for testing all the design and has the main function
 */
public class Driver {

	/**
	 * @param args
	 * @throws AutoException 
	 */
	public static void main(String[] args) throws AutoException {
		System.out.println("    Car Configuration Application");
		System.out.println("-------------------------------------");
		BuildAuto autoBuilder = new BuildAuto();
		autoBuilder.buildAuto("Focus.txt");
		autoBuilder.printAuto("Focus Wagon ZTW");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("             Test Update");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		autoBuilder.updateOptionSetName("Focus Wagon ZTW", "Transmission",
				"#Updated Transmission#");
		autoBuilder.updateOptionPrice("Focus Wagon ZTW", "#Updated Transmission#",
				"Automatic", (float)888);
		autoBuilder.printAuto("Focus Wagon ZTW");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("             Test Choice");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		Automobile auto = new FileIO().buildAutoObject("Focus.txt");
		auto.setOptionChoice("Color", "Cloud 9 White Clearcoat");
		auto.setOptionChoice("Transmission", "Standard");
		auto.setOptionChoice("Brakes/Traction Control", "ABS with Advance Trac");
		auto.setOptionChoice("Side Impact Airbags", "Selected");
		auto.setOptionChoice("Power Moonroof", "Selected");
		System.out.println("Base price is : " + auto.getBasePrice());
		auto.printChoice();
		auto.printTotalPrice();
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("             Test Delete");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		autoBuilder.deleteAuto("Focus Wagon ZTW");
		autoBuilder.printAuto("Focus Wagon ZTW");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("     Test Exception and Auto Fix");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		autoBuilder.buildAuto("Focu.txt");
		autoBuilder.printAuto("Focus Wagon ZTW");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("           Test More Cars");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		autoBuilder.buildAuto("ModelS.txt");
		autoBuilder.printAuto("ModelS Tesla");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		autoBuilder.printAuto("Focus Wagon ZTW");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		System.out.println("           Test Done");
	}

}
