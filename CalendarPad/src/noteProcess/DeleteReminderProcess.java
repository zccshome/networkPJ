package noteProcess;

import java.sql.SQLException;

import calenderProcess.MyCalender;

public class DeleteReminderProcess extends Thread {

	public void run(){

		int yearInt = MyCalender.year ;
		Byte monthInt = Byte.parseByte(MyCalender.month+"") ;
		Byte dayInt = Byte.parseByte(MyCalender.selectedDay+"");

		try {
			Backup_db.deleteReminder(yearInt, monthInt, dayInt);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			//e.printStackTrace();
			System.err.println("无法连接远程数据库！");
		}
	}
}
