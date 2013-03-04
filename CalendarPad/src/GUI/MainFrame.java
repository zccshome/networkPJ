package GUI;

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

public class MainFrame extends JFrame implements ActionListener
{
	private static final long serialVersionUID = 1L;

	private BackgroundPanel bgPanel = new BackgroundPanel();
	private JMenuBar jmb = new JMenuBar();
	private MyJMenu jm_1 = new MyJMenu("Æ¤·ô");
	private MyJMenu jm_2 = new MyJMenu("²å¼þ");
	private MyJMenu jm_3 = new MyJMenu("µÇÂ½");
	
	public static CalenderPanel calenderPanel = new CalenderPanel();
	
	private NotePanel notePanel = new NotePanel();
	private JLabel usernameLabel = new JLabel("XXX ÒÑµÇÂ¼");

	private JMenuItem jm_1_1 = new JMenuItem("ÎÞ");
	private JMenuItem jm_1_2 = new JMenuItem("³È");
	private JMenuItem jm_1_3 = new JMenuItem("ÂÌ");
	private JMenuItem jm_1_4 = new JMenuItem("·Û");
	private JMenuItem jm_1_5 = new JMenuItem("À¶");
	private JMenuItem jm_3_1 = new JMenuItem("µÇÂ½");

	private String bgImageFileName;

	public MainFrame()
	{
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
		jm_1.setOpaque(false) ;
		jm_2.setOpaque(false) ;
		jm_3.setOpaque(false) ;
		
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
		this.setTitle("Ë½ÃÜÈÕ¼Ç");
		this.setSize(900, 600);
		this.setResizable(false);
		this.setLocationRelativeTo(null);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.setLayout(new BorderLayout());

		this.setContentPane(bgPanel);

		this.setVisible(true);
	}

	private void readConfig() throws FileNotFoundException
	{
		Scanner scanner = new Scanner(new File("config/default.cfg"));
		scanner.next();
		scanner.next();
		bgImageFileName = scanner.next();
	}
	
	private void writeConfig() throws IOException
	{
		String content = "bgImageFileName = " + bgImageFileName + "\n";
		
		OutputStreamWriter osw = new OutputStreamWriter(new FileOutputStream("config/default.cfg"));
		osw.write(content);
		osw.flush();
		osw.close();
	}

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
			if (jm_3.getText().equals("µÇÂ½"))
			{
				jm_3.setText("ÍË³ö");
				jm_3_1.setText("ÍË³ö");
			}
			else 
			{
				jm_3.setText("µÇÂ½");
				jm_3_1.setText("µÇÂ½");
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
