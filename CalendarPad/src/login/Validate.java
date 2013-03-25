package login;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import noteProcess.Backup_db;
import noteProcess.SyncReminderProcess;
import utils.md5;


/**
 *1. check if the program can connect with the server.
 * 2. validate the user's input of username and password.
 * 3. synchronize the reminder with the server 
 * @author phobes
 *
 */
public class Validate {
	private static final String connectionString = "jdbc:mysql://10.131.228.247:3306/sepj1";
	private static final String dbUsername = "root";
	private static final String dbPassword = "root";
	private static final String searchUser = "SELECT user_id,password FROM user where user_name = ?";
	private static Connection con = null;
	
	
	/**
	 * validate the username and password
	 * @param username
	 * @param userpass
	 * @return
	 */
	public static boolean isValidate(String username, String userpass){
		
		//if can connect with the server
		if(hasNetwork()){
			PreparedStatement ps = null;
			try
			{
				ps = con.prepareStatement(searchUser);
				ps.setString(1, username);
				ResultSet result = ps.executeQuery();
				if(result.next()){
					String searchedPass = result.getString("password");
					if(searchedPass != null && md5.encryptMD5(userpass).equals(searchedPass)){
						int userid = result.getInt("user_id");///////////////////////////////////
						System.out.println("userid: " + userid);
						System.out.println("Internet已登录");
						Backup_db.user_id = userid;
						//synchronize with the server
						SyncReminderProcess sync = new SyncReminderProcess() ;
						sync.start() ;
						
						return true;
					}
				}
			}catch(Exception e){
				e.printStackTrace();
				return false;
			}
			return false;
		}
		else{
			//check pass via local file if cannot connect with the server
			File file = new File("config/userpass");
			BufferedReader reader = null;
			String pass = null;
			String [] namepass;
			try{
				reader = new BufferedReader( new FileReader(file) );
				String tempStr = null;
				while((tempStr = reader.readLine()) != null ){
					namepass = tempStr.split(" ");
					if(namepass[0].equals(username)){
						pass = namepass[1];
						break;
					}
				}
				reader.close();
			}catch(IOException e){
				e.printStackTrace();
			}finally{
				if (reader != null) {
	                try {
	                    reader.close();
	                } catch (IOException e1) {
	                }
	            }
			}
			if(pass != null && pass.equals(md5.encryptMD5(userpass))){
				System.out.println("本地已登录");
				return true;
			}
			return false;
		}
	}
	
	
	/**
	 * check whether can connect with the server
	 * @return
	 */
	private static  boolean hasNetwork(){
		try{
			Class.forName("com.mysql.jdbc.Driver");
			con= DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		}catch(SQLException e){
			return false;
		} catch (ClassNotFoundException e) {
			return false;
		}
		return true;
	}
	
	
}
