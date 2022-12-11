using Godot;
using System;

public class battle : Node2D
{
	public override void _Ready()
	{
		
	}

	public void enemy(Node enemy)
	{
		if(enemy.Name.Contains("clown"))
		{
			GD.Print("clown");
		}
		if(enemy.Name.Contains("lion"))
		{
			GD.Print("lion");
		}
		if(enemy.Name.Contains("pirate"))
		{
			GD.Print("pirate");
		}
		if(enemy.Name.Contains("octopus"))
		{
			GD.Print("octopus");
		}
		if(enemy.Name.Contains("ghost"))
		{
			GD.Print("ghost");
		}
		if(enemy.Name.Contains("dragon"))
		{
			GD.Print("dragon");
		}
	}
}
