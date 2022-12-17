using Godot;
using System;

public class player_battle : KinematicBody2D
{
	[Signal]
	public delegate void attack_anim();
	
	public override void _Ready()
	{
		
	}
	private void _on_AnimationPlayer_animation_finished(String anim_name)
	{
		if(anim_name == "attack")
		{
			EmitSignal("attack_anim");
		}
	}
}


