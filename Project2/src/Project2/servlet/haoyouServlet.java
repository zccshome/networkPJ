package Project2.servlet;

import java.io.IOException;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Project2.bll.UserAction;

/**
 * Servlet implementation class haoyouServlet
 */
public class haoyouServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public haoyouServlet() {
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
		int source = Integer.parseInt(request.getParameter("id2"));
		int target = Integer.parseInt(request.getParameter("id"));
		String type = request.getParameter("type");
		if(type.equals("2"))
		{
			//代表处理加好友请求
			try
			{
				boolean ans = UserAction.addFriend(source, target);
				if(ans==true)
				{
					response.sendRedirect("../Project2/Main.jsp?id="+target+"&type=2&page=1");
				}
				else
				{
					//出错设置
					request.getSession().setAttribute("wrong1", "addwrong");
					response.sendRedirect("../Project2/Main.jsp?id="+target+"&type=2&page=1");
				}
			}
			catch(Exception e)
			{
				//出错设置
				request.getSession().setAttribute("wrong1", "addwrong");
				response.sendRedirect("../Project2/Main.jsp?id="+target+"&type=2&page=1");
			}
		}
		else if(type.equals("1"))
		{
			//代表是删除好友
			try
			{
				boolean ans = UserAction.deleteFriend(source, target);
				if(ans==true)
				{
					response.sendRedirect("../Project2/Main.jsp?id="+target+"&type=2&page=1");
				}
				else
				{
					//出错设置
					request.getSession().setAttribute("wrong1", "deletewrong");
					response.sendRedirect("../Project2/Main.jsp?id="+target+"&type=2&page=1");
				}
			}
			catch(Exception e)
			{
				//出错设置
				request.getSession().setAttribute("wrong1", "deletewrong");
				response.sendRedirect("../Project2/Main.jsp?id="+target+"&type=2&page=1");
			}
		}
	}

}
