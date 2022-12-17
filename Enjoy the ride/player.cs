using Godot;
using System;

public class player : KinematicBody2D
{

	private Global g;
	[Export] public int speed = 100;

	public Vector2 velocity = new Vector2();

	public override void _Ready()
	{
		g = GetNode<Global>("/root/GM");
	}

	public void GetInput(string move)
	{
		GD.Print(g.scene);
		velocity = new Vector2();

		if (move == "right")
		{
			GetNode<Sprite>("Sprite").Frame = 3;
			velocity.x += 1;
		}
		if (move == "left")
		{
			GetNode<Sprite>("Sprite").Frame = 2;
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

		GD.Print("moved: " + move);
		Timer movetimer = GetNode<Timer>("movetimer");
		movetimer.Start();
	}
	
	public void move() 
	{
		velocity = new Vector2();

		if (Input.IsActionPressed("right"))
		{
			GetNode<Sprite>("Sprite").Frame = 3;
			velocity.x += 1;
		}
		if (Input.IsActionPressed("left"))
		{
			GetNode<Sprite>("Sprite").Frame = 2;
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
		if (g.scene == "world")
		{
			//move();
			velocity = MoveAndSlide(velocity);
			Timer movetimer = GetNode<Timer>("movetimer"); 
		}
	}
	private void _on_movetimer_timeout()
	{
		velocity.x = 0;
		velocity.y = 0;
	}
}


