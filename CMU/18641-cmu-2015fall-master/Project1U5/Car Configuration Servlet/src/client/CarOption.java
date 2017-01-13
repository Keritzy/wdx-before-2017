/**
 * 
 */
package client;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import model.Automobile;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * This is the class to modify car options locally
 */
public class CarOption {
	public void configureCarChoice(Automobile selectedAuto) {
		BufferedReader stdIn = new BufferedReader(new InputStreamReader(
				System.in));// for client input
		String fromUser = null;// for save client input
		for (int i = 0; i < selectedAuto.getOptionSetListSize(); i++) {
			String optionSetName = selectedAuto.printOptionSetName(i);
			System.out.println(optionSetName);
			selectedAuto.printOption(optionSetName);

			System.out.println("select your choice for " + "(" + optionSetName
					+ ")");

			try {
				fromUser = stdIn.readLine();
			} catch (IOException e1) {
				e1.printStackTrace();
			}

			// filter illegal input
			while (!fromUser.matches("[0-9]+")
					|| Integer.parseInt(fromUser) < 0
					|| Integer.parseInt(fromUser) > selectedAuto
							.getOptionListSize(optionSetName) - 1) {
				System.out.println("please input a legal number");
				try {
					fromUser = stdIn.readLine();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}

			// get the legal index of this option choice in option array of
			// option in option set
			int optionIndex = Integer.parseInt(fromUser);

			System.out.println();
			selectedAuto.setOptionChoice(optionSetName, optionIndex);

		}
	}
}
