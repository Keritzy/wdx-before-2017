package wdx.bms.model;

import java.io.Serializable;

public class Employee implements Serializable
{
	private int id;
    private String name;// ����
    private String number;// ����
    private String password;// ����
    private String department; // ����
    private String Position;// ְ��(0.ǰ̨����Ա 1.���о��� 2.����ҵ���ܹ�)

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

    public String getPassword() 
    {
        return password;
    }

    public void setPassword(String password) 
    {
        this.password = password;
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
}
