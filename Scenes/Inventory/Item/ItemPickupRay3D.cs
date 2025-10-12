using System.ComponentModel;
using ToolGame;

[GlobalClass]
public partial class ItemPickupRay3D : RayCast3D, IInputReceiver
{

	/* 
	public IInputReader? Reader { get; set; }
	[Export]
	protected InputReader? reader
	{
		set => Reader = value;
		get => Reader as InputReader ?? null;
	} 
	*/

	public string[] TriggeringActions { get; set; } = [InputManager.INPUT_INTERACT];
	[Export]
	protected Godot.Collections.Array<string> triggeringActions
	{
		set => TriggeringActions = [.. value];
		get => [.. TriggeringActions];
	}

	[Export]
	public Inventory InventoryComponent = null!;

	public override void _Ready()
	{
		base._Ready();
		CollideWithAreas = true;
	}

	private void AttemptPickup(ItemPickupArea3D itemPick)
	{
		if (InventoryComponent is null)
			return;
			
		int slot = InventoryComponent.GetEmptySlot();
		IInventoryItem? item = itemPick.GetItem().Pickup() as IInventoryItem;
		
		InventoryComponent.Contents[slot] = item;
	}

	public void ReceiveInput(string action)
	{
		var collider = GetCollider() as ItemPickupArea3D;
		if (collider is null)
		{
			Log.Info($"{GetPath()} tried to pick up {collider?.GetPath() ?? "nothing"} and failed as it was not a valid item.");
			return;
		}

		AttemptPickup(collider);
		Log.Info($"{GetPath()} picked up {collider.GetPath()}");
	}
}
