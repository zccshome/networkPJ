package noteProcess;

import java.awt.Color;
import java.awt.event.*;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.sql.SQLException;

import javax.swing.JTextField;

import MyPackage.CalendarPad;
import MyPackage.ShowCalender;

public class noteProcess extends MouseAdapter implements ActionListener
{
	private String filepath = "src\\diary\\";
	private String filename;
	private String postfix = ".bak";

	public void actionPerformed(ActionEvent ae)
	{
		if(ae.getActionCommand()=="保存日志")
		{

			String a=CalendarPad.jtf1.getText();
			StringBuffer b=new StringBuffer(CalendarPad.jtf2.getText());
			StringBuffer c=new StringBuffer(CalendarPad.jtf3.getText());
			StringBuffer date= new StringBuffer();//真实日期
			int yearInt = Integer.parseInt(a);
			byte monthInt = Byte.parseByte(b.toString());
			byte dayInt = Byte.parseByte(c.toString());
			if (b.length()==1)
				b.insert(0, "0");
			if (c.length()==1)
				c.insert(0, "0");
			//filename=filename+a+b+c+postfix;
			filename = new StringBuffer(filepath).append(a).append(b).append(c).append(postfix).toString();
			String note=CalendarPad.jta.getText();
			char[] buffer=new char[note.length()];
			note.getChars(0, note.length(), buffer, 0);
			try
			{
			  FileWriter f=new FileWriter(filename);
			  f.write(buffer);  
			  f.close();
				for (int i = 0; i < CalendarPad.jtf.length; i++) {
					String dt = CalendarPad.jtf[i].getText();
					if (dt.equals(date)) {
						CalendarPad.jtf[i].setBackground(Color.yellow);
					}
				}
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
		else if(ae.getActionCommand()=="删除日志")
		{
			
			String a=CalendarPad.jtf1.getText();
			StringBuffer b=new StringBuffer(CalendarPad.jtf2.getText());
			StringBuffer c=new StringBuffer(CalendarPad.jtf3.getText());
			StringBuffer date= new StringBuffer();//真实日期
			
			if (b.length()==1)
				b.insert(0, "0");
			if (c.length()==1)
				c.insert(0, "0");

			filename = new StringBuffer(filepath).append(a).append(b).append(c).append(postfix).toString();
			
			File f=new File(filename);
			f.delete();
			CalendarPad.jta.setText("");
			for (int i = 0; i < CalendarPad.jtf.length; i++) {
				String dt = CalendarPad.jtf[i].getText();
				if (dt.equals(date)) {
					CalendarPad.jtf[i].setBackground(Color.cyan);
				}
			}
			//filename = "src\\diary\\";
		}
		
	}
	public void mousePressed(MouseEvent me)
	{
		//String filename = "src\\diary\\";
		//String postfix = ".bak";
		
		String year=CalendarPad.jtf1.getText();
		String month=CalendarPad.jtf2.getText();
		JTextField source=(JTextField) me.getSource();
		String day=source.getText();
		CalendarPad.jtf3.setText(day);
		CalendarPad.jla.setText("                 "+year+" 年 "+month+" 月 "+day+" 日 ");
		try
		{
			String a=CalendarPad.jtf1.getText();
			String b=CalendarPad.jtf2.getText();
			String c=CalendarPad.jtf3.getText();
			if (b.length()==1)
				b="0"+b;
			if (c.length()==1)
				c="0"+c;
			FileReader fr=new FileReader(filepath + a + b + c + postfix);
			BufferedReader br=new BufferedReader(fr);
			String s,note=new String();                   //note为日志内容
			while((s=br.readLine())!=null)//读日志文件
			{
				note+=s;
			}
			CalendarPad.jta.setText(note);
			fr.close();
			
		}
		catch(FileNotFoundException e)
		{
			CalendarPad.jta.setText("");
		}
		catch (IOException e)
		{
	
		}
		
	}

}
