/**
 * 
 */
package dawang.assignment1;

/**
 * @author Da Wang
 * @andrew_id dawang
 */
public class ParkingTicket {
	private static final double BASE_FINE = 25.00;
	private static final double EXTRA_FINE_PER_HOUR = 10.00;
	
	private ParkedCar parkedCar;
	private PoliceOfficer policeOfficer;
	private ParkingMeter parkingMeter;
	
	public ParkingTicket(ParkedCar parkedCar, PoliceOfficer policeOfficer,
			ParkingMeter parkingMeter){
		this.parkedCar = parkedCar;
		this.policeOfficer = policeOfficer;
		this.parkingMeter = parkingMeter;
	}
	
	public void reportCarInfo(){
		System.out.println("- - - - - - - - - - -");
		System.out.println("Car Infomation");
		System.out.println("- - - - - - - - - - -");
		String info = "Make: " + parkedCar.getMake() + "\nModel: "
				+ parkedCar.getModel() + "\nColor: " + parkedCar.getColor()
				+ "\nLicense Number: " + parkedCar.getLicenseNumber();
		System.out.println(info);
	}
	
	public void reportFine() {
		System.out.println("- - - - - - - - - - -");
		System.out.println("Fine : $" + getFine());
	}
	
	public void reportOfficerInfo() {
		System.out.println("- - - - - - - - - - -");
		System.out.println("Ticket Issued Officer");
		System.out.println("- - - - - - - - - - -");
		String info = "Name: " + policeOfficer.getName()
				+ "\nBadge Number: " + policeOfficer.getBadgeNumber();
		System.out.println(info);
	}
	
	private double getFine() {
		double fine = BASE_FINE;
		// if more than 1 hour, will be regarded as 2 hour
		if (parkedCar.getParkedMinutes() - parkingMeter.getPurchasedTime() > 60) {
			fine += EXTRA_FINE_PER_HOUR
					* ((parkedCar.getParkedMinutes() - parkingMeter
							.getPurchasedTime()) / 60 );
		}
		
		return fine;
	}
	
	
}
