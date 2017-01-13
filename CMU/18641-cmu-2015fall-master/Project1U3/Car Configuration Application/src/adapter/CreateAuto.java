package adapter;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * Interface for creating and building automobile
 */
public interface CreateAuto {
	
	/**
	 * Given a text file name and build an instance of Automobile. This method
	 * does not have to return the Auto instance
	 * 
	 * @param filename the specific text file name
	 */
	public void buildAuto(String filename);
	
	/**
	 * This function searches and prints the properties of a given Automobile
	 * 
	 * @param modelName the name of the given Automobile
	 */
	public void printAuto(String modelName);
}
