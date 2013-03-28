package Test;

import noteProcess.*;

public class testEncryption {
	public static void main(String[] args) {
		for (int i = 0; i < 50; i++)
			System.out.println(test());
	}
	
	private static boolean test() {
		//先随机产生一个String
		String st = generateString();
		//检测加解密后时候与原String一样
		return st.equals(Encryption.decryption(Encryption.encryption(st)));
	}
	
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
