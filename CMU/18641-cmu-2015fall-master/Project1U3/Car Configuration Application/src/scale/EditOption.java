/**
 * 
 */
package scale;

import exception.AutoException;
import model.Automobile;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * Use synchronized methods in the Automobile class to operate on Automobile
 * LinkedHashMap instance in ProxyAutomobile
 */
public class EditOption extends Thread {
	private Automobile editAutomobile;
	/**
	 * args[0]: car model name
	 * args[1]: option set name
	 * args[2]: new option set name / option name
	 * args[3]: new option price
	 */
	private String[] args;
	private int threadID;
	private EditOptionEnumerator editOptionEnumerator;
	
	public enum EditOptionEnumerator{
		EditOptionSetName, EditOptionPrice;
	};

	// constructor
	public EditOption(int threadId, Automobile editAutomobile, 
			EditOptionEnumerator editOptionEnumerator, String[] args){
		this.threadID = threadId;
		this.editAutomobile = editAutomobile;
		this.editOptionEnumerator = editOptionEnumerator;
		this.args = args;
	}
	
	/**
	 * Thread function that will perform specific tasks based on the
	 * editOptionEnumerator. I only add two different tasks so as only to learn
	 * the thread feature.
	 */
	@Override
	public void run(){
		switch(editOptionEnumerator){
		case EditOptionSetName:
			threadUpdateOptionSetName();
			break;
		case EditOptionPrice:
			try {
				threadUpdateOptionPrice();
			} catch (NumberFormatException | AutoException ae) {
				System.out.println("Error -- " + ae.toString());
			}
			break;
		}
	}
	
	/**
	 * Set different OptionSet name to test the thread function
	 */
	public void threadUpdateOptionSetName() {
		String[] threadOptionSetName = { args[1], 
				args[2] + "1",
				args[2] + "2",
				args[2] + "3",
				args[1] };

		synchronized (editAutomobile) { 
			for (int i = 0; i < threadOptionSetName.length - 1; i++) {
				// wait for random time in for()
				waiting();
				try {
					// update option set name
					// [i] - old name
					// [i+1] - new name
					editAutomobile.updateOptionSetName(threadOptionSetName[i],
							threadOptionSetName[i + 1]);
					printThreadOperation(threadID,
							threadOptionSetName[i],
							editAutomobile.getOptionSetName(
									threadOptionSetName[i + 1]));

				} catch (AutoException ae) {
					System.out.println("Thread" + threadID + " : "
							+ "Error -- " + ae.toString());
				}

			}
		}
	}
	
	/**
	 * Print the thread operation for update option set name
	 * @param threadID of the current thread
	 * @param oldName of the option set
	 * @param newName of the option set
	 */
	public void printThreadOperation(int threadID, String oldName, String newName){
		System.out.print("Thread #" + threadID + " [OptionSet Name] ");
		System.out.println(oldName + "->" + newName);
	}
	
	/**
	 * Print the thread operation for update option price
	 * @param threadID of the current thread
	 * @param optionSetName of the option set
	 * @param optionName of the option
	 * @param price of the option
	 */
	public void printThreadOperation(int threadID, String optionSetName,
			String optionName, float price){
		System.out.print("Thread #" + threadID + " [OptionSet " + optionSetName);
		System.out.println("] [Option " + optionName + "] Price: " + price);
	}
	
	/**
	 * 
	 * @throws NumberFormatException
	 * @throws AutoException
	 */
	public void threadUpdateOptionPrice() 
			throws NumberFormatException, AutoException {
		synchronized (editAutomobile) { 
			for (int i = 1; i < 5; i++) {
				waiting();
				// args[1] - option set name
				// args[2] - option name
				// args[3] - new price
				editAutomobile.updateOptionPrice(args[1], args[2],
						i * Float.parseFloat(args[3]));
				printThreadOperation(threadID, args[1], args[2],
						editAutomobile.getOptionPrice(args[1], args[2]));
			}
		}
	}
	
	
	
	void waiting() {
		try {
			Thread.currentThread();
			Thread.sleep((long) (2000 * Math.random()));
		} catch (InterruptedException e) {
			System.out.println("Interrupted!");
		}
	}
	
}
