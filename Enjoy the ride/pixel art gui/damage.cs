using Godot;
using System;

public class damage : Control
{
	public override void _Ready()
	{
		GetNode<Sprite>("Blood1").Hide();
		GetNode<Sprite>("Blood2").Hide();
		GetNode<Sprite>("Blood3").Hide();
	}
	public void blood (int blood)
	{
		if (blood == 0)
		{
			GetNode<Sprite>("Blood1").Hide();
			GetNode<Sprite>("Blood2").Hide();
			GetNode<Sprite>("Blood3").Hide();
		}
		else if (blood == 1)
		{
			GetNode<Sprite>("Blood1").Show();
			GetNode<Sprite>("Blood2").Hide();
			GetNode<Sprite>("Blood3").Hide();
		}
		else if (blood == 2)
		{
			GetNode<Sprite>("Blood1").Hide();
			GetNode<Sprite>("Blood2").Show();
			GetNode<Sprite>("Blood3").Hide();
		}
		else if (blood == 3)
		{
			GetNode<Sprite>("Blood1").Hide();
			GetNode<Sprite>("Blood2").Hide();
			GetNode<Sprite>("Blood3").Show();
		}
	}
}
