/**
 * 
 */
package adapter;

/** The interface for creating auto and save to database
 * Implemented in ProxyAutomobile.java
 * @author dawang
 * @andrewid dawang
 */
public interface DatabaseAuto {
	public void buildDBAuto(String filename);
	public void deleteDBAuto(String autoName);
	public void updateDBAutoPrice(String autoName, float newPrice);
}
