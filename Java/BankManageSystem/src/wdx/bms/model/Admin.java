package wdx.bms.model;

import java.io.Serializable;

public class Admin implements Serializable
{
	private int id;
    private String number;// ����Ա�ʺ�
    private String name;// ����Ա����
    private String password;// ����Ա����

    public int getId() 
    {
        return id;
    }

    public void setId(int id) 
    {
        this.id = id;
    }

    public String getNumber() 
    {
        return number;
    }

    public void setNumber(String number) 
    {
        this.number = number;
    }

    public String getName() 
    {
        return name;
    }

    public void setName(String name) 
    {
        this.name = name;
    }

    public String getPassword() 
    {
        return password;
    }

    public void setPassword(String password) 
    {
        this.password = password;
    }
}
