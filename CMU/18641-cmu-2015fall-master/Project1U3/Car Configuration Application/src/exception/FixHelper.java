/**
 * 
 */
package exception;

import util.FileIO;
import model.Automobile;
import model.CarShop;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * Helper class to delegate fixes for each method
 */
public class FixHelper {
	// default input car model : Focus (Focus.txt)
	private static final String DEFAULT_CAR_MODEL_FILE = "default_car_model.txt";
	// default serial car model : Focus
	private static final String DEFAULT_SAVED_MODEL_FILE = "default_saved_model.ser";

	// Fix File not found exception by loading default car model
	public void fixFileNotFound(CarShop automobileList){
		Automobile auto = null;
		try {
			System.out.println("Invalid filename, fix it by loading default file");
			auto = new FileIO().buildAutoObject(DEFAULT_CAR_MODEL_FILE);			
		} catch (AutoException e) {
			System.out.println("Default Model File Not Find Exception: "
					+ e.toString());
		}
		automobileList.setAutomobile(auto);
	}

	// fix missing base price problem by set it to 0
	public String fixFileNoBasePrice() {
		return "0";
	}

	// fix missing option price problem by set it to 0
	public String[] fixFileNoOptionPrice(String[] input) {
		String[] output = new String[2];
		output[0] = input[0];
		output[1] = "0";
		return output;
	}

	// Fix File not found exception by loading default car model and set it into
	// input fleet
	public void fixSerialCarFileNotFound(CarShop automobileList){
		Automobile auto = null;
		try {
			auto = new FileIO().deserializeAuto(DEFAULT_SAVED_MODEL_FILE);
		} catch (AutoException e) {
			System.out.println("Default Model File Not Find Exception: "
					+ e.toString());
		}
		automobileList.setAutomobile(auto);
	}				
}
