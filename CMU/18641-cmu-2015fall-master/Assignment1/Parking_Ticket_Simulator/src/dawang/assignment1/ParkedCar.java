/**
 * 
 */
package dawang.assignment1;

/**
 * @author Da Wang
 * @andrew_id dawang
 */
public class ParkedCar {
	private String make;
	private String model;
	private String color;
	private String licenseNumber;
	private int parkedMinutes;
	
	public ParkedCar(String make, String model, String color, 
			String licenseNumber, int parkedMinutes){
		this.make = make;
		this.model = model;
		this.color = color;
		this.licenseNumber = licenseNumber;
		if (parkedMinutes >= 0){
			this.parkedMinutes = parkedMinutes;
		}
		else{
			this.parkedMinutes = 0;
		}		
	}
	
	// setter and getter for every variables
	public String getMake(){
		return make;
	}
	
	public void setMake(String make) {
		this.make = make;
	}
	
	public String getModel() {
		return model;
	}

	public void setModel(String model) {
		this.model = model;
	}

	public String getColor() {
		return color;
	}

	public void setColor(String color) {
			this.color = color;
	}


	public String getLicenseNumber() {
		return licenseNumber;
	}

	public void setLicenseNumber(String licenseNumber) {
			this.licenseNumber = licenseNumber;
	}


	public int getParkedMinutes() {
		return parkedMinutes;
	}


	public void setParkedMinutes(int parkedMinutes) {
		if (parkedMinutes >= 0) {
			this.parkedMinutes = parkedMinutes;
		}
		else{
			this.parkedMinutes = 0;
		}
	}
	
}
