using Godot;
using System;

public class number : Position2D
{
	private Vector2 velocity = new Vector2(0, 0);
	public override void _Ready()
	{

		Random random = new Random();
		int randomnumber = random.Next(0, 80) - 40;

		velocity  = new Vector2(randomnumber, 25);

		Tween tween = GetNode<Tween>("Tween");
		tween.InterpolateProperty(this, "scale", Scale, new Vector2(1,1), 0.4f, Tween.TransitionType.Linear, Tween.EaseType.Out);
		tween.InterpolateProperty(this, "scale", new Vector2(1,1), new Vector2(0.1f, 0.1f), 1, Tween.TransitionType.Linear, Tween.EaseType.Out, 0.4f);
		tween.Start();
		
	}
		
	private void _on_Tween_tween_all_completed()
	{
		QueueFree();
	}

	public override void _Process(float delta)
	{
		Position -= velocity * delta;
	}

}


