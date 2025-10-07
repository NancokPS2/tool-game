using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Container;
using ToolGame.Scenes.Items;
using ToolGame.Scenes.Mob;

namespace ToolGame;

[GlobalClass]
public partial class InventoryUI : Control
{
	[Export]
	public Texture2D TextureEmpty = null!;

	[Export]
	public Inventory InventoryComponent = null!;

	[Export]
	public Label ItemName = null!;

	[Export]
	public ItemPickupRay3D PickupRay = null!;

	[Export]
	Godot.Collections.Array<TextureRect> InventorySlotDisplay = new();

	public override void _Process(double delta)
	{
		base._Process(delta);
		int slot = 0;
		foreach (var item in InventorySlotDisplay)
		{
			if (InventoryComponent.Contents.Count() <= slot)
			{
				item.Texture = TextureEmpty;
				item.Modulate = Colors.White;
			}
			else
			{
				item.Texture = InventoryComponent.Contents[slot]?.Icon ?? TextureEmpty;
				item.Modulate = InventoryComponent.Selected == slot ? Colors.Green : Colors.White;
			}

			slot++;
		}

		if (PickupRay.GetCollider() is ItemPickupArea3D area)
		{
			ItemName.Text = area.GetItem()?.ItemName ?? "UNKNOWN";
		}
		else
			ItemName.Text = (PickupRay.GetCollider() as Node)?.Name ?? "NONE";
	}
}
