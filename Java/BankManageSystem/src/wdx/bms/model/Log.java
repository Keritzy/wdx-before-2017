package wdx.bms.model;

import java.io.Serializable;

public class Log implements Serializable
{
	private int id;
    private String name;// 姓名
    private String number;// 工号
    private String department; // 部门
    private String Position;// 职责(0.前台操作员 1.银行经理 2.银行业务总管)
    private String account; // 对应的账户账号
    private String operation; // 对应执行的操作
    private String opdetail; // 执行操作的相关信息
    

    public int getId() 
    {
        return id;
    }

    public void setId(int id) 
    {
        this.id = id;
    }

    public String getName() 
    {
        return name;
    }

    public void setName(String name) 
    {
        this.name = name;
    }

    public String getNumber() 
    {
        return number;
    }

    public void setNumber(String number) 
    {
        this.number = number;
    }


    public String getPosition() 
    {
        return Position;
    }

    public void setPosition(String position) 
    {
        Position = position;
    }

    public String getDepartment() 
    {
        return department;
    }

    public void setDepartment(String department) 
    {
        this.department = department;
    }
    
    public String getOperation()
    {
    	return operation;
    }
    
    public void setOperation(String operation)
    {
    	this.operation = operation;
    }
    
    public String getOpdetail()
    {
    	return opdetail;
    }
    
    public void setOpdetail(String opdetail)
    {
    	this.opdetail = opdetail;
    }
    
    public String getAccount()
    {
    	return account;
    }
    
    public void setAccount(String account)
    {
    	this.account = account;
    }
    
}
