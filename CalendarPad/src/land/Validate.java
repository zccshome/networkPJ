package land;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

public class Validate {
	public Validate(){
		
	}
	
	public static boolean isValidate(String username, String userpass){
		
		//network
		if(hasNetwork()){
			//methods to check pass via internet, to be continued
			return false;
		}
		else{
			//check pass via local file, to be encryped with md5
			File file = new File("settings/userpass");
			BufferedReader reader = null;
			String pass = null;
			String [] namepass;
			try{
				reader = new BufferedReader( new FileReader(file) );
				String tempStr = null;
				while((tempStr = reader.readLine()) != null ){
					namepass = tempStr.split(" ");
					if(namepass[0].equals(username)){
						pass = namepass[1];
						break;
					}
				}
				reader.close();
			}catch(IOException e){
				e.printStackTrace();
			}finally{
				if (reader != null) {
	                try {
	                    reader.close();
	                } catch (IOException e1) {
	                }
	            }
			}
			if(pass != null && pass.equals(userpass))
				return true;
			return false;
		}
	}
	private static  boolean hasNetwork(){
		return false;
	}
	
	
}
