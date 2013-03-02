package MyPackage;

import java.util.*;
import java.awt.Color;
import java.awt.event.*;
//import java.io.BufferedReader;
import java.io.File;

import noteProcess.noteProcess;
//import java.io.FileReader;


public class ShowCalender implements ActionListener
{
	static int year,month,day;
	
	public static void  showCalendar(GregorianCalendar gCalendar)      //显示日历
	{
		// 得到文件夹所有的文件名
		File file = new File("src\\diary\\");
		String fileList[] = file.list();

		String postfix = ".bak";

		int size = 0;
		if (fileList != null) {
			size = fileList.length;
		}
		
		for(int i=0;i<42;i++) {                  //清除上次日历
			CalendarPad.jtf[i].setText(""); 
	    	CalendarPad.jtf[i].setBackground(Color.cyan);//设置所有text的背景色为青色
		}
		year=gCalendar.get(Calendar.YEAR);
		month=(gCalendar.get(Calendar.MONTH)+1);
	    day=gCalendar.get(Calendar.DATE);
		CalendarPad.jtf1.setText(""+year);
		CalendarPad.jtf2.setText(""+month);
		CalendarPad.jtf3.setText(""+day);
		GregorianCalendar cal=new GregorianCalendar(year,month-1,1);
		int d=cal.get(Calendar.DAY_OF_WEEK);     //d为当月第一天在一周中的位置
		int maxday;   //当月最大日期
		if(month==1||month==3||month==5||month==7||month==8||month==10||month==12)
			maxday=31;
		else 
			maxday=30;
		if(month==2 && gCalendar.isLeapYear(year))
			maxday--;
		if(month==2 && !gCalendar.isLeapYear(year))
			maxday=28;
		for(int i=d-1, j=1;j<=maxday;i++,j++)    //显示新日历
		{
			CalendarPad.jtf[i].setText(""+j);
			
			//得到标签上面的文本
			String dt = CalendarPad.jtf[i].getText();

			if (dt.length() == 1) {
				dt = "0" + dt;
			}			
			//CalendarPad.jtf[i].setBackground(Color.cyan);//设置所有text的背景色为青色	
			// 设置标记
			for (int k = 0; k < size; k++) {
				String date = CalendarPad.jtf1.getText()
						+ CalendarPad.jtf2.getText() + dt + postfix;
				String fileName = fileList[k];
				if (fileName.equals(date)) {
					CalendarPad.jtf[i].setBackground(Color.yellow);
				}
			}

		}
		CalendarPad.jla.setText("                 "+year+" 年 "+month+" 月 "+day+" 日 ");  

	}

	public void actionPerformed(ActionEvent ae)
	{
		if(ae.getActionCommand()=="上年")
			year--;
		else if(ae.getActionCommand()=="下年")
			year++;
		else if(ae.getActionCommand()=="上月")
			month--;
		else if(ae.getActionCommand()=="下月")
			month++;
		showCalendar(new GregorianCalendar(year,month-1,day));
		
	}

}
