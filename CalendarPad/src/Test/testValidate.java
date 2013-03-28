package Test;

import login.*;

public class testValidate {
	public static void main(String[] args) {
		String user = "1";
		String pw = "1";
		System.out.println(Validate.isValidate(user, pw));
		user = "2";
		pw = "2";
		System.out.println(Validate.isValidate(user, pw));

	}
}
