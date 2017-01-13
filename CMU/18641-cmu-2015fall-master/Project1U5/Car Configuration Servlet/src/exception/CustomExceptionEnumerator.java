/**
 * 
 */
package exception;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * The enumeration of all possible error numbers and messages
 */
public enum CustomExceptionEnumerator {
	FileNotFound(1),
	FileNoBasePrice(2),
	FileNoOptionPrice(3),
	CarModelNotFound(4),
	OptionSetNotFound(5),
	OptionNotFound(6),
	SerialCarFileNotFound(7);
	
	private int exceptionIndex;
	
	private CustomExceptionEnumerator(int index){
		exceptionIndex = index;
	}
	
	// getter
	public int getExceptionIndex(){
		return exceptionIndex;
	}
}
