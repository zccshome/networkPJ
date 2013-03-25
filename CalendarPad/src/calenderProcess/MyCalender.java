package calenderProcess;

import java.io.File;
import java.util.Calendar;
import java.util.GregorianCalendar;

import utils.ChinaDate;

/**
 * 获取日历显示的内容以及处理翻页显示日历的操作的类
 * 
 * @author weijinshi
 * 
 */
public class MyCalender
{
	// 今日时间
	public static int curyear, curmonth, curday;
	// 当前显示的时间
	private Calendar cal = Calendar.getInstance();
	public static int year, month, day;
	public static int selectedDay; // 标记当天日期以便高亮显示
	// 与日历显示面板的 42 个button 相对应的 一个 OneDay 数组，每一个都包含了当天的信息
	public OneDay[] daysArray = new OneDay[42];
	// 每个月第一天和最后一天在 42 个日历西那是按钮中的起始位置和结束位置的数组下表
	public int startDay, endDay;

	public MyCalender()
	{
		year = cal.get(Calendar.YEAR);
		month = (cal.get(Calendar.MONTH) + 1);
		day = cal.get(Calendar.DATE);
		// 设置当天日期
		GregorianCalendar gCalendar = (GregorianCalendar) Calendar.getInstance();
		curyear = gCalendar.get(Calendar.YEAR);
		curmonth = (gCalendar.get(Calendar.MONTH) + 1);
		curday = gCalendar.get(Calendar.DATE);
		selectedDay = curday;
		// 初始化 42 个 OneDay
		for (int i = 0; i < 42; i++)
		{
			daysArray[i] = new OneDay();
		}
	}

	/**
	 * 在每一次翻一月或一年时刷新 日历显示面板的 42 个按钮对应的 42 个 oneday 中的内容
	 */
	public void refreshDaysArray()
	{
		for (int i = 0; i < 42; i++)
		{
			daysArray[i] = new OneDay();
		}

		cal.set(year, month - 1, 1);

		int maxday = cal.getActualMaximum(Calendar.DATE); // 当月最大日期

		startDay = cal.get(Calendar.DAY_OF_WEEK) - 1;
		endDay = startDay + maxday;

		// 读取备忘列表
		File file = new File("src\\diary\\");
		String fileList[] = file.list();
		int size = 0;
		if (fileList != null)
		{
			size = fileList.length;
		}

		for (int i = startDay, j = 1; j <= maxday; i++, j++) // 显示新日历
		{
			String dayText = "";
			if (j < 10)
				dayText = "0" + j;
			else
				dayText = "" + j;

			String dayTitle = year + "";
			if (month < 10)
				dayTitle += "0" + month;
			else
				dayTitle += month;
			dayTitle += dayText;

			dayText = "<html><p style=\"font: 16px bold; color:red\">" + dayText + "</p><p style=\"font: 10px\">";
			// 农历显示
			dayText += ChinaDate.toChinaDay(year + "", month + "", j + "") + "</p></html>";

			// 标记是否有备忘
			for (int k = 0; k < size; k++)
			{
				if (dayTitle.equals(fileList[k].substring(0, 8)))
				{
					daysArray[i].setHasReminder(true);
					break;
				}
			}

			daysArray[i].setDayText(dayText);
			daysArray[i].setDayTitle(dayTitle);

		}

	}

	/**
	 * 获得 42 个 oneday
	 * 
	 * @return
	 */
	public OneDay[] getDaysArray()
	{
		return daysArray;
	}

}
