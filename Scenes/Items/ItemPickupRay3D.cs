using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Container;
using ToolGame.Scenes.Items;

namespace ToolGame.Scenes.Mob;

[GlobalClass]
public partial class ItemPickupRay3D : RayCast3D
{
	[Export]
	public Inventory InventoryComponent = null!;

	public override void _Ready()
	{
		base._Ready();
		CollideWithAreas = true;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event.IsActionPressed("ui_accept"))
		{
			GodotObject? collider = GetCollider();
			if (collider is ItemPickupArea3D itemPick)
			{
				AttemptPickup(itemPick);
			}
		}
	}

	private void AttemptPickup(ItemPickupArea3D itemPick)
	{
		int slot = InventoryComponent.GetEmptySlot();
		InventoryComponent.Contents[slot] = itemPick.GetItem().Pickup();
	}
}
