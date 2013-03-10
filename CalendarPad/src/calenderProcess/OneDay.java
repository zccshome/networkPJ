package calenderProcess;

import noteProcess.Reminder;

/**
 * 这是一个对应于每一个日期的类，是为了于日历显示面板上的每一个按钮相对应
 * 
 * @author weijinshi
 * 
 */
public class OneDay
{
	private String dayText; // 日历面板中 button 里面显示的文字
	private String dayTitle; // 当天的日期，比如20130101，点击该按钮时，点击按钮时笔记本编辑区的标题处显示当天的日期
								// 2013年01月01日的笔记
	private boolean hasReminder; // 该日期是否已经有日记
	private Reminder reminder; // 该日期对应的日记

	public OneDay()
	{
		setDayText("");
		setDayTitle("");
		setHasReminder(false);
		setReminder(null);
	}

	/**
	 * 获取 dayText
	 * 
	 * @return
	 */
	public String getDayText()
	{
		return dayText;
	}

	/**
	 * 获取 dayTitle
	 * 
	 * @return
	 */
	public String getDayTitle()
	{
		return dayTitle;
	}

	/**
	 * 获取 hasReminder
	 * 
	 * @return
	 */
	public boolean getHasReminder()
	{
		return hasReminder;
	}

	/**
	 * 获取 reminder
	 * 
	 * @return
	 */
	public Reminder getReminder()
	{
		return reminder;
	}

	/**
	 * 设置 dayText
	 * 
	 * @return
	 */
	public void setDayText(String dayText)
	{
		this.dayText = dayText;
	}

	/**
	 * 设置 dayTitle
	 * 
	 * @return
	 */
	public void setDayTitle(String dayTitle)
	{
		this.dayTitle = dayTitle;
	}

	/**
	 * 设置 hasReminder
	 * 
	 * @return
	 */
	public void setHasReminder(boolean hasReminder)
	{
		this.hasReminder = hasReminder;
	}

	/**
	 * 设置 reminder
	 * 
	 * @return
	 */
	public void setReminder(Reminder reminder)
	{
		this.reminder = reminder;
	}
}
