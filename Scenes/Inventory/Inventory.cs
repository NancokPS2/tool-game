using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography;
using ToolGame;

[GlobalClass]
public partial class Inventory : Node, IInventory
{
	public event IInventory.ContentChange? ContentsChanged;

	[Export]
	protected int MinimumSlots;

	[Export]
	protected Godot.Collections.Dictionary<int, Item?> contents
	{
		set
		{
			Contents = value.ToDictionary(
				pair => pair.Key,
				pair => pair.Value as IInventoryItem ?? null
			);
			foreach (var item in Contents.Values)
			{
				(item as Node)?.GetParent().RemoveChild(item as Node);
			}
		}

		get
		{
			return new(Contents.ToDictionary(
			pair => pair.Key,
			pair => pair.Value as Item
				)
			);
		}
	}
	protected Dictionary<int, IInventoryItem?> Contents { set; get; } = new();

	[Export]
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

	public override void _Ready()
	{
		base._Ready();
		for (int i = 0; i < MinimumSlots; i++)
		{
			Contents.TryAdd(i, null);
		}
	}

	public IInventoryItem? GetItem(int slot)
	{
		Contents.TryGetValue(slot, out IInventoryItem? item);
		return item;
	}

	public int GetEmptySlot()
	{
		int[] emptySlots = (from pair in Contents where pair.Value == null select pair.Key).ToArray();
		if (emptySlots.Count() == 0)
			return IInventory.INVALID_SLOT;
		else
			return emptySlots[0];

	}

	public void ChangeItem(InventoryChangeContext context)
	{
		int slot = context.Slot;
		//INVALID_SLOT, find any empty slot.
		if (slot == IInventory.INVALID_SLOT)
		{
			slot = GetEmptySlot();
		}

		//Returned INVALID_SLOT again, it couldn't find any empty ones.
		if (slot == IInventory.INVALID_SLOT)
		{
			context.SetResult(EInventoryChangeResult.NOT_ENOUGH_SPACE);
		}

		//It does not have this slot.
		else if (!HasSlot(slot))
		{
			context.SetResult(EInventoryChangeResult.INVALID_SLOT);
		}

		//The slot is occupied and it is not replacing anything.
		else if (GetItem(slot) != null && !context.AllowReplacing)
		{
			context.SetResult(EInventoryChangeResult.OCCUPIED);
		}

		else
		{
			context.SetResult(EInventoryChangeResult.SUCCESS);
		}

		if (context.Result == EInventoryChangeResult.SUCCESS)
		{
			context.SetOldContent(GetItem(slot));
			Contents[slot] = context.NewContent;
			context.SetResult(EInventoryChangeResult.SUCCESS);
			ContentsChanged?.Invoke(context);
		}
		else
		{
			Log.Error($"Failed to change item in slot {context.Slot} due to {context.Result}");
			return;
		}

	}

	public bool HasSlot(int slot)
	{
		return Contents.ContainsKey(slot);
	}

	public int[] GetAllSlots()
		=> Contents.Keys.ToArray();
}
