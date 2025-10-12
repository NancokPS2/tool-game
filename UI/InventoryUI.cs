using System.Linq;
[GlobalClass]
public partial class InventoryUI : Control
{

	public IInventory InventoryComponent = null!;
	[Export]
	protected Inventory? inventoryComponent
	{
		set
		{
			if (value is not null)
				SetInventory(value);
		}

		get => InventoryComponent as Inventory ?? null;
	}


	[Export]
	public Label ItemName = null!;

	[Export]
	public ItemPickup3D ItemPickup = null!;

	[Export]
	public Node HotbarSlotsParent = null!;

	public int HotbarSlotSelected
	{
		get
		{
			return Math.Clamp(hotbarSlotSelected, 0, HotbarSlots.Count);
		}

		set
		{
			hotbarSlotSelected = Math.Clamp(value, 0, HotbarSlots.Count);
		}
	}


	private int hotbarSlotSelected;

	protected List<HotbarSlot> HotbarSlots = new();


	public override void _Ready()
	{
		base._Ready();
		foreach (var item in HotbarSlotsParent.GetChildren())
		{
			if (item is HotbarSlot hotbarSlot)
				HotbarSlots.Add(hotbarSlot);

		}
		UpdateHotbar();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (ItemPickup.PickupRaycast.GetCollider() is ItemPickupArea3D area)
		{
			ItemName.Text = (area.GetItem() as IInventoryItem)?.ItemName ?? "UNKNOWN";
		}
		else
			ItemName.Text = (ItemPickup.PickupRaycast.GetCollider() as Node)?.Name ?? "NONE";
	}

	public void SetInventory(IInventory inventory)
	{
		if (InventoryComponent is not null)
			InventoryComponent.ContentsChanged -= Update;

		InventoryComponent = inventory;

		InventoryComponent.ContentsChanged += Update;

		UpdateHotbar();
	}

	public void Update(InventoryChangeContext context)
	{
		UpdateHotbar();
	}

	public void UpdateHotbar()
	{
		for (int hotbarIndex = 0; hotbarIndex < HotbarSlots.Count(); hotbarIndex++)
		{
			HotbarSlots[hotbarIndex].SetItem(
				InventoryComponent.GetItem(hotbarIndex)
				);
		
		}
	}
}
