package Project2.servlet;

import java.io.IOException;

import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Project2.bll.UserAction;

/**
 * Servlet implementation class zhuanfaServlet
 */
public class zhuanfaServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public zhuanfaServlet() {
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
		String txt = request.getParameter("fabu");
		String idpublisherS = request.getParameter("idpublisher");
		String idoriginalmblogS = request.getParameter("idoriginalmblog");
		//String id = request.getParameter("idd");
		//System.out.println(idoriginalmblog);
		String type= request.getParameter("type");
		int idpublisher = Integer.parseInt(idpublisherS);
		int idoriginalmblog = Integer.parseInt(idoriginalmblogS);
		try
		{
			//txt = txt+"//转自"+UserAction.getNickNameById(Integer.parseInt(id))+"：";
			//如果转发微博大于140截掉
			if(txt.length()>140)
				txt = txt.substring(0,140);
			boolean ans = UserAction.insertWeibo2(txt, idpublisher, idoriginalmblog);
			if(ans==true)
			{
				response.sendRedirect("../Project2/Main.jsp?id="+idpublisher+"&type="+type+"&page=1");
			}
			else
			{
				//转发失败设置报错
				request.getSession().setAttribute("wrong1", "zhuanfawrong");
				response.sendRedirect("../Project2/Main.jsp?id="+idpublisher+"&type="+type+"&page=1");
			}
		}
		catch(Exception e)
		{
			//数据库出错设置报错！
			request.getSession().setAttribute("wrong1", "zhuanfawrong");
			response.sendRedirect("../Project2/Main.jsp?id="+idpublisher+"&type="+type+"&page=1");
		}
	}

}
