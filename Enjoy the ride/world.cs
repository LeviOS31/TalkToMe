using Godot;
using System;

public class world : Node2D
{

	public override void _Ready()
	{
		transitionscene transition = (transitionscene) GetNode("transitionscene");
		transition.transition_to_normal();
	}

	public void move(string direction)
	{
		GetNode<player>("YSort/player").GetInput(direction);
	}
}
