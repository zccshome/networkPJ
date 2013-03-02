package noteProcess;

public class Reminder
{
	private int year;
	private byte month;
	private byte day;
	private String content;
	
	public void setYear(int year)
	{
		this.year = year;
	}
	public int getYear()
	{
		return year;
	}
	public void setMonth(byte month)
	{
		this.month = month;
	}
	public byte getMonth()
	{
		return month;
	}
	public void setDay(byte day)
	{
		this.day = day;
	}
	public byte getDay()
	{
		return day;
	}
	public void setContent(String content)
	{
		this.content = content;
	}
	public String getContent()
	{
		return content;
	}
}
