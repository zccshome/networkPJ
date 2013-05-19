package Project2.servlet;

import java.io.IOException;
import java.sql.SQLException;

import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Project2.bll.User;
import Project2.bll.UserAction;

/**
 * Servlet implementation class LoginServlet
 */
public class LoginServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public LoginServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see Servlet#init(ServletConfig)
	 */
	public void init(ServletConfig config) throws ServletException {
		// TODO Auto-generated method stub
	}

	/**
	 * @see Servlet#destroy()
	 */
	public void destroy() {
		// TODO Auto-generated method stub
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		request.setCharacterEncoding("UTF-8");
		String username = request.getParameter("account");
		String password = request.getParameter("password");
		//System.out.println(username);
		//System.out.println(password);
		User user = null;
		try {
			user = UserAction.Login(username, password);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			//数据库端出错
			request.getSession().setAttribute("wrong", 1);
			response.sendRedirect("../Project2/Log_html.jsp");
		}
		if(user != null)
		{
			//查到用户信息，登陆成功设置USER SESSION
			request.getSession().setAttribute("user", user);
			response.sendRedirect("../Project2/Main.jsp?id="+user.getId()+"&type=2&page=1");
		}
		else
		{
			//出错设置
			request.getSession().setAttribute("wrong", 1);
			response.sendRedirect("../Project2/Log_html.jsp");
		}
	}

}
