<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>wdx�ĳ����޵м�Լ�������й���ϵͳ</title>
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
    <br>
    
    <form action="login" method="post">
    	<table align="center">
    		<tr>
    			<td colspan="2"> <h1>��ӭ�������й���ϵͳ</h1></td>
    		</tr>
    		<tr>
    			<td>�û�����</td>
    			<td><input type="text" name="name"/></td>
    		</tr>
    		<tr>
    			<td>���룺</td>
    			<td><input type="password" name="pwd"/></td>
    		</tr>
    		<tr>
    			<td>��Ա���ͣ�</td>
    			<td>
    				<input type="radio" name="type" value="1"/>ǰ̨����Ա
    				<input type="radio" name="type" value="2"/>���о���
    				<input type="radio" name="type" value="3"/>����ҵ���ܹ�
    				<input type="radio" name="type" value="4"/>ϵͳ����Ա
    			</td>
    		</tr>
    		<tr>
    			<td colspan="2"><input type="submit" value="��¼"/></td>
    		</tr>
    		
    	</table>
    </form>
    
  </body>
</html>
