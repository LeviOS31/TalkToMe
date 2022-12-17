using Godot;
using System;

public class menu : Control
{
	bool yellow = true;
	bool pressed = false;
	bool movetoggle = false;

	public override void _Ready()
	{
		GetNode<Timer>("texttimer").Start();
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("Talk"))
		{
			_on_startButton_pressed();
		}
	}
	

	public override void _PhysicsProcess(float delta)
	{
		playermenu player = GetNode("player") as playermenu; 
		checkpressed();
		Timer transitiontimer = GetNode<Timer>("transitionscene/Timer");

		if (movetoggle == true)
		{
			player.Move(delta);
		}
		if (transitiontimer.TimeLeft < 0.1 && transitiontimer.TimeLeft > 0.000001)
		{
			GetTree().ChangeScene("res://alltest.tscn");
		}
	}

	public void checkpressed()
	{
		if (pressed) 
		{
			movetoggle = true;
		}
	}

	public void _on_startButton_pressed()
	{
		pressed = true;
		GetNode<Timer>("player/Timer").Start();
		GetNode<Timer>("player/Timer2").Start();
		GetNode<Label>("Label").QueueFree();
		GetNode<Timer>("texttimer").Stop();
	}

	private void _on_Timer_timeout()
	{
		transitionscene transition = GetNode("transitionscene") as transitionscene;
		transition.transition_to_black();
	}

	private void _on_texttimer_timeout()
	{
		Label text = GetNode<Label>("Label");
		if (yellow)
		{
			text.AddColorOverride("font_color", new Color("#fc9e2b"));
			yellow = false;
		}
		else
		{
			text.AddColorOverride("font_color", new Color("#fce42b"));
			yellow = true;
		}
		
		GetNode<Timer>("texttimer").Start();
	}
}






