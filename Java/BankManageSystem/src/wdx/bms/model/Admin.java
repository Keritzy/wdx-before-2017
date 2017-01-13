package wdx.bms.model;

import java.io.Serializable;

public class Admin implements Serializable
{
	private int id;
    private String number;// 管理员帐号
    private String name;// 管理员姓名
    private String password;// 管理员密码

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
