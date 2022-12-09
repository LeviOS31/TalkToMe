using Godot;
using System;

public class dragon : Area2D
{
	[Signal]
	delegate void touched(Node sender);

	public override void _Ready()
	{
		
	}

	private void _on_dragon_body_entered(object body)
	{
		GD.Print("dragon");
		EmitSignal("touched", GetNode<dragon>("."));
	}
}




