package Test;

import noteProcess.*;

public class testRSAEncrypt {
	public static void main(String[] args) {
		for (int i = 0; i < 100; i++)
			System.out.println(test());
	}
	
	private static boolean test() {
		String raw = generateString();
		RSAEncrypt.encryption(raw, "src\\diary\\miao.bak");
		return RSAEncrypt.decryption("src\\diary\\miao.bak").equals(raw);
	}
	
	private static String generateString() {
		StringBuffer sb = new StringBuffer();
		int length = (int) (Math.random() * 50);
		for (int i = 0; i < length; i++) {
			char tmp = (char)(96 * Math.random() + 32);
			sb.append(tmp);
		}
		
		return sb.toString();
	}
}
