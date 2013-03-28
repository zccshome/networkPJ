package gui;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.util.Scanner;

import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;

import login.LoginFrame;

/**
 * 程序主窗口类
 * 
 * @author weijinshi
 * 
 */
public class MainFrame extends JFrame implements ActionListener
{
	private static final long serialVersionUID = 1L;

	public static boolean isLogin = false; // 用户是否登录的标记

	private BackgroundPanel bgPanel = new BackgroundPanel();
	private JMenuBar jmb = new JMenuBar();
	private MyJMenu jm_1 = new MyJMenu("皮肤");
	private MyJMenu jm_2 = new MyJMenu("插件");
	public static JMenu jm_3 = new JMenu("登陆");

	public static JLabel usernameLabel = new JLabel("未登录");

	public static CalenderPanel calenderPanel = new CalenderPanel();

	private NotePanel notePanel = new NotePanel();

	private JMenuItem jm_1_1 = new JMenuItem("无");
	private JMenuItem jm_1_2 = new JMenuItem("橙");
	private JMenuItem jm_1_3 = new JMenuItem("绿");
	private JMenuItem jm_1_4 = new JMenuItem("粉");
	private JMenuItem jm_1_5 = new JMenuItem("蓝");
	public static JMenuItem jm_3_1 = new JMenuItem("登陆");

	private String bgImageFileName;

	public MainFrame()
	{
		// 先读取配置文件
		try
		{
			readConfig();
		} catch (FileNotFoundException e1)
		{
			e1.printStackTrace();
		}

		calenderPanel.setBounds(20, 0, 500, 550);
		notePanel.setBounds(530, 0, 350, 550);
		usernameLabel.setBounds(550, 10, 200, 30);

		jmb.setBounds(20, 10, 500, 20);
		jmb.setOpaque(false);
		jmb.setBorder(null);
		jmb.setLayout(null);
		jmb.add(jm_1);
		jmb.add(jm_2);
		jmb.add(jm_3);

		jm_1.setBounds(0, 0, 50, 20);
		jm_2.setBounds(50, 0, 50, 20);
		jm_3.setBounds(100, 0, 50, 20);
		jm_1.setOpaque(false);
		jm_2.setOpaque(false);
		jm_3.setOpaque(false);

		jm_1.add(jm_1_1);
		jm_1.add(jm_1_2);
		jm_1.add(jm_1_3);
		jm_1.add(jm_1_4);
		jm_1.add(jm_1_5);
		jm_3.add(jm_3_1);

		jm_1_1.addActionListener(this);
		jm_1_2.addActionListener(this);
		jm_1_3.addActionListener(this);
		jm_1_4.addActionListener(this);
		jm_1_5.addActionListener(this);
		jm_3_1.addActionListener(this);

		bgPanel.setLayout(null);
		bgPanel.add(calenderPanel);
		bgPanel.add(notePanel);
		bgPanel.add(jmb);
		bgPanel.add(usernameLabel);

		this.setIconImage(Toolkit.getDefaultToolkit().getImage("images/icon.png"));
		this.setTitle("私密日记");
		this.setSize(900, 600);
		this.setResizable(false);
		this.setLocationRelativeTo(null);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.setLayout(new BorderLayout());

		this.setContentPane(bgPanel);

		this.setVisible(true);
	}

	/**
	 * 读取配置文件
	 * 
	 * @throws FileNotFoundException
	 */
	private void readConfig() throws FileNotFoundException
	{
		Scanner scanner = new Scanner(new File("config/default.cfg"));
		scanner.next();
		scanner.next();
		bgImageFileName = scanner.next();
	}

	/**
	 * 写入配置文件
	 * 
	 * @throws IOException
	 */
	private void writeConfig() throws IOException
	{
		String content = "bgImageFileName = " + bgImageFileName + "\n";

		OutputStreamWriter osw = new OutputStreamWriter(new FileOutputStream("config/default.cfg"));
		osw.write(content);
		osw.flush();
		osw.close();
	}

	/**
	 * 事件处理，主要是菜单项的相应
	 */
	public void actionPerformed(ActionEvent e)
	{
		if ((JMenuItem) e.getSource() == jm_1_1 || (JMenuItem) e.getSource() == jm_1_2
				|| (JMenuItem) e.getSource() == jm_1_3 || (JMenuItem) e.getSource() == jm_1_4
				|| (JMenuItem) e.getSource() == jm_1_5)
		{
			if ((JMenuItem) e.getSource() == jm_1_1)
				bgImageFileName = "*";
			else if ((JMenuItem) e.getSource() == jm_1_2)
				bgImageFileName = "bg_1.png";
			else if ((JMenuItem) e.getSource() == jm_1_3)
				bgImageFileName = "bg_2.png";
			else if ((JMenuItem) e.getSource() == jm_1_4)
				bgImageFileName = "bg_3.png";
			else if ((JMenuItem) e.getSource() == jm_1_5)
				bgImageFileName = "bg_4.png";

			try
			{
				writeConfig();
			} catch (IOException e1)
			{
				e1.printStackTrace();
			}
			bgPanel.repaint();
		}

		if ((JMenuItem) e.getSource() == jm_3_1)
		{
			if (jm_3.getText().equals("登陆"))
			{
				new LoginFrame();
			}
			else
			{
				jm_3.setText("登陆");
				jm_3_1.setText("登陆");
				usernameLabel.setText("未登录");
				isLogin = false;
				calenderPanel.refresh();
				NotePanel.refresh("", "");
			}
		}
	}

	class BackgroundPanel extends JPanel
	{
		private static final long serialVersionUID = 1L;

		protected void paintComponent(Graphics g)
		{
			super.paintComponent(g);
			g.drawImage((new ImageIcon("images/" + bgImageFileName)).getImage(), 0, 0, null);
		}

	}

	class MyJMenu extends JMenu
	{
		private static final long serialVersionUID = 1L;

		public MyJMenu(String text)
		{
			super(text);
			this.setFont(new Font("Serif", 1, 12));
			this.addMouseListener(new MouseAdapter()
			{
				public void mouseEntered(MouseEvent e)
				{
					((JMenu) e.getSource()).setForeground(Color.red);

				}

				public void mouseExited(MouseEvent e)
				{
					((JMenu) e.getSource()).setForeground(Color.black);

				}
			});
		}

	}

}
