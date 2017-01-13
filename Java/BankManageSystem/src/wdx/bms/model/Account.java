package wdx.bms.model;

import java.io.Serializable;
import java.sql.Timestamp;

public class Account implements Serializable
{
	private int id;
    private String name;// �û�����
    private String password;// �û�����
    private String usertype;// �û�����(0.��ͨ�����û� 1.���˹���û� 2.��ҵ�û�)
    private String identity;// ���֤
    private String number;// �ʺ�
    private String amount;// ���
    private String createdate;// ��������
    private String updatedate;// �������
    private String freeze;// �Ƿ񶳽�(0.���� 1.����)
    private String accounttype;// �˻�����(0.���� 1.����)
    private int saveyear;// ���ڴ��ʱ��

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

    public String getUsertype() 
    {
        return usertype;
    }

    public void setUsertype(String usertype) 
    {
        this.usertype = usertype;
    }

    public String getIdentity() 
    {
        return identity;
    }

    public void setIdentity(String identity) 
    {
        this.identity = identity;
    }

    public String getNumber() 
    {
        return number;
    }

    public void setNumber(String number) 
    {
        this.number = number;
    }

    public String getAmount() 
    {
        return amount;
    }

    public void setAmount(String amount) 
    {
        this.amount = amount;
    }

    public String getCreatedate() 
    {
        return createdate;
    }

    public void setCreatedate(String createdate) 
    {
        this.createdate = createdate;
    }

    public String getUpdatedate() 
    {
        return updatedate;
    }

    public void setUpdatedate(String updatedate) 
    {
        this.updatedate = updatedate;
    }

    public String getFreeze() 
    {
        return freeze;
    }

    public void setFreeze(String freeze) 
    {
        this.freeze = freeze;
    }

    public String getAccounttype() 
    {
        return accounttype;
    }

    public void setAccounttype(String accounttype) 
    {
        this.accounttype = accounttype;
    }

    public int getSaveyear() 
    {
        return saveyear;
    }

    public void setSaveyear(int saveyear) 
    {
        this.saveyear = saveyear;
    }
	
}
