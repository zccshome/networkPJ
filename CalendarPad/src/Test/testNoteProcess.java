package Test;

import java.util.ArrayList;

import noteProcess.*;

public class testNoteProcess {
	public static void main(String[] args) {
		noteProcess np = new noteProcess();
		ArrayList<Reminder> res = np.reach();
		System.out.println("----本地----");
		for(Reminder rd: res) {
			System.out.println(rd.getYear() + " " + rd.getMonth() + " " + rd.getDay() + " " + rd.getContent());
		}
		np.backup(res);
		np.download();
		res = np.reach();
		System.out.println("----同步后----");
		for(Reminder rd: res) {
			System.out.println(rd.getYear() + " " + rd.getMonth() + " " + rd.getDay() + " " + rd.getContent());
		}
	}
}
