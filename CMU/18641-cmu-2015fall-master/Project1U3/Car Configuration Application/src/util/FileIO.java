/**
 * 
 */
package util;

import java.io.*;

import exception.AutoException;
import exception.CustomExceptionEnumerator;
import exception.FixHelper;
import model.Automobile;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the class for loading data from text file as well as serialize / 
 * deserialize data
 */
public class FileIO {
	public Automobile buildAutoObject(String filename) throws AutoException {

		Automobile auto = null; 
		BufferedReader br = null; 
		String curLine; 
		// name, base price and option set size
		String[] baseInfo = new String[3]; 
		int optionSetSize = 0; 
		String[] curOptionSet; // temporarily store option set name and size
		String[] storeOptionString; // temporarily store option strings without
									// split
		String[] curOptionDetail; // temporarily store option detail

		try {
			br = new BufferedReader(new FileReader(filename));

			// read in base info
			for (int i = 0; i < baseInfo.length; i++) {
				baseInfo[i] = br.readLine();
			}
			
			try {
				// if there is no base price , FixHelper will set it 0
				if (baseInfo[2].length() == 0) {
					throw new AutoException(
							CustomExceptionEnumerator.FileNoBasePrice);
				}
			} catch (AutoException e) {
				FixHelper fixHelper = new FixHelper();
				baseInfo[2] = fixHelper.fixFileNoBasePrice();
			}
			
			auto = new Automobile(baseInfo[0],
					baseInfo[1],
					Float.parseFloat(baseInfo[2]));
			
			// get option set size
			while ((curLine = br.readLine()) != null){
				curOptionSet = curLine.split("#");
				auto.setOptionSet(curOptionSet[0]);
				for (int j = 0; j < Integer.parseInt(curOptionSet[1]); j++){
					curLine = br.readLine();
					curOptionDetail = curLine.split(",");
					
					auto.setOption(curOptionSet[0], curOptionDetail[0],
							Float.parseFloat(curOptionDetail[1]));	
				}
			}

		} catch (FileNotFoundException fe) {
			// if file not found, FixHelper will fix it
			throw new AutoException(CustomExceptionEnumerator.FileNotFound);
		} catch (IOException e) {
			System.out.println("Error -- " + e.toString());
		} finally {
			try {
				if (br != null) {// if file not found, it will be null
					br.close();
				}
			} catch (IOException brCloseException) {
				System.out.println("Error -- " + brCloseException.toString());
			}
		}
		return auto;
	}
	
	public void serializeAuto(Automobile auto){
		try{
			ObjectOutputStream out = new ObjectOutputStream(
					new FileOutputStream("auto.ser"));
			out.writeObject(auto);
			out.close();
		} catch (Exception e){
			System.out.print("Error: " + e);
			System.exit(1);
		}
	}
	
	public Automobile deserializeAuto(String filename) throws AutoException{
		Automobile auto = null;
		try{
			ObjectInputStream in = new ObjectInputStream(
					new FileInputStream(filename));
			auto = (Automobile) in.readObject();
			in.close();
		} catch (Exception e){
			System.out.print("Error: " + e);
			System.exit(1);
		}
		return auto;
	}
	
}
