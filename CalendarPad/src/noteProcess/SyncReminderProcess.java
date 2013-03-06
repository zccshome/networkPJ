package noteProcess;

public class SyncReminderProcess extends Thread {

	public void run(){
		
		noteProcess temp = new noteProcess();
		temp.backup(temp.reach());
		System.out.println("backuped!");
		temp.download();
		System.out.println("downloaded!");
		
	}
}
