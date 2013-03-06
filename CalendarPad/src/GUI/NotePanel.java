package GUI;

import java.awt.Font;

import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;

import noteProcess.noteProcess;

public class NotePanel extends JPanel
{
	private static final long serialVersionUID = 1L;

	private TransparentButton saveButton = new TransparentButton("保存日记");
	private TransparentButton deleteButton = new TransparentButton("删除日记");
	private static JLabel dayTitleLabel = new JLabel("");
	public static JTextArea noteArea = new JTextArea(10, 10);
	private JScrollPane jsc = new JScrollPane(noteArea);

	public NotePanel()
	{
		saveButton.addActionListener(new noteProcess());//为按钮添加侦听
		deleteButton.addActionListener(new noteProcess());//为按钮添加侦听
		
		saveButton.setBounds(70, 60, 100, 30);
		deleteButton.setBounds(200, 60, 100, 30);
		dayTitleLabel.setBounds(30, 100, 200, 30);
		jsc.setBounds(20, 130, 330, 420);

		jsc.setOpaque(false);
		jsc.getViewport().setOpaque(false);
		noteArea.setOpaque(false);
		noteArea.setFont(new Font("Serif", 0, 18));

		this.setOpaque(false);
		this.setLayout(null);
		this.add(saveButton);
		this.add(deleteButton);
		this.add(dayTitleLabel);
		this.add(jsc);
	}

	public static void refresh(String dayTitle, String content)
	{
		if (MainFrame.isLogin)
		{
			dayTitleLabel.setText(dayTitle);
			noteArea.setText(content);
		}
		else {
			dayTitleLabel.setText("");
			noteArea.setText("");
		}
	}
	

}
