package calenderProcess;

import noteProcess.Reminder;

public class OneDay
{
	private String dayText;
	private String dayTitle;
	private boolean hasReminder;
	private Reminder reminder;

	public OneDay()
	{
		setDayText("");
		setDayTitle("");
		setHasReminder(false);
		setReminder(null);
	}

	public String getDayText()
	{
		return dayText;
	}
	public String getDayTitle()
	{
		return dayTitle;
	}

	public boolean getHasReminder()
	{
		return hasReminder;
	}

	public Reminder getReminder()
	{
		return reminder;
	}

	public void setDayText(String dayText)
	{
		this.dayText = dayText;
	}
	public void setDayTitle(String dayTitle)
	{
		this.dayTitle = dayTitle;
	}

	public void setHasReminder(boolean hasReminder)
	{
		this.hasReminder = hasReminder;
	}

	public void setReminder(Reminder reminder)
	{
		this.reminder = reminder;
	}

}
