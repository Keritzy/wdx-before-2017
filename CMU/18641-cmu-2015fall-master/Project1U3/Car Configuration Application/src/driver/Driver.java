/**
 * 
 */
package driver;

import adapter.BuildAuto;
import exception.AutoException;
import model.Automobile;
import scale.EditOption.EditOptionEnumerator;
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
		System.out.println("             Test Thread");
		System.out.println("- - - - - - - - - - - - - - - - - - -");
		
		String[][] UpdateSetNameCases = {
				{"Focus Wagon ZTW","Transmission","Thread_Transmission"},
				{"Focus Wagon ZTW","Color","Thread_Color"},
				{"Focus Wagon ZTW","Power Moonroof","Thread_Power Moonroof"}
		};
		
		String[][] UpdateOptionPriceCases = {
				{"Focus Wagon ZTW","Transmission","Automatic","100"},
				{"Focus Wagon ZTW","Power Moonroof","Selected","111"},
				{"Focus Wagon ZTW","Brakes/Traction Control","Standard","123"}
		};

		for (int i = 0; i < UpdateSetNameCases.length; i++){
			autoBuilder.edit(EditOptionEnumerator.EditOptionSetName,
					UpdateSetNameCases[i]);
		}
		
		for (int i = 0; i < UpdateOptionPriceCases.length; i++){
			autoBuilder.edit(EditOptionEnumerator.EditOptionPrice,
					UpdateOptionPriceCases[i]);
		}
		
	}

}
