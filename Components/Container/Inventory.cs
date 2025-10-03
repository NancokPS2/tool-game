using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Container;

[GlobalClass]
public partial class Inventory : Node, IComponent
{
	[Export]
	protected Godot.Collections.Array<Node> contents
	{
		set
		{
			Contents = value.OfType<IItem>().ToList();
			Contents.ForEach(x => (x as Node)?.GetParent().RemoveChild(x as Node));
		}
		get => [.. Contents.OfType<Node>()];
	}
	public List<IItem> Contents = new();

	public int Selected
	{
		get
		{
			return Math.Clamp(selected, 0, Contents.Count());
		}
		set
		{
			selected = Math.Clamp(value, 0, Contents.Count());
		}
	}

	private int selected;

	public string[] GetProcessingGroups() => [CompGroups.INVENTORY];
}
