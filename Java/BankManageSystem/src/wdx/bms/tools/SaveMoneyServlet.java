package wdx.bms.tools;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Calendar;

import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import wdx.bms.model.Account;
import wdx.bms.model.Employee;
import wdx.bms.model.Log;
import wdx.bms.util.FileUtil;
import wdx.bms.util.FolderUtil;
import wdx.bms.util.SearchUtil;

public class SaveMoneyServlet extends HttpServlet {

	/**
	 * Constructor of the object.
	 */
	public SaveMoneyServlet() {
		super();
	}

	/**
	 * Destruction of the servlet. <br>
	 */
	public void destroy() {
		super.destroy(); // Just puts "destroy" string in log
		// Put your code here
	}

	/**
	 * The doGet method of the servlet. <br>
	 *
	 * This method is called when a form has its tag value method equals to get.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		response.setContentType("text/html");
		PrintWriter out = response.getWriter();
		out.println("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
		out.println("<HTML>");
		out.println("  <HEAD><TITLE>存款操作结果</TITLE></HEAD>");
		out.println("  <BODY>");
		out.print("    This is ");
		out.print(this.getClass());
		out.println(", using the GET method");
		out.println("  </BODY>");
		out.println("</HTML>");
		out.flush();
		out.close();
	}

	/**
	 * The doPost method of the servlet. <br>
	 *
	 * This method is called when a form has its tag value method equals to post.
	 * 
	 * @param request the request send by the client to the server
	 * @param response the response send by the server to the client
	 * @throws ServletException if an error occurred
	 * @throws IOException if an error occurred
	 */
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {

		// 获取对应的数值，并创建对象，写入到文件中
		request.setCharacterEncoding("UTF-8");
		
		String pwd = request.getParameter("pwd");
		String number = request.getParameter("number");		
		String amount = request.getParameter("amount");
	
		String message = "";
		String operator = "";
		String operatorType = "";
		// 获取Cookie信息
		Cookie cklist[] = request.getCookies();
		if (cklist != null && cklist.length > 0)
		{
			for(Cookie c:cklist)
			{
				if (c.getName().equals("name"))
				{
					operator = java.net.URLDecoder.decode(c.getValue(),"UTF-8");
					message += "<tr><td>操作人: </td>";
					message += "<td>" + operator + "</td></tr>";
				}
				
				if (c.getName().equals("type"))
				{
					operatorType = java.net.URLDecoder.decode(c.getValue(),"UTF-8");
					message += "<tr><td>雇员类型: </td>";
					message += "<td>" + operatorType + "</td></tr>";
				}
				
			}
		}
		
		
		// 获取真实路径
		String realPath = this.getServletContext().getRealPath("index.jsp");
		int end = realPath.lastIndexOf("/");
		realPath = realPath.substring(0, end+1);
						
		String folder = realPath + "AccountData";
		
		Log l = new Log();
		Account a = SearchUtil.getAccountByName(number, folder);
		if (a != null)
		{
			// 若找到匹配的账户，则重新写入各项信息并保存
			
			double oldmoney = Double.valueOf(a.getAmount());
			double delta = Double.valueOf(amount);
			double newmoney = oldmoney + delta;
			a.setAmount(String.valueOf(newmoney));
			String filepath = folder + "/" + a.getId();
			FileUtil.writeObject(filepath, a);
		
			message += "<tr><td>账户: " + a.getNumber() + "</td>";
			message += "<tr><td>余额: " + String.valueOf(newmoney) + "</td></tr>";
			
			// 写入日志
			String efolder = realPath + "EmployeeData";
			Employee e = SearchUtil.getEmployeeByName(operator, efolder);
			
			
			if (e != null)
			{
				l.setName(operator);
				l.setNumber(e.getNumber());
				l.setDepartment(e.getDepartment());
				l.setPosition(operatorType);
				l.setAccount(a.getNumber());
				l.setOperation("存款");
				l.setOpdetail("余额:" + String.valueOf(newmoney));
				
				
				String lfolder = realPath + "LogData";
				FolderUtil.createFolder(lfolder);
				
				Calendar c = Calendar.getInstance();
				String dix = String.valueOf(c.hashCode());
				
				String lfilepath = lfolder + "/" + l.getName() + dix;
				FileUtil.writeObject(lfilepath, l);

			}
			else
			{
				message += "没有对应雇员";
			}
			
			
		}
		else
		{
			message += "没有找对对应的账户，请检查后重试";
			
		}
		
				
		response.setContentType("text/html");
		
		// 防乱码
		response.setCharacterEncoding("UTF-8");
		response.setHeader("content-type","text/html;charset=UTF-8");
				
		PrintWriter out = response.getWriter();
		out.println("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
		out.println("<HTML>");
		out.println("  <HEAD><TITLE>存款操作结果</TITLE></HEAD>");
		out.println("  <BODY>");
		out.println("<table align=\"center\">");
		out.println("<tr><td colspan=\"2\"> <h1>存款结果</h1></td></tr>");
		out.println("<tr><td colspan=\"2\"> <h2>文件存储中的数据</h2></td></tr>");
		out.print(message);
		
		out.println("<tr><td colspan=\"2\"> <h2>数据库中的数据</h2></td></tr>");
		Connection conn=null;
        Statement stmt=null;
        ResultSet rs = null;
        
        
		try 
		{
			Class.forName("com.mysql.jdbc.Driver");
			String url = "jdbc:mysql://localhost:3306/bms";
			conn = DriverManager.getConnection(url, "root", "wang33");
			stmt = conn.createStatement();
		
			
			// 读取输入最新的记录
			stmt.executeUpdate("update account set amount = '" + a.getAmount()  +"' where id = " + a.getId());
			rs = stmt.executeQuery("select * from account where id = " + a.getId());
			
			rs.next();
			out.println("<tr><td>存款余额:</td>");
			out.println("<td>" + rs.getString("amount") +  "</td></tr>");
			 
			
			Calendar c = Calendar.getInstance();
			// 写入日志
			stmt.execute("insert into log values (" + c.hashCode()+ ",'" + l.getName() + "','" + l.getNumber() + "','" + l.getDepartment() + "','" + l.getPosition() + "','" + l.getAccount() + "','" + l.getOperation() + "','" + l.getOpdetail()  + "')");
		} 
		catch (SQLException ee) 
		{
		   // TODO Auto-generated catch block
			out.println("SQLException"+ ee.toString()+ "<br/>");
		}
		catch(ClassNotFoundException ce)
        {
			out.println("加载数据库驱动失败<br/>");
			out.println(ce.toString()+ "<br/>");
        }
		
		
		out.println("<tr><td colspan=\"2\"><input type=\"button\" value=\"返回\" onclick=\"javascript:history.go(-1);\"/></td></tr>");
		out.println("</table>");
		out.println("  </BODY>");
		out.println("</HTML>");
		out.flush();
		out.close();
	}

	/**
	 * Initialization of the servlet. <br>
	 *
	 * @throws ServletException if an error occurs
	 */
	public void init() throws ServletException {
		// Put your code here
	}

}
