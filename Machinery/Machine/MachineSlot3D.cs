using System;
using System.Collections.Generic;
using ToolGame.Interaction;

namespace ToolGame.Machinery;

[GlobalClass]
public partial class MachineSlot3D : InteractionArea3D, IInteractionTarget
{
	[Export]
	public MachinePart3D? Part;

	public List<EPartCategory> CategoriesAllowed = new();
	[Export]
	protected Godot.Collections.Array<EPartCategory> categoriesAllowed
	{
		set => CategoriesAllowed = new(value);
		get => new(CategoriesAllowed);
	}

}
