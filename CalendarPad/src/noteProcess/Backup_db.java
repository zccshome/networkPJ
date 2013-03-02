package noteProcess;

import java.sql.*;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;

import MyPackage.CalendarPad;
import MyPackage.ShowCalender;

public class Backup_db
{
	private static final String connectionString = "jdbc:mysql://localhost:3306/sepj1";
	//username for mysql, 'root' by default.
	private static final String dbUsername = "root";
	//user password for mysql, change to yours.
	private static final String dbPassword = "111";
	
	private static final String getReminder = "SELECT year,month,day,content FROM reminder where user_id=1";
	private static final String addReminder = "INSERT INTO reminder(user_id,year,month,day,content,reminder_type) values(1,?,?,?,?,0)";
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
			ps = c.prepareStatement(addReminder);
			//System.out.println(year + "\n"+month+"\n"+day+"\n"+content);
			ps.setInt(1, year);
			ps.setByte(2, month);
			ps.setByte(3, day);
			ps.setString(4, content);
			ps.executeUpdate();
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
				Reminder reminder = new Reminder();
				reminder.setYear(newid.getInt("year"));
				reminder.setMonth(newid.getByte("month"));
				reminder.setDay(newid.getByte("day"));
				reminder.setContent(newid.getString("content"));
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
	public static void main(String[] args)
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

	}
}
