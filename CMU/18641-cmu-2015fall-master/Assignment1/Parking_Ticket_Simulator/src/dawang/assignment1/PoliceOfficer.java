/**
 * 
 */
package dawang.assignment1;

/**
 * @author Da Wang
 * @andrew_id dawang
 */
public class PoliceOfficer {
	private String policeName;
	private String badgeNumber;
	
	public PoliceOfficer(String name,String badgeNumber){
		this.policeName = name;
		this.badgeNumber = badgeNumber;
	}
	
	// getter and setter
	public String getName(){
		return policeName;
	}
	
	public void setName(String name){
		this.policeName = name;
	}
	
	public String getBadgeNumber(){
		return badgeNumber;
	}
	
	public void setBadgeNumber(String badgeNumber){
		this.badgeNumber = badgeNumber;
	}
	
	public void checkParkingExpired(ParkedCar parkedCar, 
			ParkingMeter parkingMeter){
		if (parkedCar.getParkedMinutes() <= parkingMeter.getPurchasedTime()) {
			System.out.println("Legal Parking.");
		}
		else{
			System.out.println("Illegal Parking.");
			issueTicket(parkedCar, parkingMeter);
		}
	}
	
	private void issueTicket(ParkedCar parkedCar, ParkingMeter parkingMeter){
		ParkingTicket parkingTicket = new ParkingTicket(parkedCar, 
				this, parkingMeter);
		System.out.println("- - - - - - - - - - - - - -");
		System.out.println("Illegal Parking Fine Ticket");
		parkingTicket.reportCarInfo();
		parkingTicket.reportFine();
		parkingTicket.reportOfficerInfo();
	}
}
