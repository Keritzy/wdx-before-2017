package wdx.bms.util;

import java.io.*;
import java.util.*;

import wdx.bms.model.*;

public class FileUtil 
{
	// filepath中包含名字
	public static void writeObject(String filepath, Object obj)
	{
	    try 
	    { 
	    	FileOutputStream fo = new FileOutputStream(filepath); 
			ObjectOutputStream so = new ObjectOutputStream(fo); 
	    	so.writeObject(obj); 
	    	so.close(); 
	    } 
	    catch (IOException e) 
	    { 
	    	System.out.println(e); 
	    } 
		
	}
	
	public static Admin readAdmin(String filepath) 
	{	
		Admin a = null;
		try 
		{ 			 
			FileInputStream fi = new FileInputStream(filepath); 
			ObjectInputStream si = new ObjectInputStream(fi);
			a = (Admin)si.readObject();			
			si.close(); 					
	    } 
		catch (IOException ex) 
		{ 
			System.out.println(ex); 
		} 
		catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
		return a;
	}
	
	public static Employee readEmployee(String filepath)
	{
		Employee ep = null;
		
		try 
		{ 			 
			FileInputStream fi = new FileInputStream(filepath); 
			ObjectInputStream si = new ObjectInputStream(fi);
			ep = (Employee)si.readObject();			
			si.close(); 					
	    } 
		catch (IOException ex) 
		{ 
			System.out.println(ex); 
		} 
		catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ep;
		
	}
	
	public static EnterpriseOperator readEnterpriseOperator(String filepath)
	{
		EnterpriseOperator eo = null;
		
		try 
		{ 			 
			FileInputStream fi = new FileInputStream(filepath); 
			ObjectInputStream si = new ObjectInputStream(fi);
			eo = (EnterpriseOperator)si.readObject();			
			si.close(); 					
	    } 
		catch (IOException ex) 
		{ 
			System.out.println(ex); 
		} 
		catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return eo;
	}
	
	public static Account readAccount(String filepath)
	{
		Account a = null;
		
		try 
		{ 			 
			FileInputStream fi = new FileInputStream(filepath); 
			ObjectInputStream si = new ObjectInputStream(fi);
			a = (Account)si.readObject();			
			si.close(); 					
	    } 
		catch (IOException ex) 
		{ 
			System.out.println(ex); 
		} 
		catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return a;
		
	}
	
	public static Log readLog(String filepath)
	{
		Log l = null;
		
		try 
		{ 			 
			FileInputStream fi = new FileInputStream(filepath); 
			ObjectInputStream si = new ObjectInputStream(fi);
			l = (Log)si.readObject();			
			si.close(); 					
	    } 
		catch (IOException ex) 
		{ 
			System.out.println(ex); 
		} 
		catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return l;
	}
	
	public static List<String> getAllFiles(String folderpath)
	{
		List<String> fileList = new ArrayList<String>();
		
		File dir = new File(folderpath);
		File[] files = dir.listFiles();
		for (File file : files)
		{
			if (file.getName().contains("."))
				continue;
			fileList.add(file.getName());			
		}
		
		return fileList;
	}
	
	// 删除指定文件
	public static void deleteFile(String path)
	{
		File file = new File(path);
		file.delete();
		
	}
}
