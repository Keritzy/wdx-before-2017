<%@ page language="java" import="java.util.*" pageEncoding="GB18030"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <base href="<%=basePath%>">
    
    <title>����ѡ��</title>
    
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
  		type = "ǰ̨����Ա";
  	}
  	else if (type.equals("2"))
  	{
  		type = "����";
  	}
  	else if (type.equals("3"))
  	{
  		type = "ҵ���ܹ�";
  	}
  	else if (type.equals("4"))
  	{
  		type = "ϵͳ����Ա";
  	}
  %>
  
  <body>
  		<table align="center">
    		<tr>
    			<td width="80"> <h1></h1></td>
    			<td><h1>���ã�<%=type%> <%=name%>����ѡ��������Ҫ�Ĺ���</h1></td>
    		</tr>
    		<tr>
    			<td width="40">������</td>
    			<td>����ʱ��Ҫ���֤���룬���������˻����ͣ����ĳ�ʼ�����������û����ʺţ��û������ʼ���롣ϵͳ����������ʱ����־��</td>
    		</tr>
    		<tr>
    			<td width="40">��</td>
    			<td>���ʱ��Ҫ�������ṩ�Լ����˺ţ������Լ�Ҫ���Ľ�����ϵͳ�����û��ʻ�������Լ�������־��</td>
    		</tr>
    		<tr>
    			<td width="40">ȡ�</td>
    			<td>ȡ��ʱ��Ҫ�������ṩ�Լ����˺š������Լ�Ҫȡ�Ľ�ϵͳ�ж��Ƿ����㹻����Թ�ȡ�ȡ�������û��ʻ���������ȡ����־��</td>
    		</tr>
    		<tr>
    			<td width="40">��ѯ��</td>
    			<td>��ѯʱ�û���Ҫ�������ṩ�Լ������֤���˺ź����룻���з�������û��ʻ�������Լ���ѯ����־���û������Բ�ѯһ��ʱ�����ԵĲ�������ˮ����</td>
    		</tr>
    		<tr>
    			<td width="40">ת�ʣ�</td>
    			<td>�û�ת��ʱ��Ҫ�ж��Ƿ��ܹ�����ת�������������㣬��Ҫ�������ṩת���ʻ����˺š����֤���롢������û����Ƽ�ת���ʻ����ʺź��û����ƣ��û�Ҳ��Ҫ�ṩת�˽�ϵͳ���潫����ת����־��ת���û��ʻ���������Ϣ��</td>
    		</tr>
    		<tr>
    			<td width="40">�����룺</td>
    			<td>�û������и�����ʱ��Ҫ�������ṩ���֤���ʺš��Լ����룬���������µ����롣ϵͳ���潫���������־��</td>
    		</tr>
    		<tr>
    			<td width="40">������</td>
    			<td>�û�����������ʱ��Ҫ�������ṩ���֤���ʺ��Լ����룬���Ұ��ʻ������ȫ��ȡ����ϵͳ���潫���������־��</td>
    		</tr>
    	</table>
    	<table>
    		<tr>
    			<td width="80"> <h1></h1></td>
    			<td><h2>����ҵ��</h2></td>
    		</tr>
    		<tr>
    			<td></td>
    			<td><input type="button" value="����" style="width:120px" onclick="window.location.href='openaccount.jsp'"/></td>
    			<td><input type="button" value="���" style="width:120px" onclick="window.location.href='savemoney.jsp'"/></td>
    			<td><input type="button" value="ȡ��" style="width:120px" onclick="window.location.href='withdrawmoney.jsp'"/></td>
    			<td><input type="button" value="��ѯ" style="width:120px" onclick="window.location.href='query.jsp'"/></td>
    		</tr>
    		<tr>
    			<td></td>
    			<td><input type="button" value="ת��" style="width:120px" onclick="window.location.href='transfermoney.jsp'"/></td>
    			<td><input type="button" value="�޸�����" style="width:120px" onclick="window.location.href='resetpassword.jsp'"/></td>
    			<td><input type="button" value="����" style="width:120px" onclick="window.location.href='deleteaccount.jsp'"/></td>
    		</tr>
    		<tr>
    			<td width="80"> <h1></h1></td>
    			<td ><h2>����ҵ��</h2></td>
    		</tr>
    		<tr>
    			<td></td>
    			<td><input type="button" value="��־" style="width:120px" onclick="window.location.href='log'"/></td>
    			<td><input type="button" value="��ӹ���Ա" style="width:120px" onclick="window.location.href='addadmin.jsp'"/></td>
    			<td><input type="button" value="��ӹ�Ա" style="width:120px" onclick="window.location.href='addemployee.jsp'"/></td>
    			<td><input type="button" value="�����ҵ�˻�����Ա" style="width:120px" onclick="window.location.href='addenterpriseadmin.jsp'"/></td>
    		</tr>	
    	</table>
  </body>
</html>
