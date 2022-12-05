
using System;
using Godot;


public class movemenu : KinematicBody2D
{
	
	public void Move(float delta, float speed)
	{  
		float moveAmount = speed * delta;
		Position += Transform.x.Normalized() * moveAmount;
	}
	
	
	
}
