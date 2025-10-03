using System;
using System.Collections.Generic;
using ToolGame.Interaction;

namespace ToolGame.Machinery;

[GlobalClass]
public partial class MachineSlot : InteractionArea3D, IInteractionTarget
{
	public MachinePart? Part;
	[Export]
	protected Node? part
	{
		set => Part = value is MachinePart machPart ? machPart : null;
		get => Part as Node;
	}

	public List<EPartCategory> CategoriesAllowed = new();
	[Export]
	protected Godot.Collections.Array<EPartCategory> categoriesAllowed
	{
		set => CategoriesAllowed = new(value);
		get => new(CategoriesAllowed);
	}

}
