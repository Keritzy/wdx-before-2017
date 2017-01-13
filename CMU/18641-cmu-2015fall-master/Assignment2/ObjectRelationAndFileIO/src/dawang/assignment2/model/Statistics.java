/**
 * 
 */
package dawang.assignment2.model;

import dawang.assignment2.prototype.IPrinter;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This class implements the IPrinter interface for printing the information and
 * use three arrays to store the statistics of the quiz.
 */
public class Statistics implements IPrinter{
	private static final int QUIZ_COUNT = 5;
	private int[] lowscores = new int[QUIZ_COUNT];
	private int[] highscores = new int[QUIZ_COUNT];
	private float[] avgscores = new float[QUIZ_COUNT];
	
	
	// get the statistics data in one run
	public void getHighLowAvg(Student[] students){
		if (students == null){
			return;
		}
		
		int high = 0;
		int low = 999;
		float avg = 0;
		int studentCount = 0;
		
		for (int i = 0; i < QUIZ_COUNT; ++i){
			studentCount = 0;
			high = 0;
			low = 999;
			avg = 0.0f;
			
			for (int j = 0; j < students.length; ++j){
				if (students[j] == null){
					break;
				}
				
				if (students[j].getScore()[i] > high){
					high = students[j].getScore()[i];
				}
				if (students[j].getScore()[i] < low){
					low = students[j].getScore()[i];
				}
				avg += students[j].getScore()[i];
				studentCount++;
			}
			lowscores[i] = low;
			highscores[i] = high;
			avgscores[i] = avg / (float)studentCount;
		}
		
	}
	
	@Override
	public void printInfo() {
		// TODO Auto-generated method stub
		System.out.print("High Score");
		for (int i = 0; i < QUIZ_COUNT; ++i) {
			System.out.print("\t" + highscores[i]);
		}
		System.out.println();

		System.out.print("Low Score");
		for (int i = 0; i < QUIZ_COUNT; ++i) {
			System.out.print("\t" + lowscores[i]);
		}
		System.out.println();

		System.out.print("Average\t");
		for (int i = 0; i < QUIZ_COUNT; ++i) {
			System.out.print("\t" + String.format("%.1f", avgscores[i]));
		}
		System.out.println();
	}

	
	
}
