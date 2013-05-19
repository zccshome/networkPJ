package Project2.servlet;

import java.io.IOException;
import java.util.ArrayList;

import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Project2.bll.UserAction;

/**
 * Servlet implementation class ffServlet
 */
public class ffServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public ffServlet() {
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
		int id = Integer.parseInt(request.getParameter("id"));
		String type = request.getParameter("type");
		//System.out.println(""+id+"zcc"+type);
		if(type.equals("1"))
		{
			//代表需要得到好友列表
			try
			{
				ArrayList<String[]> personInfo = UserAction.getFriendPerson(id);
				request.getSession().setAttribute("personInfo", personInfo);
				response.sendRedirect("Person.jsp?id="+id);
			}
			catch(Exception e)
			{
				//出错设置提醒
				request.getSession().setAttribute("personInfo", new ArrayList<String[]>());
				response.sendRedirect("Person.jsp?id="+id);
			}
		}
		else if(type.equals("2"))
		{
			//代表需要得到粉丝列表
			try
			{
				ArrayList<String[]> personInfo = UserAction.getFanPerson(id);
				request.getSession().setAttribute("personInfo", personInfo);
				response.sendRedirect("Person.jsp?id="+id);
			}
			catch(Exception e)
			{
				//出错设置提醒
				request.getSession().setAttribute("personInfo", new ArrayList<String[]>());
				response.sendRedirect("Person.jsp?id="+id);
			}
		}
	}

}
