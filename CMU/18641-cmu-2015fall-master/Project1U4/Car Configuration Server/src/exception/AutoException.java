/**
 * 
 */
package exception;

import java.io.*;
import java.sql.Timestamp;
import java.util.Date;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This class has the following features:
 * 1. Ability to track no error and error message
 * 2. Contain an enumeration of all possible error numbers and messages, which
 * can be used, when AutoException is instantiated.
 * 3. Ability to log AutoException with timestamps into a log file (you do not
 * need to implement any complex logging mechanism)
 */
public class AutoException extends Exception {
	private int exceptionIndex;
	private String exceptionName;
	
	// constructor
	public AutoException(CustomExceptionEnumerator exception){
		exceptionIndex = exception.getExceptionIndex();
		exceptionName = exception.toString();
		log();
	}
	
	@Override
	public String toString(){
		return "Exception #" + exceptionIndex + " " + exceptionName;
	}
	
	// getter
	public int getExceptionIndex(){
		return exceptionIndex;
	}
	
	// log AutoException with timestamps into a log file
	public void log(){
		Date date = new Date();
		Timestamp timestamp = new Timestamp(date.getTime());
		String logline = timestamp.toString() + " " + this.toString() + "\n";
		try {
			BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(
					new FileOutputStream("exception_log.txt", true)));
			bw.write(logline);
			bw.flush();
			bw.close();
		} catch (IOException e) {
			System.out.println("Error -- " + e.toString());
		}
	}
}
