package Project2.servlet;

import java.io.IOException;
import java.sql.SQLException;

import javax.servlet.Servlet;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.jspsmart.upload.File;  
import com.jspsmart.upload.SmartUpload;  
import com.jspsmart.upload.SmartUploadException;  

import Project2.bll.User;

/**
 * Servlet implementation class addPicServlet
 */
public class addPicServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private   ServletConfig   config;
    /**
     * @see HttpServlet#HttpServlet()
     */
    public addPicServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see Servlet#init(ServletConfig)
	 */
	public void init(ServletConfig config) throws ServletException {
		// TODO Auto-generated method stub
		this.config = config;
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
		//用了一个smart upload
		 request.setCharacterEncoding("UTF-8"); 
		 String filePath = "C:/Users/asus/Documents/Eclispe/.metadata/.plugins/org.eclipse.wst.server.core/tmp0/wtpwebapps/Project2/Pic/";
		 //System.out.println(request.getRealPath("/"));
	     //String messages="";
	     //String forward="";
	     String id = ""+((User)request.getSession().getAttribute("user")).getId();
	     SmartUpload su = new SmartUpload();      
	     long maxsize = 2 * 1024 * 1024;// 设置每个上传文件的大小，为2MB  
	     String allowedFilesList = "png";  
	     String denidFilesList = "exe,bat,jsp,htm,html";  
	          
	     try
	     {  
	    	 su.initialize(config, request, response);//初始化
	         su.setMaxFileSize(maxsize);// 限制上传文件的大小  
	         su.setAllowedFilesList(allowedFilesList);// 设置允许上传的文件类型  
	         su.setDeniedFilesList(denidFilesList);     
	         su.upload();// 上传文件  
	         {  
	             File file = su.getFiles().getFile(0);// 获取上传的文件，因为只上传了一个文件，所以可直接获取              
	             if (!file.isMissing())
	             {// 如果选择了文件  
	                 //String now = new Date().getTime() + "";//获取当前时间并格式化为字符串  
	                 String photoAddr=filePath + id + "."+file.getFileExt();//filePath值  
	                 
	                 file.saveAs(photoAddr,File.SAVEAS_AUTO);              
	             }
	             else
	             {  
	                 //messages="请选择要上传的文件！";  
	                 //forward="/Project2/error.jsp";  
	            	 request.getSession().setAttribute("wrong1", "picwrong2");
	             }                  
	         }              
	     }
	     catch (java.lang.SecurityException e)
	     {  
	    	 //messages="<li>上传文件失败！上传的文件类型只允许为：png</li>";  
	         //forward="/Project2/error.jsp";    
	    	 request.getSession().setAttribute("wrong1", "picwrong1");
	     }
	     catch (SmartUploadException e)
	     {  
	         //messages="上传文件失败！";  
	         //forward="/Project2/error.jsp";  
	    	 request.getSession().setAttribute("wrong1", "picwrong");
	     }
	     catch (SQLException e)
	     {  
	    	 request.getSession().setAttribute("wrong1", "picwrong");
	     }                  
	     //request.setAttribute("messages",messages);          
	     response.sendRedirect("Main.jsp?id="+id+"&type=2&page=1");
	 }  
}
