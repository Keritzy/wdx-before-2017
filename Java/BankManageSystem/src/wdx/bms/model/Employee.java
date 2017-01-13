package wdx.bms.model;

import java.io.Serializable;

public class Employee implements Serializable
{
	private int id;
    private String name;// 姓名
    private String number;// 工号
    private String password;// 密码
    private String department; // 部门
    private String Position;// 职责(0.前台操作员 1.银行经理 2.银行业务总管)

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
