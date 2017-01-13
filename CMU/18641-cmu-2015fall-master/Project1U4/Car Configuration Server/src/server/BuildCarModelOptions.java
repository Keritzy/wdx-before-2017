/**
 * 
 */
package server;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.util.ArrayList;
import java.util.Properties;

import adapter.BuildAuto;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * (From Document)
 * 1. accept properties object from client socket over an object stream and 
 * create an automobile
 * 2. add that created Automobile to the LinkedHashMap
 * 3. AutoServer interface should be implemented in BuildAuto and 
 * BuildCarModelOptions classes
 * 4. Based on the current structure, this method will be implemented in
 * proxyAutomobile class and called in a method of BuildCarModelOptions.
 */
public class BuildCarModelOptions implements AutoServer {

	private static BuildAuto buildAuto;
	
	// get the unique static buildAuto instance
	public BuildCarModelOptions(){
		buildAuto = new BuildAuto();
	}
	
	public void buildAutoFromProperty(Properties properites) {
		buildAuto.buildAutoFromProperty(properites);
	}

	public ArrayList<String> getModelList() {
		ArrayList<String> autoList = buildAuto.getModelList();
		return autoList;
	}

	public void sendModel(ObjectOutputStream objectOutputStream, 
			String modelName) throws IOException {
		buildAuto.sendModel(objectOutputStream, modelName);
	}

}
