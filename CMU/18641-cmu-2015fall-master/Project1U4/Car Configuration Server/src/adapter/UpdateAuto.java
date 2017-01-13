/**
 * 
 */
package adapter;

/**
 * @author Da Wang
 * @andrew_id dawang
 * 
 * Update information for automobile
 */
public interface UpdateAuto {
	public void updateOptionSetName(String modelName, String optionSetName,
			String newName);
	
	public void updateOptionPrice(String modelName, String optionName,
			String option, float newPrice);
}
