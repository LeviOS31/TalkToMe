using Godot;
using System;

public class playermenu : KinematicBody2D
{
	private bool up = false;

	public void Move(float delta)
	{
		var anim = GetNode("playeranim") as AnimationPlayer;
		float moveAmount = -50 * delta;
		Position += Transform.x.Normalized() * moveAmount;
		if (up)
		{
			float moveupAmount = -50 * delta;
			Position += Transform.y.Normalized() * moveupAmount;
		}
		anim.Play("run");
	}

	private void _on_Timer2_timeout()
	{
		up = true;
	}
}


