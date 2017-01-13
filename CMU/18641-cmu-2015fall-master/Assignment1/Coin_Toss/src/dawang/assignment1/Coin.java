package dawang.assignment1;

/**
 * 
 * @author Da Wang
 * @andrew_id dawang
 */

public class Coin {
	
	private String sideUp;
	
	public Coin(){
		toss();
	}
	
	/*
	 * toss()
	 * If the random value is less than 0.5
	 * regarded as "heads", else "tails"
	 */
	public void toss(){
		if (Math.random() < 0.5){
			sideUp = "heads";
		}
		else{
			sideUp = "tails";
		}
	}
	
	public String getSideUp(){
		return sideUp;
	}
}
