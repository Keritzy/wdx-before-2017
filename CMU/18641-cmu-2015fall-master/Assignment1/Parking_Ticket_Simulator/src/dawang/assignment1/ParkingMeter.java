/**
 * 
 */
package dawang.assignment1;

/**
 * @author Da Wang
 * @andrew_id dawang
 */
public class ParkingMeter {
	private int purchasedMinutes;
	
	public ParkingMeter(){
		purchasedMinutes = 0;
	}
	
	public ParkingMeter(int min){
		if (min >= 0){
			purchasedMinutes = min;
		}
		else{
			purchasedMinutes = 0;
		}
	}
	
	// getter and setter
	
	public int getPurchasedTime(){
		return purchasedMinutes;
	}
	
	public void setPurchasedTime(int min){
		if (min >= 0) {
			purchasedMinutes = min;
		}
		else{
			purchasedMinutes = 0;
		}
	}
}
