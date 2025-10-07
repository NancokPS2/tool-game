using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Scenes.Items;

[GlobalClass]
public partial class ItemPickupArea3D : Area3D
{
	[Export]
	protected Node? item
	{
		set => Item = value as IItem ?? throw new Exception();
		get => Item as Node;
	}

	public IItem Item = null!;

	public IItem GetItem()
		=> Item;
}
