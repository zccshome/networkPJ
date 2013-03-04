package noteProcess;

public class Encryption
{
	public static String encryption(String content)
	{
		StringBuffer b = new StringBuffer();
		for(int i = 0; i < content.length(); i++)
		{
			char tempB = (char)((int)content.charAt(i)+3);
			b.append(tempB);
		}
		return b.toString();
		//return content;
	}
	public static String decryption(String content)
	{
		StringBuffer b = new StringBuffer();
		for(int i = 0; i < content.length(); i++)
		{
			char tempB = (char)((int)content.charAt(i)-3);
			b.append(tempB);
		}
		return b.toString();
		//return content;
	}
}
