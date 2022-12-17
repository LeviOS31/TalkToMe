using Godot;
using System;

public class dummy : Area2D
{
	[Signal]
	delegate void touched(Node sender);

	public override void _Ready()
	{
		
	}

	private void _on_Dummy_body_entered(object body)
	{
		GD.Print("Dummy");
		EmitSignal("touched", GetNode<dummy>("."));
	}
}


