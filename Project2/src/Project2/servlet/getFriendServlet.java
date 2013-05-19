package Project2.servlet;

import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Project2.bll.UserAction;

/**
 * Servlet implementation class getFriendServlet
 */
public class getFriendServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public getFriendServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		String idS = request.getParameter("id");
		String ret = "";
		try
		{
			ArrayList<String[]> aaa = UserAction.getFriendPerson(Integer.parseInt(idS));
			for(int i = 0; i < aaa.size(); i++)
			{
				ret += "<div class='friends'>";
				ret += "<a href='../Project2/Log_html.jsp'>";
				ret += "<img src='Pic/"+aaa.get(i)[0]+".png'/>";
				ret += "</a>";
				ret += "<div>"+aaa.get(i)[1]+"</div>";
				ret += "</div>";
			}
		}
		catch (SQLException e)
		{
		}
		response.setContentType("text/html;charset=UTF-8");
		response.getWriter().write(ret);
	}

}
