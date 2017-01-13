/**
 * 
 */
package util;

/**
 * @author dawang
 * @andrewid dawang
 * 
 * This is the class for the utility function for servlet
 */
public class ServletUtil {
	public static String headWithTitle(String title) {
	    return("<!DOCTYPE html>\n" +
	           "<html>\n" +
	           "<head><title>" + title + "</title></head>\n");
	  }
}
