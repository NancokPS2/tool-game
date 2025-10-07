using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame;

[GlobalClass]
public partial class Item3D : Node3D, IItem
{
	[Export]
	public string ItemName { set; get; } = "";

	[Export]
	public PackedScene? ItemScene { get; set; }

	[Export]
	public Texture2D? Icon { get; set; }

	protected Node? PreviousParent;

	public IItem Pickup()
	{
		PreviousParent = GetParent();

		GetParent().RemoveChild(this);

		if (IsInsideTree())
			throw new Exception("Failed to get removed");

		return this;
	}

	public void Place(Vector3 whereGlobal)
	{
		if (PreviousParent is null)
			throw new Exception("Does not have a previous parent, it cannot be placed.");

		PreviousParent.AddChild(this);

		SetDeferred("global_position", whereGlobal);
	}
}
