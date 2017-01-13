/**
 * 
 */
package server;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.util.ArrayList;
import java.util.Properties;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * The Interface for the server to support different features
 */
public interface AutoServer {
	
	public void buildAutoFromProperty(Properties properites);
	public ArrayList<String> getModelList();
	public void sendModel(ObjectOutputStream objectOutputStream,
			String modelName) throws IOException;
}
