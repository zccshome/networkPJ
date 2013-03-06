package noteProcess;

import java.sql.SQLException;

import GUI.NotePanel;
import calenderProcess.MyCalender;

public class SaveReminderProcess extends Thread {

	public void run(){

		String note=NotePanel.noteArea.getText();
		if ( note == null || note.equals("") )
			return ;
		

		int yearInt = MyCalender.year ;
		Byte monthInt = Byte.parseByte(MyCalender.month+"") ;
		Byte dayInt = Byte.parseByte(MyCalender.selectedDay+"");
		
		try {
			Backup_db.addReminder(yearInt, monthInt, dayInt, Encryption.encryption(note)/*Encryption.encryption(note)*/);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			//e.printStackTrace();
			System.err.println("无法连接远程数据库！");
		}
		
	}
}
