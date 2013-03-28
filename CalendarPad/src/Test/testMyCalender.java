package Test;

import noteProcess.Reminder;
import calenderProcess.*;

public class testMyCalender {
	public static void main(String[] args) {
		MyCalender mc = new MyCalender();
		mc.refreshDaysArray();
		OneDay[] ods = mc.getDaysArray();
		for(OneDay od: ods) {
			System.out.println(od.getDayText() + " " + od.getDayTitle() + " " + (od.getHasReminder() ? showReminder(od) : ""));
		}
		
		
	}
	
	private static String showReminder(OneDay od) {
		Reminder rd = od.getReminder();
		return rd.getYear() + " " + rd.getMonth() + " " + rd.getDay() + rd.getContent();
	}
}
