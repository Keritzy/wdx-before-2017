package wdx.bms.model;

import java.io.Serializable;

public class EnterpriseOperator implements Serializable
{
	private int id;
    private String name;// ����
    private String password;// ����
    private String number; // ����
    private String accountNumber;// ��Ӧ��ҵ�û��ʺ�

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

    public String getPassword() 
    {
        return password;
    }

    public void setPassword(String password) 
    {
        this.password = password;
    }

    public String getNumber() 
    {
        return number;
    }

    public void setNumber(String number) 
    {
        this.number = number;
    }

    public String getAccountNumber() 
    {
        return accountNumber;
    }

    public void setAccountNumber(String accountNumber) 
    {
        this.accountNumber = accountNumber;
    }
}
