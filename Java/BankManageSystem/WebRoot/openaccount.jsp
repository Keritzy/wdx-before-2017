<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>开户页面</title>
    
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
	<meta http-equiv="expires" content="0">    
	<meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
	<meta http-equiv="description" content="This is my page">
	<!--
	<link rel="stylesheet" type="text/css" href="styles.css">
	-->
	
  </head>
  
  <body>
    <form action="openaccount" method="post">
    	<table align="center">
    		<tr>
    			<td colspan="2"> <h1>开户页面</h1></td>
    		</tr>
    		<tr>
    			<td>ID(数字)：</td>
    			<td><input type="text" name="id"/></td>
    		</tr>
    		<tr>
    			<td>用户名：</td>
    			<td><input type="text" name="name"/></td>
    		</tr>
    		<tr>
    			<td>密码：</td>
    			<td><input type="password" name="pwd"/></td>
    		</tr>
    		<tr>
    			<td>用户类型：</td>
    			<td>
    				<input type="radio" name="type" value="0"/>普通个人用户
    				<input type="radio" name="type" value="1"/>个人贵宾用户
    				<input type="radio" name="type" value="2"/>企业用户
    			</td>
    		</tr>
    		<tr>
    			<td>账号：</td>
    			<td><input type="text" name="number"/></td>
    		</tr>
    		<tr>
    			<td>身份证号：</td>
    			<td><input type="text" name="identity"/></td>
    		</tr>
    		<tr>
    			<td>金额：</td>
    			<td><input type="text" name="amount"/></td>
    		</tr>
    		<tr>
    			<td>开户日期：</td>
    			<td><input type="text" name="createdate"/></td>
    		</tr>
    		<tr>
    			<td>存款日期：</td>
    			<td><input type="text" name="updatedate"/></td>
    		</tr>
    		<tr>
    			<td>是否冻结：</td>
    			<td>
    				<input type="radio" name="isfreeze" value="0"/>是
    				<input type="radio" name="isfreeze" value="1"/>否
    			</td>
    		</tr>
    		<tr>
    			<td>账户类型：</td>
    			<td>
    				<input type="radio" name="accounttype" value="0"/>活期
    				<input type="radio" name="accounttype" value="1"/>定期
    			</td>
    		</tr>
    		<tr>
    			<td>定期存款时间(数字)：</td>
    			<td><input type="text" name="saveyear"/></td>
    		</tr>
    		<tr>
    			<td><input type="submit" value="确认开户"/></td>
    			<td><input type="button" value="返回" onclick="javascript:history.go(-1);"/></td>
    		</tr>  		
    	</table>
    </form>
  </body>
</html>
