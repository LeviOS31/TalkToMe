using Godot;
using System;

[Signal]
delegate void transitioned();
public class transitionscene : CanvasLayer
{
	public void transition_to_black()
	{
		var Animation = GetNode<AnimationPlayer>("AnimationPlayer");
		Timer timer  = GetNode<Timer>("Timer");
		timer.Start();
		Animation.Play("fade_to_black");
	}

	public void transition_to_normal()
	{
		var Animation = GetNode<AnimationPlayer>("AnimationPlayer");
		Animation.Play("fade_to_normal");
	}
	private void _on_AnimationPlayer_animation_finished(String anim_name)
	{
		var Animation = GetNode<AnimationPlayer>("AnimationPlayer");

		if (anim_name == "fade_to_black")
		{
			EmitSignal("transitioned");
			Animation.Play("fade_to_normal");
		}
	}

}




