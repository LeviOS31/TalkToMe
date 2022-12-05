using Godot;
using System;

public class menu : Control
{
	bool pressed = false;
	bool movetoggle = false;

	public override void _Ready()
	{
		Button buttonstart = GetNode("transitionscene/VBoxContainer/startButton") as Button;
		buttonstart.GrabFocus();
	}


	public override void _PhysicsProcess(float delta)
	{
		movemenu clouds = GetNode("clouds") as movemenu;
		movemenu birbs = GetNode("birds") as movemenu;
		playermenu player = GetNode("player") as playermenu; 
		checkpressed();
		Timer transitiontimer = GetNode<Timer>("transitionscene/Timer");

		clouds.Move(delta, 5);
		birbs.Move(delta, 20);

		if (movetoggle == true)
		{
			player.Move(delta);
		}
		if (transitiontimer.TimeLeft < 0.1 && transitiontimer.TimeLeft > 0.000001)
		{
			GetTree().ChangeScene("res://addons/speechtotext/Example.tscn");
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
		Timer timer = GetNode("player/Timer") as Timer;
		timer.Start();
	}
	private void _on_exitButton_pressed()
	{
		GetTree().Quit();
	}
	private void _on_Timer_timeout()
	{
		transitionscene transition = GetNode("transitionscene") as transitionscene;
		transition.transition_to_black();
	}

}




