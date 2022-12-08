using Godot;
using System;
using System.Linq;

public class all : Control
{
	
	enum load 
	{
		Battle,
		World,
		Map,
		Menu,
		
	}
	private PackedScene world;

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

			if (previousload == load.World)
			{
				worldinstance = GetNode("world");
				playerlocation = worldinstance.GetNode<player>("YSort/player").Position;
			}
			switch (_currentload)
			{
				case load.World:
					worldinstance = world.Instance();
					AddChild(worldinstance);
					if (playerlocation != new Vector2(0,0))
					{
						worldinstance.GetNode<player>("YSort/player").Position = playerlocation;
					}
					break;
				case load.Menu:
					break;
				case load.Map:
					break;
				case load.Battle:
					break;
			}
		}
	}

	Godot.Object speechtotextobj;

	public override void _Ready()
	{
		world = GD.Load<PackedScene>("res://world.tscn");
		GDScript speechtotext = (GDScript) GetNode("SpeechToText").Get("script");
		speechtotextobj = (Godot.Object) speechtotext.New();
		speechtotextobj.Call("init");
		while(!(bool)speechtotextobj.Call("can_speak"))
		{

		}
		GD.Print("godot-speech-to-text plugin loaded");
		Currentload = load.World;
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

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("Talk"))
		{
			if ((bool)speechtotextobj.Call("can_speak"))
			{
				GD.Print("start speaking");
				speechtotextobj.Call("start");
			}
		}
		if (Input.IsActionJustReleased("Talk"))
		{
			if ((bool)speechtotextobj.Call("am_speaking"))
			{
				GD.Print("stop speaking");
				var result = speechtotextobj.Call("stop_and_get_result");
				if (result is GDScriptFunctionState)
				{
					result = (result, "completed");
				}
				GD.Print("Recognized: " + result);
				string[] strings = {"forward","backwards","left","right","stop"};
				if (strings.Any(result.ToString().Contains) && _currentload == load.World)
				{
					GD.Print("Move");
					GetNode<world>("world").move(result.ToString());
				}
			}
		}
	}

}
