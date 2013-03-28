package Test;

import java.sql.SQLException;
import java.util.*;

import noteProcess.*;

public class testBackup_db {
	public static void main(String[] args) throws SQLException {
		//产生一系列的每日事件，测试今天后1000-5000天内的日期，不会产生对今日的影响。
		int length = 10; //测试次数
		GregorianCalendar[] days = new GregorianCalendar[length];
		String[] contents = new String[length];
		System.out.println("----Generated----");
		//产生length组随机数据，显示前上传至服务器
		for (int i = 0; i < length; i++) {
			GregorianCalendar day = getRandomDate();
			days[i] = day;
			String content = generateString();
			contents[i] = content;
			Backup_db.addReminder(day.get(Calendar.YEAR), (byte)(day.get(Calendar.MONTH) + 1), (byte)(day.get(Calendar.DAY_OF_MONTH)), content);
			System.out.println(day.get(Calendar.YEAR) + " " + (day.get(Calendar.MONTH) + 1) + " " + day.get(Calendar.DAY_OF_MONTH) + "\t" + content);
		}
		
		//将这些数据从服务器下载下来，再进行比较
		ArrayList<Reminder> res = Backup_db.getReminder();
		System.out.println("----From Server----");
		for (Reminder rd: res) {
			System.out.println(rd.getYear() + " " + rd.getMonth() + " " + rd.getDay() + "\t" + rd.getContent());
		}
		
		//将测试数据删除
		for (int i = 0; i < length; i++) {
			GregorianCalendar day = days[i];
			String content = contents[i];
			Backup_db.deleteReminder(day.get(Calendar.YEAR), (byte)(day.get(Calendar.MONTH) + 1), (byte)(day.get(Calendar.DAY_OF_MONTH)));
		}
		
		/*GregorianCalendar gc = new GregorianCalendar();
		gc.add(Calendar.DAY_OF_YEAR, 1000);
		
		for (int i = 0; i < 4000; i++) {
			Backup_db.deleteReminder(gc.get(Calendar.YEAR), (byte)(gc.get(Calendar.MONTH) + 1), (byte)(gc.get(Calendar.DAY_OF_MONTH)));
			gc.add(Calendar.DAY_OF_YEAR, 1);
			System.out.println(i);
		}*/
		
		
		
	}
	
	private static GregorianCalendar getRandomDate() {
		GregorianCalendar day = new GregorianCalendar();
		int off = (int) (4000 * Math.random() + 1000);
		day.add(Calendar.DAY_OF_YEAR, off);
		return day;
	}
	
	private static String generateString() {
		StringBuffer sb = new StringBuffer();
		int length = (int) (Math.random() * 50);
		for (int i = 0; i < length; i++) {
			char tmp = (char)(96 * Math.random() + 32);
			sb.append(tmp);
		}
		
		return sb.toString();
	}
	
}
