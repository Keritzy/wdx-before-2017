package wdx.bms.model;

import java.io.Serializable;
import java.sql.Timestamp;

public class Account implements Serializable
{
	private int id;
    private String name;// 用户姓名
    private String password;// 用户密码
    private String usertype;// 用户类型(0.普通个人用户 1.个人贵宾用户 2.企业用户)
    private String identity;// 身份证
    private String number;// 帐号
    private String amount;// 金额
    private String createdate;// 开户日期
    private String updatedate;// 存款日期
    private String freeze;// 是否冻结(0.冻结 1.激活)
    private String accounttype;// 账户类型(0.活期 1.定期)
    private int saveyear;// 定期存款时间

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
