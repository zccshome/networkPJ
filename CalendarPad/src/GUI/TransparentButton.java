package GUI;

import java.awt.Insets;

import javax.swing.JButton;

public class TransparentButton extends JButton
{
	private static final long serialVersionUID = 1L;
	public int index;
	
	public TransparentButton(int i)
	{
		index = i;
		this.setOpaque(false);
		this.setContentAreaFilled(false);
	}
	
	public TransparentButton(String text)
	{
		super(text);
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setMargin(new Insets(0, 0, 0, 0));
	}

}
