/**
 * 
 */
package dawang.assignment2.util;

import java.io.*;
import java.util.*;

import dawang.assignment2.exception.TooManyStudentException;
import dawang.assignment2.model.Student;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the utility class for loading the data files
 */
public class Util {
	private static final int MAX_VALID_STUDENT = 40;
	private static final int QUIZ_COUNT = 5;
	
	public static Student[] loadFile(String filename, Student[] students){
		int studentCount = 0;
		BufferedReader buff = null;
		
		try{
			FileReader file = new FileReader(filename);
			buff = new BufferedReader(file);
			boolean eof = false;
			
			// skip the first line
			buff.readLine();
			
			while (!eof){
				String line = buff.readLine();
				if (line == null){
					eof = true;
					break;
				}
				else{
					if (studentCount < MAX_VALID_STUDENT){
						StringTokenizer str = new StringTokenizer(line);
						int sid = 0;
						int[] scores = new int[QUIZ_COUNT];
						int scoreCount = 0;
						
						sid = Integer.parseInt(str.nextToken());
						
						while(str.hasMoreTokens()){
							scores[scoreCount] = 
									Integer.parseInt(str.nextToken());
							scoreCount++;
						}
						
						students[studentCount] = new Student();
						students[studentCount].setSID(sid);
						students[studentCount].setScore(scores);
						
						studentCount++;
					}
					else {
						if (line != null){
							throw new TooManyStudentException(
									"More than " + MAX_VALID_STUDENT 
									+ " students");
						}
					}
				}
			}
		}
		catch (TooManyStudentException s){
			System.out.println("Too Many Students Exception - " 
					+ s.toString());
		}
		catch (IOException e){
			System.out.println("Error -- " + e.toString());
		}
		finally {
			try {
				buff.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				System.out.println("Error -- " + e.toString());
			}
		}
		
		return students;
	}

}
