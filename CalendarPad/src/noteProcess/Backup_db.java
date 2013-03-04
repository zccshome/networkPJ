package noteProcess;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.sql.*;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;

public class Backup_db
{
	//private static final String connectionString = "jdbc:mysql://localhost:3306/sepj1";
	private static final String connectionString = "jdbc:mysql://10.131.228.247:3306/sepj1";
	//username for mysql, 'root' by default.
	private static final String dbUsername = "root";
	//user password for mysql, change to yours.
	private static final String dbPassword = "root";
	
	private static final String getReminder = "SELECT year,month,day,content FROM reminder where user_id=1";
	private static final String addReminder = "INSERT INTO reminder(user_id,year,month,day,content,reminder_type,up_date) values(1,?,?,?,?,0,?)";
	private static final String deleteReminder = "DELETE FROM reminder WHERE year=? and month=? and day=?";
	private static final String updateReminder = "UPDATE reminder SET content= ? , up_date = ? where year=? and month=? and day=?";
	private static final String updateDate = "UPDATE reminder SET up_date = ? where year=? and month=? and day=?";
	private static final String getDate = "SELECT reminder_id,content,up_date FROM reminder where year=? and month=? and day=?";
	
	static
	{
		try {
			//class name for mysql driver
			Class.forName("com.mysql.jdbc.Driver");
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public static int addReminder(int year, byte month, byte day, String content) throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		try
		{
			ps = c.prepareStatement(getDate);
			ps.setInt(1, year);
			ps.setByte(2, month);
			ps.setByte(3, day);
			ResultSet newid = ps.executeQuery();
			if (!newid.next())
			{
				SimpleDateFormat sd = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
				Date d = new Date(Calendar.getInstance().getTimeInMillis());
				String ddate = sd.format(d);
				ps = c.prepareStatement(addReminder);
				//System.out.println(year + "\n"+month+"\n"+day+"\n"+content);
				ps.setInt(1, year);
				ps.setByte(2, month);
				ps.setByte(3, day);
				ps.setString(4, content);
				ps.setString(5, ddate);
				ps.executeUpdate();
			}
			else
			{
				String contentWeb = newid.getString("content");
				SimpleDateFormat sd = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
				String dweb = newid.getString("up_date");
				Date d = new Date(Calendar.getInstance().getTimeInMillis());
				String ddate = sd.format(d);
				
				String yearS = ""+year;
				String monthS = ""+month;
				String dayS = ""+day;
				if (monthS.length()==1)
					monthS = "0"+monthS;
				if (dayS.length()==1)
					dayS = "0"+dayS;
				String filepath = "src\\diary\\";
				String postfix = ".bak";
				String filename = filepath+yearS+monthS+dayS+postfix;
				File file = new File(filename);
				String dnow = sd.format(file.lastModified());
				
				if(dweb.compareTo(dnow) <= 0 && !content.equals(contentWeb))
				{
					//System.out.println(content+contentWeb+ddate);
					ps = c.prepareStatement(updateReminder);
					//System.out.println(year + "\n"+month+"\n"+day+"\n"+content);
					ps.setString(1, content);
					ps.setString(2, ddate);
					ps.setInt(3,year);
					ps.setByte(4, month);
					ps.setByte(5, day);
					ps.executeUpdate();
				}
				else if(dweb.compareTo(dnow) <= 0 && content.equals(contentWeb))
				{
					ps = c.prepareStatement(updateDate);
					//System.out.println(year + "\n"+month+"\n"+day+"\n"+content);
					ps.setString(1, ddate);
					ps.setInt(2, year);
					ps.setByte(3,month);
					ps.setByte(4,day);
					ps.executeUpdate();
				}
				else if(dweb.compareTo(dnow) > 0)
				{
					try
					{
						FileWriter f=new FileWriter(filename);
						f.write(contentWeb);  
						f.close();
						ps = c.prepareStatement(updateDate);
						//System.out.println(year + "\n"+month+"\n"+day+"\n"+content);
						ps.setString(1, ddate);
						ps.setInt(2,year);
						ps.setByte(3,month);
						ps.setByte(4,day);
						ps.executeUpdate();
						//filename = "src\\diary\\";
						/*try {
							int aa = Backup_db.addReminder(yearInt, monthInt, dayInt, note);
							System.out.println(aa);
						} catch (SQLException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}*/
					}
					catch(IOException e)
					{
						System.out.println(e.getMessage());
					}
				}
				//System.out.println(dnow+"\n"+dweb);
			}
			return 1;
		}
		catch (SQLException e)
		{
			e.printStackTrace();
			return -1;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	
	public static ArrayList<Reminder> getReminder() throws SQLException
	{
		Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
		PreparedStatement ps = null;
		ArrayList<Reminder> list = new ArrayList<Reminder>();
		try
		{
			ps = c.prepareStatement(getReminder);
			ResultSet newid = ps.executeQuery();
			while (newid.next())
			{
				Reminder reminder = new Reminder(
						newid.getInt("year"),
						newid.getByte("month"),
						newid.getByte("day"),
						newid.getString("content"));
				list.add(reminder);
			}
			return list;
		}
		catch (SQLException e)
		{
			e.printStackTrace();
			return null;
		}
		finally
		{
			if (c != null)
			{
				c.close();
			}
		}
	}
	public static void deleteReminder(int year, byte month, byte day) throws SQLException
	{
		try 
		{
			Connection c = DriverManager.getConnection(connectionString, dbUsername, dbPassword);
			PreparedStatement ps = null;
			ps = c.prepareStatement(deleteReminder);
			ps.setInt(1, year);
			ps.setByte(2, month);
			ps.setByte(3, day);
			ps.executeUpdate();
		}
		catch(Exception e)
		{
			
		}
	}
	/*public static void main(String[] args)
	{
		try {
			ArrayList<Reminder> temp = Backup_db.getReminder();
			for(Reminder r:temp)
			{
				System.out.println(r.getYear()+"\n"+r.getMonth()+"\n"+r.getDay()+"\n"+r.getContent());
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}*/
}
