/**
 * 
 */
package util;

import java.io.*;

import model.Automotive;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * This is the class for loading data from text file as well as serialize / 
 * deserialize data
 */
public class FileIO {
	public Automotive buildAutoObject(String filename) {

		Automotive auto = null; 
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
			auto = new Automotive(baseInfo[0],
					Float.parseFloat(baseInfo[1]),
					Integer.parseInt(baseInfo[2]));

			// get option set size
			optionSetSize = auto.getOptionSetSize();
			
			for (int i = 0; i < optionSetSize; i++) {
				// get option set information and initialize option set
				curLine = br.readLine();
				curOptionSet = curLine.split("#");
				auto.setOptionSet(i, curOptionSet[0],
						Integer.parseInt(curOptionSet[1]));

				// get options and its price
				for (int j = 0; j < Integer.parseInt(curOptionSet[1]); j++){
					curLine = br.readLine();
					curOptionDetail = curLine.split(",");
					
					auto.setOption(i, j, curOptionDetail[0],
							Float.parseFloat(curOptionDetail[1]));	
				}
				
			}

		} catch (IOException e) {
			System.out.println("Error -- " + e.toString());
		} finally {
			try {
				br.close();
			} catch (IOException brCloseException) {
				System.out.println("Error -- " + brCloseException.toString());
			}
		}
		return auto;
	}
	
	public void serializeAuto(Automotive auto){
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
	
	public Automotive deserializeAuto(String filename){
		Automotive auto = null;
		try{
			ObjectInputStream in = new ObjectInputStream(
					new FileInputStream(filename));
			auto = (Automotive) in.readObject();
			in.close();
		} catch (Exception e){
			System.out.print("Error: " + e);
			System.exit(1);
		}
		return auto;
	}
	
}
