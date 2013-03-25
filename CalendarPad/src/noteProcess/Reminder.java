package noteProcess;

public class Reminder implements java.io.Serializable
{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int year;
	private byte month;
	private byte day;
	private String content;
	
	public Reminder(int year, byte month, byte day, String content)
	{
		this.year = year;
		this.month = month;
		this.day = day;
		this.content = content;
	}
	
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
