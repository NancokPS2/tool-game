using Godot;
using System;
using System.Linq;
using ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class SystemManager : Node
{
	[Export]
	protected Timer SystemTimerNode = null!;

	public override void _Ready()
	{
		base._Ready();
		foreach (var item in GetChildren().OfType<BaseSystem>())
		{
			SystemTimerNode.Timeout += () => item.SystemProcess((long)SystemTimerNode.WaitTime);
		}
	}

}
