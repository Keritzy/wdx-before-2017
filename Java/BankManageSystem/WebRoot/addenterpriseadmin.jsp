<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>添加企业账户管理员页面</title>
    
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
    <form action="addenterpriseadmin" method="post">
    	<table align="center">
    		<tr>
    			<td colspan="2"> <h1>添加企业账户管理员页面</h1></td>
    		</tr>
    		<tr>
    			<td>ID(数字)：</td>
    			<td><input type="text" name="id"/></td>
    		</tr>
    		<tr>
    			<td>姓名：</td>
    			<td><input type="text" name="name"/></td>
    		</tr>
    		<tr>
    			<td>工号：</td>
    			<td><input type="text" name="number"/></td>
    		</tr>
    		<tr>
    			<td>密码：</td>
    			<td><input type="password" name="pwd"/></td>
    		</tr>
    		<tr>
    			<td>对应企业账号：</td>
    			<td><input type="text" name="accountNumber"/></td>
    		</tr>
    		<tr>
    			<td><input type="submit" value="确认添加企业账户管理员"/></td>
    			<td><input type="button" value="返回" onclick="javascript:history.go(-1);"/></td>
    		</tr>  		
    	</table>
    </form>
  </body>
</html>
