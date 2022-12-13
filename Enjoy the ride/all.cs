using Godot;
using System;
using System.Linq;

public class all : Control
{
	
	private Global g;
	enum load 
	{
		Battle,
		World,
		Map,
		Menu,
		
	}
	private PackedScene world;
	private PackedScene battle;

	private Node enemy;

	private Node battlenode;

	private load _currentload;

	private Vector2 playerlocation = new Vector2(0,0);
	private load Currentload 
	{
		get {return _currentload;}
		set 
		{
			load previousload = _currentload;
			_currentload = value;
			Node worldinstance;

			battle battlescript;

			if (previousload == load.World)
			{
				worldinstance = GetNode("world");
				playerlocation = worldinstance.GetNode<player>("YSort/player").Position;
			}
			switch (_currentload)
			{
				case load.World:
					if (GetNodeOrNull("world") == null)
					{
						worldinstance = world.Instance();
						AddChild(worldinstance);
					}
					else 
					{
						worldinstance = GetNode("world");
						GetNode<Node2D>("world").Show();
					}
					worldinstance.GetNode<Camera2D>("playercam").Current = true;
					foreach (dragon N in worldinstance.GetNode<Node>("YSort/enemies").GetChildren())
					{
						N.Connect("touched", this, "start_battle");
					}
					if (playerlocation != new Vector2(0,0))
					{
						worldinstance.GetNode<player>("YSort/player").Position = playerlocation;
					}
					g.scene = "world";
					break;
				case load.Menu:
					g.scene = "menu";
					break;
				case load.Map:
					g.scene = "map";
					break;
				case load.Battle:
					GetNode<Node2D>("world").Hide();
					battlenode = battle.Instance();
					battlenode.GetNode<Camera2D>("Battlecam").Current = true;
					AddChild(battlenode);
					battlescript = battlenode as battle;
					battlescript.spawnenemy(enemy);
					g.scene = "battle";
					break;
			}
		}
	}

	Godot.Object speechtotextobj;

	public override void _Ready()
	{
		g = GetNode<Global>("/root/GM");
		battle = GD.Load<PackedScene>("res://battle/battle.tscn");
		world = GD.Load<PackedScene>("res://world.tscn");
		Currentload = load.World;
		GDScript speechtotext = (GDScript) GetNode("SpeechToText").Get("script");
		speechtotextobj = (Godot.Object) speechtotext.New();
		speechtotextobj.Call("init");
		while(!(bool)speechtotextobj.Call("can_speak"))
		{

		}
		GetNode<AnimatedSprite>("GUI/top/talk").Animation = "off";
		GD.Print("godot-speech-to-text plugin loaded");
	}

// block B L AO K
// dodge D AO JH
// punch P AH N CH
// kick K IH K
// swing S W IH NG
// heal F L IY
// start S T AA R T
// quit Q UW IH T
// menu M EH N Y UW
// forward F AO R W ER D
// backwards B AE K W ER D Z
// left L EH F T
// right R AY T
// help HH EH L P

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("Talk"))
		{
			if ((bool)speechtotextobj.Call("can_speak"))
			{
				GD.Print("start speaking");
				GetNode<AnimatedSprite>("GUI/top/talk").Animation = "on";
				speechtotextobj.Call("start");
			}
		}
		if (Input.IsActionJustReleased("Talk"))
		{
			if ((bool)speechtotextobj.Call("am_speaking"))
			{
				GD.Print("stop speaking");
				GetNode<AnimatedSprite>("GUI/top/talk").Animation = "off";
				var result = speechtotextobj.Call("stop_and_get_result");
				if (result is GDScriptFunctionState)
				{
					result = (result, "completed");
				}
				GD.Print("Recognized: " + result);
				string[] strings = {"forward","backwards","left","right","stop"};
				if (strings.Any(result.ToString().Contains) && _currentload == load.World)
				{
					GetNode<world>("world").move(result.ToString());
				}
			}
		}
	}

	public void start_battle( Node sender)
	{
		GD.Print("touched :" + sender.Name);
		enemy = sender;
		Currentload = load.Battle;
	}

}
