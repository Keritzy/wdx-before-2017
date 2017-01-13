/**
 * 
 */
package dawang.assignment2.model;

import dawang.assignment2.exception.IncorrectScoreNumberException;
import dawang.assignment2.prototype.IPrinter;
import dawang.assignment2.prototype.People;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This class inherits the abstract class People and implement the IPrinter
 * interface for printing the Information. It will also handle several number
 * checking tasks to make sure the inputs are valid.
 */
public class Student extends People implements IPrinter{
	private static final int QUIZ_COUNT = 5;
	private int SID;
	private int scores[] = new int[QUIZ_COUNT];
	
	// getter and setter
	public int getSID(){
		return SID;
	}
	
	public void setSID(int sid){
		SID = sid;
	}
	
	public int[] getScore(){
		return scores;
	}
	
	// Exception handling
	// If input > 5 scores, use first 5
	// If input < 5 scores, the rests are 0
	public void setScore(int[] scores){
		try {
			if (scores.length != this.scores.length){
				throw new IncorrectScoreNumberException("SID: " 
						+ String.format("%04d", SID));
			}
			this.scores = scores;
		}
		catch (IncorrectScoreNumberException s){
			System.out.println("Incorrect Score Number Exception - " 
					+ s.toString());
			if (scores.length < QUIZ_COUNT){
				for (int i = 0; i < scores.length; ++i){
					this.scores[i] = scores[i];
				}
			}
			else {
				for (int i = 0; i < QUIZ_COUNT; ++i){
					this.scores[i] = scores[i];
				}
			}
		}
		
	}
	
	public void printInfo(){
		System.out.print(String.format("%04d", SID));

		for (int i = 0; i < QUIZ_COUNT; ++i) {
			System.out.print("\t" + String.format("%03d", scores[i]));
		}
		System.out.println();
	}
}
