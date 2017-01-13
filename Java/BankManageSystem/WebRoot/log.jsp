<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>日志页面</title>
    
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
    <form action="addadmin" method="post">
    	<table align="center">
    		<tr>
    			<td colspan="2"> <h1>日志查看页面</h1></td>
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
    			<td>雇员类型：</td>
    			<td>
    				<input type="radio" name="type" value="1"/>前台操作员
    				<input type="radio" name="type" value="2"/>银行经理
    				<input type="radio" name="type" value="3"/>银行业务总管
    				<input type="radio" name="type" value="4"/>系统管理员
    			</td>
    		</tr>
    		<tr>
    			<td><input type="submit" value="确认添加管理员"/></td>
    			<td><input type="button" value="返回" onclick="javascript:history.go(-1);"/></td>
    		</tr>  		
    	</table>
    </form>
  </body>
</html>
