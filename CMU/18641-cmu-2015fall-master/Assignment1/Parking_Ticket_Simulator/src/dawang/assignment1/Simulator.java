/**
 * 
 */
package dawang.assignment1;

/**
 * @author Da Wang
 * @andrew_id dawang
 */
public class Simulator {
	// 5 test cases
	private static Testcase[] testCases = {
			new Testcase("Within the purchased parking time",
					"BMW","I8","WHITE","20150901",50,
					"Mariah Carey","0001",60),
			new Testcase("The same as the purchased parking time",
					"INFINITI","Q8","RED","20150902",60,
					"Nicky","0002",60),
			new Testcase("Beyond the purchased parking time(1min)",
					"BENZ","S550","BLACK","20150903",61,
					"Harriet Sugar","0003",60),
			new Testcase("Ticketing within 1 hour",
					"AUDI","RS7","SILVER","20150904",120,
					"Kathy Griffin","0004",60),
			new Testcase("Ticketing beyond 1 hour",
					"TESLA","MODEL S","BLACK","20150905",121,
					"Tesla Domo","0005",60)	
	};
	
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		System.out.println("Car Parking Simulator");
		
		
		// go over each test case
		for (int i = 0; i < testCases.length; i++){
			System.out.println("#################################");
			System.out.println("Simulator #" + (i+1) + ": " 
					+ testCases[i].getDescription());
			System.out.println("The Car has parked for " 
					+ testCases[i].getParkedMinutes() + " minutes");
			System.out.println("Purchased parking time is "
					+ testCases[i].getPurchasedTime() + " minutes");
			System.out.println("- - - - - - - - - - -");
			System.out.println("Police Office Checking");
			testCases[i].checkCars();
		}
		
		

	}

}
