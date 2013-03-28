package Test;

import utils.*;
import java.util.*;

public class testChinaDate {
	public static void main(String[] args) {
		
		for (int i = 0; i < 100; i++) {
			String[] tmp = getRandomDate();
			System.out.println(tmp[0] + " " +  tmp[1] + " " + tmp[2] + " " + ChinaDate.toChinaDay(tmp[0], tmp[1], tmp[2]));
		}
	}
	
	private static String[] getRandomDate() {
		int off = (int) (Math.random() * 10000);
		boolean direction = Math.random() > 0.5;
		GregorianCalendar today = new GregorianCalendar();
		today.add(Calendar.DAY_OF_YEAR, direction? off: -off);
		return new String[]{Integer.toString(today.get(Calendar.YEAR)), Integer.toString(1 + today.get(Calendar.MONTH)), Integer.toString(today.get(Calendar.DAY_OF_MONTH))};
	}
	
}
