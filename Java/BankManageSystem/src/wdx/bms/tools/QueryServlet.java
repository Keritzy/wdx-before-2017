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
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import wdx.bms.model.Account;
import wdx.bms.util.FileUtil;
import wdx.bms.util.SearchUtil;

public class QueryServlet extends HttpServlet {

	/**
	 * Constructor of the object.
	 */
	public QueryServlet() {
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
		out.println("  <HEAD><TITLE>��ѯ�������</TITLE></HEAD>");
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

		// ��ȡ��Ӧ����ֵ������������д�뵽�ļ���
		request.setCharacterEncoding("UTF-8");
		
		String pwd = request.getParameter("pwd");
		String number = request.getParameter("number");		
		String identity = request.getParameter("identity");
	
		
		// ��ȡ��ʵ·��
		String realPath = this.getServletContext().getRealPath("index.jsp");
		int end = realPath.lastIndexOf("/");
		realPath = realPath.substring(0, end+1);
						
		String folder = realPath + "AccountData";
		
		String message = "";
		
		Account a = SearchUtil.getAccountByName(number, folder);		
		if (a != null)
		{
			// ���ҵ�ƥ����˻�����������

			message += "<tr><td>�˻�: " + a.getNumber() + "</td>";
			message += "<tr><td>���: " + a.getAmount() + "</td></tr>";
			
		}
		else
		{
			message += "û���ҶԶ�Ӧ���˻������������";
			
		}		
		
		response.setContentType("text/html");
		// ������
		response.setCharacterEncoding("UTF-8");
		response.setHeader("content-type","text/html;charset=UTF-8");
		
		PrintWriter out = response.getWriter();
		out.println("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
		out.println("<HTML>");
		out.println("  <HEAD><TITLE>��ѯ�������</TITLE></HEAD>");
		out.println("  <BODY>");
		out.println("<table align=\"center\">");
		out.println("<tr><td colspan=\"2\"> <h1>��ѯ���</h1></td></tr>");
		out.println("<tr><td colspan=\"2\"> <h2>�ļ��洢�е�����</h2></td></tr>");
		out.print(message);
		
		out.println("<tr><td colspan=\"2\"> <h2>���ݿ��е�����</h2></td></tr>");
		Connection conn=null;
        Statement stmt=null;
        ResultSet rs = null;
        
        
		try 
		{
			Class.forName("com.mysql.jdbc.Driver");
			String url = "jdbc:mysql://localhost:3306/bms";
			conn = DriverManager.getConnection(url, "root", "wang33");
			stmt = conn.createStatement();

			rs = stmt.executeQuery("select * from account where id = " + a.getId());
			
			rs.next();
			out.println("<tr><td>������:</td>");
			out.println("<td>" + rs.getString("amount") +  "</td></tr>");
			
			
		} 
		catch (SQLException ee) 
		{
		   // TODO Auto-generated catch block
			out.println("SQLException"+ ee.toString()+ "<br/>");
		}
		catch(ClassNotFoundException ce)
        {
			out.println("�������ݿ�����ʧ��<br/>");
			out.println(ce.toString()+ "<br/>");
        }
		
		out.println("<tr><td colspan=\"2\"><input type=\"button\" value=\"����\" onclick=\"javascript:history.go(-1);\"/></td></tr>");
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
