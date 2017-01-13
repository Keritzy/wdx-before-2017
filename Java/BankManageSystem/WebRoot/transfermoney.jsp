<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>转账页面</title>
    
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
    <form action="transfermoney" method="post">
    	<table align="center">
    		<tr>
    			<td colspan="2"> <h1>转账页面</h1></td>
    		</tr>
    		<tr>
    			<td colspan="2"><h2>转出账号相关信息</h2></td>
    		</tr>
    		<tr>
    			<td>用户名：</td>
    			<td><input type="text" name="name"/></td>
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
    			<td>密码：</td>
    			<td><input type="password" name="pwd"/></td>
    		</tr>
    		
    		<tr>
    			<td>金额：</td>
    			<td><input type="text" name="amount"/></td>
    		</tr>
    		
    		<tr>
    			<td colspan="2"><h2>转入账号相关信息</h2></td>
    		</tr>
    		<tr>
    			<td>用户名：</td>
    			<td><input type="text" name="name1"/></td>
    		</tr>
    		<tr>
    			<td>账号：</td>
    			<td><input type="text" name="number1"/></td>
    		</tr>
    		
    		<tr>
    			<td><input type="submit" value="确认转账"/></td>
    			<td><input type="button" value="返回" onclick="javascript:history.go(-1);"/></td>
    		</tr>  		
    	</table>
    </form>
  </body>
</html>
