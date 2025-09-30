using Godot;
using System;

public partial class MachineTest : Node3D
{
	[Export]
	public MachinePart3D Part = null!;

	[Export]
	public MachineSlot3D Slot = null!;

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.IsActionPressed("ui_accept"))
		{
			
		}
	}


}
