package wdx.bms.util;

import java.io.File;

public class FolderUtil 
{
	public static boolean createFolder(String filepath)
	{
		boolean res = false;
		File file = new File(filepath);
		// 若不存在则创建
		if (!(file.exists() && file.isDirectory()))
		{
			res = file.mkdir();
		}
		else
		{
			res = false;
		}
		return res;
	}
	
	public static boolean deleteFolder(String filepath)
	{
		boolean res = false;
		File file = new File(filepath);
		if(file.exists() && file.isDirectory())
		{
			res = file.delete();
		}
		return res;
	}
}
