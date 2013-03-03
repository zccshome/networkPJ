package land;
import java.awt.Dimension;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;

public class LandingPanel extends JFrame{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private JLabel nameLabel = new JLabel("username:");
	private JLabel passLabel = new JLabel("password:");
	private JTextField nameField = new JTextField();
	private JPasswordField passField = new JPasswordField();
	private JButton loadButton = new JButton("登陆");
	private JButton cancleButton = new JButton("取消");
	private JLabel msgLabel = new JLabel();
	
	public LandingPanel(){
		
		//set the frame
		setVisible(true);
		setResizable(false);
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		setSize(screenSize.width/3, screenSize.height* 10/25);
		setLocation(screenSize.width/3, screenSize.height/4);
		
		//set the components
		passField.setEchoChar('*');
		
		nameLabel.setSize(100, 100);
		nameLabel.setLocation(80, 30);
		
		passLabel.setSize(100, 100);
		passLabel.setLocation(80, 90);
		
		nameField.setSize(140, 20);
		nameField.setLocation(170, 70);
		
		passField.setSize(140, 20);
		passField.setLocation(170, 130);
		
		loadButton.setSize(90, 25);
		loadButton.setLocation(80, 180);
		
		cancleButton.setSize(90,25);
		cancleButton.setLocation(220, 180);
		
		msgLabel.setSize(160,25);
		msgLabel.setLocation(80, 240);
		
		//background image of the frame
		ImageIcon bgim = new ImageIcon("settings/bg.jpg");
		JLabel bglbl = new JLabel(bgim);
		bglbl.setSize(this.getSize().width,this.getSize().height);
		
		//add the components
		this.add(nameLabel);
		this.add(nameField);
		this.add(passLabel);
		this.add(passField);
		this.add(loadButton);
		this.add(cancleButton);
		this.add(bglbl);
		
		loadButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				// TODO Auto-generated method stub
				String username = nameField.getText();
				String userpass = String.valueOf(passField.getPassword());
				if(username.equals("") || username == null || userpass.equals("") || userpass == null){
					msgLabel.setText("用户名密码不能为空");
					JOptionPane.showMessageDialog(null, "用户名密码不能为空");
				}
				else{
					if(Validate.isValidate(username, userpass) == true){
						JOptionPane.showMessageDialog(null, "登陆成功");
						//***********************转向Calendar页面
					}
					else{
						JOptionPane.showMessageDialog(null, "登录失败");
					}
				}
				
			}
		});
		cancleButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				// TODO Auto-generated method stub
				setVisible(false);
			}
		});
	}


	
}
