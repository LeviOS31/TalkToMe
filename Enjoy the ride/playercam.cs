using Godot;
using System;

public class playercam : Camera2D
{
	public override void _Ready()
	{
		Position2D topleft = GetNode<Position2D>("limit/topleft");
		Position2D bottomright = GetNode<Position2D>("limit/bottomright");

		LimitTop = Convert.ToInt32(topleft.Position.y);
		LimitBottom = Convert.ToInt32(bottomright.Position.y);
		LimitLeft = Convert.ToInt32(topleft.Position.x);
		LimitRight = Convert.ToInt32(bottomright.Position.x);
	}

}
