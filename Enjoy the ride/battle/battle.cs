using Godot;
using System;

public class battle : Node2D
{

	public enum turn
	{
		player,
		enemy,
		execute,
	}

	private turn _currentturn;
	private string enemychose;	
	private string playerchose;
	private Attack playerattack = new Attack(15, 5, 5, 15, 3);
	private Defend playerdefend = new Defend(100, 80);

	private turn Currentturn
	{
		set
		{
			_currentturn = value;
			if(_currentturn == turn.player)
			{
				GD.Print("player turn");
			}
			if(_currentturn == turn.enemy)
			{
				GD.Print("enemy turn");
			}
			if(_currentturn == turn.execute)
			{
				GD.Print("execute");
			}
		}
	}


	private int enemyhealth;
	private int health;

	public turn Currentturn1 { get => _currentturn;}

	// private clownbattle clown;
	// private lionbattle lion;
	// private piratebattle pirate;
	// private octopusbattle octopus;
	// private ghostbattle ghost;
	private dragonbattle dragon;


	public override void _Ready()
	{
		_currentturn = turn.player;
		health = GetNode<Global>("/root/GM").health;
	}

	public override void _Process(float delta)
	{
		if (_currentturn == turn.enemy)
		{
			enemyattack();
			_currentturn = turn.execute;
		}
		else if (_currentturn == turn.player)
		{

		}
		else if (_currentturn == turn.execute)
		{
			int enemydamage;
			int playerdamage;
			int defence;
			if (dragon != null)
			{
				if (enemychose == "attack")
				{
					enemydamage = dragon.attacking();
					if (playerchose == "defend")
					{
						defence = playerdefend.calc();
						int num = 100 - defence;
						enemydamage = enemydamage / 100 * num;
					}
					health -= enemydamage;
					GetNode<Global>("/root/GM").health = health;
					GetNode<ProgressBar>("health/ProgressBar").Value = health;
				}
				else if (playerchose == "attack")
				{
					playerdamage = playerattack.calc();
					if (enemychose == "defend")
					{
						defence = dragon.defending();
						int num = 100 - defence;
						playerdamage = playerdamage / 100 * num;
					}
					dragon.health -= playerdamage;
					GetNode<ProgressBar>("healthenemy/ProgressBar").Value = dragon.health;
				}
			}
		}
	}

	private void enemyattack()
	{
		if(dragon != null)
		{
			enemychose = dragon.choose();
		}
	}
	public void spawnenemy(Node enemy)
	{
		if(enemy.Name.Contains("clown"))
		{

		}
		else if(enemy.Name.Contains("lion"))
		{

		}
		else if(enemy.Name.Contains("pirate"))
		{

		}
		else if(enemy.Name.Contains("octopus"))
		{

		}
		else if(enemy.Name.Contains("ghost"))
		{

		}
		else if(enemy.Name.Contains("dragon"))
		{
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/dragon.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			dragon = temp as dragonbattle;
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
		// GD.Print(enemy);
		// GD.Print(enemy.Call("choose"));
	}
}
