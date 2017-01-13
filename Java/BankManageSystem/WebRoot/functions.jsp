<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>功能选择</title>
    
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->

  </head>
  
  <%
  	String name = (String)request.getAttribute("Name");
  	String type = (String)request.getAttribute("Type");
  	
  	if (type.equals("1"))
  	{
  		type = "前台操作员";
  	}
  	else if (type.equals("2"))
  	{
  		type = "经理";
  	}
  	else if (type.equals("3"))
  	{
  		type = "业务总管";
  	}
  	else if (type.equals("4"))
  	{
  		type = "系统管理员";
  	}
  %>
  
  <body>
  		<table align="center">
    		<tr>
    			<td width="80"> <h1></h1></td>
    			<td><h1>您好，<%=type%> <%=name%>。请选择你所需要的功能</h1></td>
    		</tr>
    		<tr>
    			<td width="40">开户：</td>
    			<td>开户时需要身份证号码，决定开的账户类型，存款的初始金额。银行生成用户的帐号，用户输入初始密码。系统给出开户的时间日志。</td>
    		</tr>
    		<tr>
    			<td width="40">存款：</td>
    			<td>存款时需要给银行提供自己的账号，密码以及要存款的金额；银行系统反馈用户帐户的余额以及存款的日志。</td>
    		</tr>
    		<tr>
    			<td width="40">取款：</td>
    			<td>取款时需要给银行提供自己的账号、密码以及要取的金额；系统判断是否有足够余额以供取款。取款后给出用户帐户的余额，产生取款日志。</td>
    		</tr>
    		<tr>
    			<td width="40">查询：</td>
    			<td>查询时用户需要给银行提供自己的身份证、账号和密码；银行方面给出用户帐户的余额以及查询的日志。用户还可以查询一段时间所以的操作（流水）。</td>
    		</tr>
    		<tr>
    			<td width="40">转帐：</td>
    			<td>用户转帐时需要判断是否能够满足转账条件。如满足，需要给银行提供转出帐户的账号、身份证号码、密码和用户名称及转入帐户的帐号和用户名称，用户也需要提供转账金额。系统方面将产生转账日志、转出用户帐户的余额等信息。</td>
    		</tr>
    		<tr>
    			<td width="40">改密码：</td>
    			<td>用户到银行改密码时需要给银行提供身份证、帐号、以及密码，并且输入新的密码。系统方面将产生相关日志。</td>
    		</tr>
    		<tr>
    			<td width="40">销户：</td>
    			<td>用户到银行销户时需要给银行提供身份证、帐号以及密码，并且把帐户的余额全部取出。系统方面将产生相关日志。</td>
    		</tr>
    	</table>
    	<table>
    		<tr>
    			<td width="80"> <h1></h1></td>
    			<td><h2>对外业务</h2></td>
    		</tr>
    		<tr>
    			<td></td>
    			<td><input type="button" value="开户" style="width:120px" onclick="window.location.href='openaccount.jsp'"/></td>
    			<td><input type="button" value="存款" style="width:120px" onclick="window.location.href='savemoney.jsp'"/></td>
    			<td><input type="button" value="取款" style="width:120px" onclick="window.location.href='withdrawmoney.jsp'"/></td>
    			<td><input type="button" value="查询" style="width:120px" onclick="window.location.href='query.jsp'"/></td>
    		</tr>
    		<tr>
    			<td></td>
    			<td><input type="button" value="转账" style="width:120px" onclick="window.location.href='transfermoney.jsp'"/></td>
    			<td><input type="button" value="修改密码" style="width:120px" onclick="window.location.href='resetpassword.jsp'"/></td>
    			<td><input type="button" value="销户" style="width:120px" onclick="window.location.href='deleteaccount.jsp'"/></td>
    		</tr>
    		<tr>
    			<td width="80"> <h1></h1></td>
    			<td ><h2>对内业务</h2></td>
    		</tr>
    		<tr>
    			<td></td>
    			<td><input type="button" value="日志" style="width:120px" onclick="window.location.href='log'"/></td>
    			<td><input type="button" value="添加管理员" style="width:120px" onclick="window.location.href='addadmin.jsp'"/></td>
    			<td><input type="button" value="添加雇员" style="width:120px" onclick="window.location.href='addemployee.jsp'"/></td>
    			<td><input type="button" value="添加企业账户管理员" style="width:120px" onclick="window.location.href='addenterpriseadmin.jsp'"/></td>
    		</tr>	
    	</table>
  </body>
</html>
