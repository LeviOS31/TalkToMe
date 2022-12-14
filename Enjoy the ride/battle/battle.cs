using Godot;
using System;

public class battle : Node2D
{

	public enum turn
	{
		player,
		enemy,
		execute,
		none,
	}

	[Signal]
	public delegate void chosen();

	private turn _currentturn;
	private string enemyname;
	private string enemychose = "";

	private Attack clownattack = new Attack(5, 2, 2, 5, 15);
	private Defend clowndefend = new Defend(75, 90);
	private Attack lionattack = new Attack(10, 5, 10, 7, 8);
	private Defend liondefend = new Defend(85, 80);
	private Attack pirateattack = new Attack(7, 5, 1, 80, 20);
	private Defend piratedefend = new Defend(60, 77);
	private Attack octopusattack = new Attack(14, 5, 8, 14, 9);
	private Defend octopusdefend = new Defend(100, 50);
	private Attack ghostattack = new Attack(4, 2, 0, 0, 0);
	private Defend ghostdefend = new Defend(100, 99);
	private Attack dragonattack = new Attack(20, 10, 5, 10, 5);
	private Defend dragondefend = new Defend(90, 87);

	private string playerchose = "";
	public string Playerchose { get => playerchose;}
	private Attack playerattack = new Attack(15, 5, 5, 15, 3);
	private Defend playerdefend = new Defend(100, 80);

	private turn Currentturn
	{
		set
		{
			turn previousturn = _currentturn;
			_currentturn = value;
			if (previousturn != _currentturn)
			{
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
	}


	private int enemyhealth = 100;
	private int playerhealth;

	public turn Currentturn1 { get => _currentturn;}

	public override void _Ready()
	{
		Currentturn = turn.player;
		playerhealth = GetNode<Global>("/root/GM").health;

		sethealth(playerhealth, GetNode<ProgressBar>("health/ProgressBar"));
		sethealth(enemyhealth, GetNode<ProgressBar>("enemyhealth/ProgressBar"));
	}

	public override async void _Process(float delta)
	{
		if (_currentturn == turn.enemy)
		{
			await ToSignal(GetNode<Timer>("Timer"), "timeout");
			Currentturn = turn.execute;
		}
		else if (_currentturn == turn.player)
		{
			await ToSignal(this, "chosen");
			Currentturn = turn.enemy; 
		}
		else if (_currentturn == turn.execute)
		{
			int enemydamage = 0;
			int playerdamage = 0;
			int defence = 0;
			if (enemychose == "attack")
			{
				if(enemyname == "clown")
				{
					enemydamage = clownattack.calc();
				}
				else if(enemyname == "lion")
				{
					enemydamage = lionattack.calc();
				}
				else if(enemyname == "pirate")
				{
					enemydamage = pirateattack.calc();
				}
				else if(enemyname == "octopus")
				{
					enemydamage = octopusattack.calc();
				}
				else if(enemyname == "ghost")
				{
					enemydamage = ghostattack.calc();
				}
				else if(enemyname == "dragon")
				{
					enemydamage = dragonattack.calc();
				}
				if (playerchose == "defend")
				{
					defence = playerdefend.calc();
					int num = 100 - defence;
					GD.Print("num: " + num);
					enemydamage = (int)Math.Ceiling((enemydamage) / 100.00 * (num));
				}
				playerhealth -= enemydamage;
				GetNode<Global>("/root/GM").health = playerhealth;
				GD.Print("player health: " + playerhealth);
				GD.Print("damage: " + enemydamage);
			}
			if (playerchose == "attack")
			{
				playerdamage = playerattack.calc();
				if (enemychose == "defend")
				{
					if(enemyname == "clown")
					{
						defence = clowndefend.calc();
					}
					else if(enemyname == "lion")
					{
						defence = liondefend.calc();
					}
					else if(enemyname == "pirate")
					{
						defence = piratedefend.calc();
					}
					else if(enemyname == "octopus")
					{
						defence = octopusdefend.calc();
					}
					else if(enemyname == "ghost")
					{
						defence = ghostdefend.calc();
					}
					else if(enemyname == "dragon")
					{
						defence = dragondefend.calc();
					}
					int num = 100 - defence;
					playerdamage = (int)Math.Ceiling((playerdamage) / 100.00 * (num));;
				}
				enemyhealth -= playerdamage;
				GD.Print("enemy health: " + enemyhealth);
				GD.Print("damage: " + playerdamage);
			}
			sethealth(playerhealth, GetNode<ProgressBar>("health/ProgressBar"));
			sethealth(enemyhealth, GetNode<ProgressBar>("enemyhealth/ProgressBar"));
			playerchose = "";
			enemychose = "";
			Currentturn = turn.player;
		}
	}
	private void sethealth(int health, ProgressBar bar)
	{
		bar.Value = health;
	}

	private void enemyattack()
	{
		if (enemyhealth > 0)
		{
			Random random = new Random();
			int num;
			num = random.Next(1, 100);
			if (enemyhealth == 100)
			{
				if (num <= 85)
				{
					enemychose = "attack";
				}
				else
				{
					enemychose = "defend";
				}
			}
			else if (enemyhealth > 75)
			{
				if (num <= 75)
				{
					enemychose = "attack";
				}
				else
				{
					enemychose = "defend"; 
				}
			}
			else if (enemyhealth > 50)
			{
				if (num <= 50)
				{
					enemychose = "attack";
				}
				else
				{
					enemychose = "defend";
				}
			}
			else if (enemyhealth < 50)
			{
				if (num <= 30)
				{
					enemychose = "attack";
				}
				else
				{
					enemychose = "defend";
				}
			}
			GD.Print("enemy chose " + enemychose);
			GetNode<Timer>("Timer").Start();
		}
	}
	public void spawnenemy(Node enemy)
	{
		if(enemy.Name.Contains("clown"))
		{
			enemyname = "clown";
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/clown.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
		else if(enemy.Name.Contains("lion"))
		{
			enemyname = "lion";
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/lion.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
		else if(enemy.Name.Contains("pirate"))
		{
			enemyname = "pirate";
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/pirate.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
		else if(enemy.Name.Contains("octopus"))
		{
			enemyname = "octopus";
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/octopus.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
		else if(enemy.Name.Contains("ghost"))
		{
			enemyname = "ghost";
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/dragon.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
		else if(enemy.Name.Contains("dragon"))
		{
			enemyname = "dragon";
			var enemypacked = GD.Load<PackedScene>("res://battle/enemies/dragon.tscn"); 
			Node temp = enemypacked.Instance();
			GetNode("enemies").AddChild(temp);
			Node2D enemy2d = (Node2D) temp;
			enemy2d.Position = new Vector2(200, 115);
		}
	}

	public void playerchosen(string result)
	{
		GD.Print("result: " + result);
		if(result == "punch")
		{
			playerchose = "attack";
			EmitSignal("chosen");
			GD.Print("player chose " + playerchose);
			enemyattack();

		}
		else if(result == "block")
		{
			playerchose = "defend";
			EmitSignal("chosen");
			GD.Print("player chose " + playerchose);
			enemyattack();
		}
		
	}
}
