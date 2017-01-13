/**
 * 
 */
package dawang.assignment1;

/**
 * @author Da Wang
 * @andrew_id dawang
 */
public class Simulation {
	private static int[] testcases = {20, 10, 0, -1};
	
	public static void main(String[] args){
		
		
		for (int i = 0; i < testcases.length; ++i){
			System.out.println("Testcase #" + (i+1) + " with " + testcases[i] + " times toss");
			tossSimulator(testcases[i]);
			System.out.println("-----------------");
		}
		
	}
	
	public static void tossSimulator(int times){
		System.out.println("**** Toss Simulator ****");
		System.out.println("Toss " + times + " Times");
		
		int headsCount = 0;
		int tailsCount = 0;
		
		String curSideUp;
		
		// check if the times is valid
		if (times <= 0){
			System.out.println("No toss or Illegal toss times!");
			return;
		}
		
		// if valid, begin tossing
		Coin iCoin = new Coin();
		System.out.println("Initially facing up: " + iCoin.getSideUp());
		
		for (int i = 1; i <= times; ++i){
			iCoin.toss();
			curSideUp = iCoin.getSideUp();
			System.out.println("Toss #" + i + " Facing Up: " + curSideUp);
			
			if (curSideUp.equals("heads")){
				headsCount++;
			}
			else{
				tailsCount++;
			}
		}
		
		// print the result
		System.out.println(headsCount + " times tossing HEADS");
		System.out.println(tailsCount + " times tossing TAILS");
		System.out.println("**** Simulation Done ****");
		
	}
}
