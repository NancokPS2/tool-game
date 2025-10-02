using Godot;
using System;

public partial class MachineTest : Node3D
{
	[Export]
	public MachinePart3D Part = null!;

	[Export]
	public MachineSlot3D Slot = null!;


}
