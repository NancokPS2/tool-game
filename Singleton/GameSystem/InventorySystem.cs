using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToolGame.Container;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class InventorySystem : BaseSystem
{
	public static InventorySystem Instance = null!;

	public override void _EnterTree()
	{
		base._EnterTree();
		Instance = this;
	}

	public static IItem? GetSelectedItem(ulong entityId)
	{
		Inventory? inventoryComp = ECSManager.GetComponent<Inventory>(entityId);
		if (inventoryComp is null || inventoryComp.Contents.Count() == 0)
			return null;

		else
			return inventoryComp.Contents[inventoryComp.Selected];
	}
}
