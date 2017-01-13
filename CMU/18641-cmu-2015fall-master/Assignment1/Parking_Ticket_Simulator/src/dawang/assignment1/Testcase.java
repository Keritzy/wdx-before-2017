/**
 * 
 */
package dawang.assignment1;

/**
 * @author dawang
 *
 */
public class Testcase {
	// Test Description
	private String testDescription;
	
	// Car Info
	private ParkedCar parkedCar;
	
	// Police Info
	private PoliceOfficer policeOfficer;
	
	// Parking Meter
	private ParkingMeter parkingMeter;
	
	public Testcase(String td, String make, String model,
			String color, String licenseNumber, int min,
			String name, String badge, int time){
		this.testDescription = td;
		parkedCar = new ParkedCar(make, model, color, licenseNumber, min);
		policeOfficer = new PoliceOfficer(name, badge);
		parkingMeter = new ParkingMeter(time);
	}
	
	public String getDescription(){
		return this.testDescription;
	}

	public int getParkedMinutes(){
		return parkedCar.getParkedMinutes();
	}
	
	public int getPurchasedTime(){
		return parkingMeter.getPurchasedTime();
	}
	
	public void checkCars(){
		policeOfficer.checkParkingExpired(parkedCar, parkingMeter);
	}
}
