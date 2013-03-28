package gui;

import java.awt.Insets;

import javax.swing.JButton;

/**
 * 一个透明按钮的类
 * 
 * @author weijinshi
 * 
 */
public class TransparentButton extends JButton
{
	private static final long serialVersionUID = 1L;
	public int index; // 按钮在数组中的下标

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
