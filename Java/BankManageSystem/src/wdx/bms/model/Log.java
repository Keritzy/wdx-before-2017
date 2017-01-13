package wdx.bms.model;

import java.io.Serializable;

public class Log implements Serializable
{
	private int id;
    private String name;// ����
    private String number;// ����
    private String department; // ����
    private String Position;// ְ��(0.ǰ̨����Ա 1.���о��� 2.����ҵ���ܹ�)
    private String account; // ��Ӧ���˻��˺�
    private String operation; // ��Ӧִ�еĲ���
    private String opdetail; // ִ�в����������Ϣ
    

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
