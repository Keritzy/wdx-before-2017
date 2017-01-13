package wdx.bms.util;

import java.io.File;
import java.util.*;

import wdx.bms.model.*;

public class SearchUtil 
{
	public static Account getAccountByName(String name, String folderpath)
	{
		Account a = null;
		List<Account> accountList = new ArrayList<Account>();
		File dir = new File(folderpath);
		File[] files = dir.listFiles();
		for (File file : files)
		{
			if (file.getName().contains("."))
				continue;
			
			accountList.add(FileUtil.readAccount(folderpath + "/" + file.getName()));			
		}
		// 匹配对应账户，获得id
		for (int i = 0; i < accountList.size(); i++)
		{
			if (accountList.get(i).getNumber().equals(name))
			{
				a = accountList.get(i);
				break;
			}
		}		
		return a;
	}
	
	public static Employee getEmployeeByName(String name, String folderpath)
	{
		Employee e = null;
		
		List<Employee> employeeList = new ArrayList<Employee>();
		File dir = new File(folderpath);
		File[] files = dir.listFiles();
		for (File file : files)
		{
			if (file.getName().contains("."))
				continue;
			
			employeeList.add(FileUtil.readEmployee(folderpath + "/" + file.getName()) );
		}
		
		// 匹配对应雇员，获得id
		for (int i = 0; i < employeeList.size(); i++)
		{
			if (employeeList.get(i).getName().equals(name))
			{
				e = employeeList.get(i);
				break;
			}
		}		
		
		return e;
		
	}
}
