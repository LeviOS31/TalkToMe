using Godot;
using System;

public class playermenu : KinematicBody2D
{

	public void Move(float delta)
	{
		var anim = GetNode("playeranim") as AnimationPlayer;
		float moveAmout = -100 * delta;
		Position += Transform.x.Normalized() * moveAmout;
		anim.Play("run");
	}

}
