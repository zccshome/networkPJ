package Project2.servlet;

import java.io.IOException;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Project2.bll.UserAction;

/**
 * Servlet implementation class fabuPinglunServlet
 */
public class fabuPinglunServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public fabuPinglunServlet() {
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
		//如果评论字数超过140，截掉之
		String txt = request.getParameter("fabu");
		if(txt.length() > 140)
			txt = txt.substring(0, 140);
		String idpublisherS = request.getParameter("idpublisher");
		String idtargetmblogS = request.getParameter("idtargetmblog");
		String returnS = request.getParameter("returnid");
		int idpublisher = Integer.parseInt(idpublisherS);
		int idtargetmblog = Integer.parseInt(idtargetmblogS);
		String type= request.getParameter("type");
		String page= request.getParameter("page");
		if(txt.length() == 0)
		{
			//SERVLET端判断是不是空评论，是的话直接返回
			request.setAttribute("word", 0);
			response.sendRedirect("../Project2/Main.jsp?id="+returnS+"&type="+type+"&page="+page);
			return;
		}
		try
		{
			//不是空评论的话连接数据库
			boolean ans = UserAction.insertPinglun(txt, idpublisher, idtargetmblog);
			if(ans==true)
			{
				response.sendRedirect("../Project2/Main.jsp?id="+returnS+"&type="+type+"&page="+page);
			}
			else
			{
				request.getSession().setAttribute("wrong1", "pinglunwrong");
				response.sendRedirect("../Project2/Main.jsp?id="+returnS+"&type="+type+"&page="+page);
			}
		}
		catch(Exception e)
		{
			//出错的话设置提醒的SESSION,页面端会有处理
			request.getSession().setAttribute("wrong1", "pinglunwrong");
			response.sendRedirect("../Project2/Main.jsp?id="+returnS+"&type="+type+"&page="+page);
		}
		//System.out.println(id+txt);
	}

}
