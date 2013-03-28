package Test;

import utils.*;

public class testMd5 {
	public static void main(String[] args) {
		for (int i = 0; i < 100; i++) {
			String tmp = generateString();
			String ans = md5.encryptMD5(tmp);
			System.out.println(tmp + "\t" + ans);
		}
	}
	
	
	//
	private static String generateString() {
		StringBuffer sb = new StringBuffer();
		int length = (int) (Math.random() * 40);
		for (int i = 0; i < length; i++) {
			char tmp = (char)(96 * Math.random() + 32);
			sb.append(tmp);
		}
		
		return sb.toString();
	}
}
