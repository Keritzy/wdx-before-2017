package wdx.bms.tools;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class LoginServlet extends HttpServlet {

	/**
	 * Constructor of the object.
	 */
	public LoginServlet() {
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
			throws ServletException, IOException {

		request.setCharacterEncoding("UTF-8");
		String name = request.getParameter("name");
		String pwd = request.getParameter("pwd");
		String type = request.getParameter("type");
		// ������Ҫһ��ѡ������ְԱ�����ʱ��û��
		//if((name != null && !name.equals(""))&&(pwd != null && !pwd.equals("")))
		{
			if(name.equals("wdx")&&pwd.equals("wdx")&&type != null) // ��ʱ��Ӧ��Ҫ��ȡ���жϣ����ھ���д��
			{
				// ��¼�ɹ�����ת������ѡ��ҳ��
				//request.getRequestDispatcher("functions.jsp").forward(request, response);
				// ��������ȥ����
				
				request.setAttribute("Name", name);
				request.setAttribute("Type", type);
				
				// ����Ϣ�����cookie�У���ҳ���д�ֵ
				name = java.net.URLEncoder.encode(name, "UTF-8");
				type = java.net.URLEncoder.encode(type, "UTF-8");
				
				Cookie ck = new Cookie("name",name);
				Cookie ck1 = new Cookie("type", type);
				ck.setMaxAge(3600);
				ck1.setMaxAge(3600);
				response.addCookie(ck);
				response.addCookie(ck1);
				
				request.getRequestDispatcher("functions.jsp").forward(request, response);
			}
			else
			{
				//ֱ���ض���error.jsp����ʾ����
				response.sendRedirect("error.jsp");
			}
		}
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
