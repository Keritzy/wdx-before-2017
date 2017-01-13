/**
 * 
 */
package dawang.assignment2;

import dawang.assignment2.model.Statistics;
import dawang.assignment2.model.Student;
import dawang.assignment2.util.Util;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This class is the Driver class with the main function that will test the 
 * whole program.
 * 
 * With Three different testcases and exception handling which is required
 * from the document
 */
public class Driver {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// Test case 1: same as the document
		testCase("Test Case 1: Sample Input", "student_sample.txt");
		testCase("Test Case 2: Homework Input", "student.txt");
		testCase("Test Case 3: Exception Input", "student_45.txt");
		
	}

	public static void testCase(String description, String filename){
		System.out.println("----------------------------");
		System.out.println(description);
		System.out.println("----------------------------");
		Student[] class18641 = new Student[40];
		
		class18641 = Util.loadFile(filename, class18641);
		Statistics stat = new Statistics();
		stat.getHighLowAvg(class18641);
		
		System.out.println("Stud\t" + "Q1\t" + "Q2\t" + "Q3\t" + "Q4\t" + "Q5");
		for (int i = 0; i < class18641.length && class18641[i] != null; ++i) {
			class18641[i].printInfo();
		}
		System.out.println();

		// print statics here
		stat.printInfo();
	}
	
}
