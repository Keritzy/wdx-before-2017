package wdx.bms.tools;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import wdx.bms.model.Admin;
import wdx.bms.model.Employee;
import wdx.bms.util.FileUtil;
import wdx.bms.util.FolderUtil;

public class AddEmployeeServlet extends HttpServlet {

	/**
	 * Constructor of the object.
	 */
	public AddEmployeeServlet() {
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
		out.println("  <HEAD><TITLE>A Servlet</TITLE></HEAD>");
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
			throws ServletException, IOException 
	{
		// 获取对应的数值，并创建对象，写入到文件中
		request.setCharacterEncoding("UTF-8");
		String name = request.getParameter("name");
		String pwd = request.getParameter("pwd");
		String number = request.getParameter("number");
		String id = request.getParameter("id");
		String department = request.getParameter("department");
		String type = request.getParameter("type");
		
		Employee e = new Employee();
		e.setName(name);
		e.setPassword(pwd);
		e.setNumber(number);
		e.setId(Integer.parseInt(id));
		e.setDepartment(department);
		e.setPosition(type);
		
		// 获取真实路径
		String realPath = this.getServletContext().getRealPath("index.jsp");
		int end = realPath.lastIndexOf("/");
		realPath = realPath.substring(0, end+1);
				
		String folder = realPath + "EmployeeData";
		String message = "";
		
		// 创建文件夹
		FolderUtil.createFolder(folder);
		String filepath = folder + "/" + id;
		FileUtil.writeObject(filepath, e);
		

		// 尝试读取雇员列表并输出
		
		List<String> fileList = FileUtil.getAllFiles(folder);		
		for (int i = 1; i <= fileList.size(); i++)
		{
			Employee aread = FileUtil.readEmployee(folder + "/" + fileList.get(i-1));
			
			message += "<tr><td>雇员" + i + ":</td>";
			message += "<td>"+ aread.getId() + "/" + aread.getName() + "/" + aread.getNumber() + "/" + aread.getPosition() + "/" + aread.getDepartment() +  "</td></tr>";
		}		

		response.setContentType("text/html");
		// 防乱码
		response.setCharacterEncoding("UTF-8");
		response.setHeader("content-type","text/html;charset=UTF-8");
		
		PrintWriter out = response.getWriter();
		out.println("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
		out.println("<HTML>");
		out.println("  <HEAD><TITLE>添加雇员操作结果</TITLE></HEAD>");
		out.println("  <BODY>");
		out.println("<table align=\"center\">");
		out.println("<tr><td colspan=\"2\"> <h1>添加雇员操作完成，当前的雇员列表为</h1></td></tr>");
		out.println("<tr><td colspan=\"2\"> <h2>文件存储中的数据</h2></td></tr>");
		out.println("<tr><td colspan=\"2\">ID/姓名/账号/类型/部门</td></tr>");
		out.println(message);
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
		
			stmt.execute("insert into employee values (" + e.getId()+ ",'" + e.getName() + "','" + e.getNumber() + "','" + e.getPassword() + "','" + e.getDepartment() + "','" + e.getPosition() + "')");

			// 读取输入最新的记录
			rs = stmt.executeQuery("select * from employee");
			int i = 1;
			while (rs.next())
			{
				out.println("<tr><td>雇员" + i + ":</td>");
				out.println("<td>" + rs.getInt("id") + "/" + rs.getString("name") + "/" + rs.getString("number") + "/" + rs.getString("department") + "/" + rs.getString("position") + "</td></tr>");
				i++;
			}  
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
