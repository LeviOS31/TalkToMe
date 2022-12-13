using Godot;
using System;

//Levi

public class dragonbattle : Node2D
{
	[Signal]
	delegate void damaged(int health);
	public int health = 100;
	Attack attack = new Attack(20, 10, 5, 10, 5);
	Defend defend = new Defend(90, 87);

	private void _on_damaged()
	{
		EmitSignal("damaged", health);
	}

	public string choose()
	{
		if (health > 0)
		{
			Random random = new Random();
			int num;
			num = random.Next(1, 100);
			if (health == 100)
			{
				if (num <= 85)
				{
					return "attack";
				}
				else
				{
					//defend
					return "defend";
				}
			}
			else if (health > 75)
			{
				if (num <= 75)
				{
					return "attack";
				}
				else
				{
					//defend
					return "defend"; 
				}
			}
			else if (health > 50)
			{
				if (num <= 50)
				{
					return "attack";
				}
				else
				{
					//defend
					return "defend";
				}
			}
			else if (health < 50)
			{
				if (num <= 30)
				{
					return "attack";
				}
				else
				{
					//defend
					return "defend";
				}
			}
		}
		else
		{
			//die
			return "die";
		}
		return "error";
	}

	public int attacking()
	{
		return attack.calc();
	}
	public int defending()
	{
		return defend.calc();
	}
}
