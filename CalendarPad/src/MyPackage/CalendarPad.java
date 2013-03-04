package MyPackage;

//import java.awt.*;
import java.awt.event.*;
//import java.util.Calendar;
//import java.util.GregorianCalendar;

import javax.swing.*;

//import noteProcess.noteProcess;
import GUI.*;


public class CalendarPad 
{
//	public static JTextField jtf1=new JTextField(4);      //显示年
//	public static JTextField jtf2=new JTextField(2);      //显示月
//	public static JTextField jtf3=new JTextField(2); 
//	public static JTextField[] jtf=new JTextField[42];    //显示日  
//	public static JButton jb1,jb2,jb3,jb4;        //上年 下年 上月 下月 按钮
//	public static JTextArea jta=new JTextArea(3,3);       //记录日志
//	public static JLabel jla=new JLabel("          ");    //显示完整日期
//	
//	public CalendarPad()
//	{		
//		JFrame mainframe=new JFrame("日历记事本");        //主窗口
//		Container cp=mainframe.getContentPane();//获取主窗体的容器
//		cp.setLayout(new GridLayout(1,2));      //布局
//		
//		JPanel leftpanel= new JPanel();  //日历容器
//		leftpanel.setBackground(Color.cyan);
//		leftpanel.setLayout(new BorderLayout());
//		
//		JPanel leftnorth=new JPanel();      //放置选择年月的按钮和文本框
//		jb1=new JButton("上年");
//		leftnorth.add(jb1);
//		jb1.addActionListener(new ShowCalender());//日历显示时为按钮添加侦听器
//		
//		leftnorth.add(jtf1);
//		jb2=new JButton("下年");
//		jb2.addActionListener(new ShowCalender());
//		leftnorth.add(jb2);
//		jb3=new JButton("上月");
//		jb3.addActionListener(new ShowCalender());
//		leftnorth.add(jb3);
//		
//		leftnorth.add(jtf2);
//		jb4=new JButton("下月");
//		leftnorth.add(jb4);
//		jb4.addActionListener(new ShowCalender());
//		leftnorth.add(jtf3);
//		
//		leftpanel.add(leftnorth, BorderLayout.NORTH);
//		
//		JPanel leftcenter=new JPanel();
//		leftcenter.setLayout(new GridLayout(7,7));//进行布局
//		String[] xinqi={"星期日","星期一","星期二","星期三","星期四","星期五","星期六"};
//		JLabel[] jl=new JLabel[7];
//		for(int i=0;i<7;i++)
//		{
//			jl[i]=new JLabel(xinqi[i]);
//			leftcenter.add(jl[i]);
//			
//		}
//		
//		for(int i=0;i<42;i++)
//		{
//			leftcenter.add(jtf[i]=new JTextField());
//			jtf[i].addMouseListener(new noteProcess());
//			jtf[i].setEditable(false);
//		}
//		leftpanel.add(leftcenter, BorderLayout.CENTER);
//		
//		JPanel rightpanel=new JPanel();                //计事本容器
//		rightpanel.setLayout(new BorderLayout());
//		
//		rightpanel.add(jla,BorderLayout.NORTH);
//		
//		jta.setBorder(BorderFactory.createTitledBorder("日记"));
//		rightpanel.add(jta,BorderLayout.CENTER);
//		JPanel rightsouth=new JPanel();
//		JButton jb5=new JButton("保存日志");
//		rightsouth.add(jb5);
//		jb5.addActionListener(new noteProcess());//为按钮添加侦听
//		JButton jb6=new JButton("删除日志");
//		rightsouth.add(jb6);
//		jb6.addActionListener(new noteProcess());
//		rightpanel.add(rightsouth, BorderLayout.SOUTH);
//			
//		cp.add(leftpanel);
//		cp.add(rightpanel);
//		mainframe.setVisible(true);
//		mainframe.pack();
//		mainframe.addWindowListener(new WinLis());
//	}
	
	public static void main(String[] args)
	{
//		CalendarPad calpad=new CalendarPad();
//		GregorianCalendar gCalendar = (GregorianCalendar) Calendar.getInstance();
//		ShowCalender.showCalendar(gCalendar);
		
		// 设置外观
		try {
			//UIManager.setLookAndFeel(new com.sun.java.swing.plaf.windows.WindowsLookAndFeel());
			String lookAndFeel = "com.sun.java.swing.plaf.windows.WindowsClassicLookAndFeel";
			UIManager.setLookAndFeel(lookAndFeel);
		} catch (Exception e) {
			JOptionPane.showMessageDialog(null, "加载 plaf 模式显示组件出错！", "错误",
					JOptionPane.WARNING_MESSAGE);
		}
		
		new MainFrame();

	}
}
class WinLis extends WindowAdapter//监听窗口的动态
{
	public void windowClosing(WindowEvent we)
	{
		System.exit(0);
	}
}
