package Project2.servlet;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.sql.Timestamp;

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
public class RegServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public RegServlet() {
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
		String nickname = request.getParameter("nickname");
		String password = request.getParameter("password1");
		String comefrom = request.getParameter("from");
		//System.out.println(username);
		//System.out.println(nickname);
		//System.out.println(password);
		//System.out.println(comefrom);
		User user = new User();
		user.setId(1);
		user.setNickname(nickname);
		user.setUsername(username);
		user.setComefrom(comefrom);
		user.setRegTime(new Timestamp(System.currentTimeMillis()));
		user.setPassword(password);
		int success = 1;
		try
		{
			//处理注册请求
			success = UserAction.Reg(user);
		}
		catch (Exception e)
		{
			//数据库出问题
			request.getSession().setAttribute("wrong", 2);
			response.sendRedirect("../Project2/Reg_html.jsp");
		}
		if(success == 0)
		{
			/*try
			{
				user = UserAction.Login(username, password);
			}
			catch(SQLException e)
			{}*/
			//注册成功！开始换默认头像操作！
			request.getSession().setAttribute("user", user);
			int id = user.getId();
			String oldPath = "C:/Users/asus/Documents/Eclispe/.metadata/.plugins/org.eclipse.wst.server.core/tmp0/wtpwebapps/Project2/Pic/default.png";
			String newPath = "C:/Users/asus/Documents/Eclispe/.metadata/.plugins/org.eclipse.wst.server.core/tmp0/wtpwebapps/Project2/Pic/"+id+".png";
			copyFile(oldPath, newPath);
			response.sendRedirect("../Project2/Main.jsp?"+user.getId()+"&type=2&page=1");
		}
		else if(success == 2)
		{
			//发现用户名重复！
			request.getSession().setAttribute("wrong", 1);
			response.sendRedirect("../Project2/Reg_html.jsp");
		}
		else
		{
			//数据库出错！
			request.getSession().setAttribute("wrong", 2);
			response.sendRedirect("../Project2/Reg_html.jsp");
		}
	}
	public static void copyFile(String oldPath, String newPath)
	{ 
		try
		{ 
			int bytesum = 0; 
			int byteread = 0; 
			File oldfile = new File(oldPath); 
			if (oldfile.exists())
			{ //文件存在时 
				InputStream inStream = new FileInputStream(oldPath); //读入原文件 
				FileOutputStream fs = new FileOutputStream(newPath); 
				byte[] buffer = new byte[1444]; 
				//int length; 
				while ( (byteread = inStream.read(buffer)) != -1)
				{ 
					bytesum += byteread; //字节数 文件大小 
					//System.out.println(bytesum); 
					fs.write(buffer, 0, byteread); 
				} 
				inStream.close(); 
			} 
		 } 
		 catch (Exception e)
		 { 
		 }
	}
}
