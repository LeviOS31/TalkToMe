using Godot;
using System;

public class player : KinematicBody2D
{
	[Export] public int speed = 100;

	public Vector2 velocity = new Vector2();

	public void GetInput(string move)
	{
		velocity = new Vector2();

		if (move == "right")
		{
			velocity.x += 1;
		}
		if (move == "left")
		{
			velocity.x -= 1;
		}
		if (move == "backwards")
		{
			GetNode<Sprite>("Sprite").Frame = 0;
			velocity.y += 1;
		}
		if (move == "forward")
		{
			GetNode<Sprite>("Sprite").Frame = 1;
			velocity.y -= 1;
		}

		if (move == "stop")
		{
			velocity.x = 0;
			velocity.y = 0;
		}
		
		velocity = velocity.Normalized() * speed;

		Timer movetimer = GetNode<Timer>("movetimer");
		movetimer.Start();
	}
	
	public void move() 
	{
		velocity = new Vector2();

		if (Input.IsActionPressed("right"))
		{
			velocity.x += 1;
		}
		if (Input.IsActionPressed("left"))
		{
			velocity.x -= 1;
		}

		if (Input.IsActionPressed("down"))
		{
			GetNode<Sprite>("Sprite").Frame = 0;
			velocity.y += 1;
		}

		if (Input.IsActionPressed("up"))
		{
			GetNode<Sprite>("Sprite").Frame = 1;
			velocity.y -= 1;
		}

		velocity = velocity.Normalized() * speed;
	}

	public override void _PhysicsProcess(float delta)
	{
		move();
		velocity = MoveAndSlide(velocity);
		Timer movetimer = GetNode<Timer>("movetimer");
		if(movetimer.TimeLeft < 0.01 && movetimer.TimeLeft > 0.000001)
		{
			velocity.x = 0;
			velocity.y = 0;
		}
	}
}
