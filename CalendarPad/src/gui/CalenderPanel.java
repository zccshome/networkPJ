package gui;

import java.awt.Color;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

import noteProcess.noteProcess;
import calenderProcess.MyCalender;

/**
 * 显示日历的面板类
 * 
 * @author weijinshi
 * 
 */
public class CalenderPanel extends JPanel implements ActionListener
{
	private static final long serialVersionUID = 1L;

	public static MyCalender cal = new MyCalender();

	private JPanel p0 = new JPanel();
	private JPanel p1 = new JPanel();
	private JPanel p2 = new JPanel();

	private TransparentButton prevYearButton = new TransparentButton("上一年");
	private JLabel yearLabel = new JLabel("", SwingConstants.CENTER);
	private TransparentButton nextYearButton = new TransparentButton("下一年");
	private TransparentButton prevMonthButton = new TransparentButton("上一月");
	private JLabel monthLabel = new JLabel("", SwingConstants.CENTER);
	private TransparentButton nextMonthButton = new TransparentButton("下一月");

	private TransparentButton[] dayButtons = new TransparentButton[42];

	public CalenderPanel()
	{
		prevYearButton.setBounds(20, 0, 50, 30);
		yearLabel.setBounds(80, 0, 40, 30);
		nextYearButton.setBounds(130, 0, 50, 30);
		prevMonthButton.setBounds(240, 0, 50, 30);
		monthLabel.setBounds(300, 0, 40, 30);
		nextMonthButton.setBounds(350, 0, 50, 30);

		prevYearButton.addActionListener(this);
		nextYearButton.addActionListener(this);
		prevMonthButton.addActionListener(this);
		nextMonthButton.addActionListener(this);

		p0.setOpaque(false);
		p0.setBounds(40, 60, 500, 50);
		p0.setLayout(null);
		p0.add(prevYearButton);
		p0.add(yearLabel);
		p0.add(nextYearButton);
		p0.add(prevMonthButton);
		p0.add(monthLabel);
		p0.add(nextMonthButton);

		p1.setOpaque(false);
		p1.setBounds(0, 80, 500, 80);
		p1.setLayout(new GridLayout(1, 7));
		p1.add(new JLabel("周日", SwingConstants.CENTER));
		p1.add(new JLabel("周一", SwingConstants.CENTER));
		p1.add(new JLabel("周二", SwingConstants.CENTER));
		p1.add(new JLabel("周三", SwingConstants.CENTER));
		p1.add(new JLabel("周四", SwingConstants.CENTER));
		p1.add(new JLabel("周五", SwingConstants.CENTER));
		p1.add(new JLabel("周六", SwingConstants.CENTER));

		cal.refreshDaysArray();

		p2.setOpaque(false);
		p2.setBounds(0, 130, 500, 420);
		p2.setLayout(new GridLayout(6, 7));
		refresh();

		this.setOpaque(false);
		this.setLayout(null);
		this.add(p0);
		this.add(p1);
		this.add(p2);
	}

	/**
	 * 点击翻页后 刷新日历面板的显示
	 */
	public void refresh()
	{
		p2.removeAll();
//		p2.invalidate() ;

		for (int i = 0; i < 42; i++)
		{
			dayButtons[i] = new TransparentButton(i);

			// 点击日期显示备忘
			dayButtons[i].addActionListener(new noteProcess());

			String dayTitle = cal.daysArray[i].getDayTitle();

			dayButtons[i].setText(cal.getDaysArray()[i].getDayText());

			if (MainFrame.isLogin)
			{
				// 若有备忘则高亮
				if (cal.daysArray[i].getHasReminder())
				{
					dayButtons[i].setOpaque(true);
					dayButtons[i].setBackground(Color.yellow);
				}
			}

			// 若是今日时间则高亮
			if (dayTitle.length() == 8
					&& dayTitle.substring(0, 4).equals(MyCalender.curyear + "")
					&& dayTitle.substring(4, 6).equals(
							(MyCalender.curmonth < 10) ? "0" + MyCalender.curmonth : MyCalender.curmonth + "")
					&& dayTitle.substring(6, 8).equals(
							(MyCalender.curday < 10) ? "0" + MyCalender.curday : MyCalender.curday + ""))
			{
				dayButtons[i].setOpaque(true);
				dayButtons[i].setBackground(Color.green);
			}

			p2.add(dayButtons[i]);

		}

		yearLabel.setText(MyCalender.year + "");
		monthLabel.setText(MyCalender.month + "");

		p2.validate();

	}

	/**
	 * 事件监听方法， 主要处理日历的翻页事件
	 */
	public void actionPerformed(ActionEvent e)
	{
		if (e.getSource() == prevYearButton)
		{
			MyCalender.year--;
		}
		else if (e.getSource() == nextYearButton)
		{
			MyCalender.year++;
		}
		else if (e.getSource() == prevMonthButton)
		{
			MyCalender.month--;
			if (MyCalender.month < 1)
			{
				MyCalender.month = 12;
				MyCalender.year--;
			}
		}
		else if (e.getSource() == nextMonthButton)
		{
			MyCalender.month++;
			if (MyCalender.month > 12)
			{
				MyCalender.month = 1;
				MyCalender.year++;
			}
		}

		cal.refreshDaysArray();
		refresh();
	}

}
