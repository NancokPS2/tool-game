using ToolGame;

[GlobalClass]
public partial class ItemPickup3D : Node3D, IInputReceiver
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

	[Export]
	public RayCast3D PickupRaycast = null!;

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
		if (!PickupRaycast.CollideWithAreas)
			Log.Error($"This ItemPickup3D probably can't use this RayCast3D because it cannot collide with areas.");
		
	}

	private void AttemptPickup(IPickup pickup)
	{
		if (InventoryComponent is null)
		{
			Log.Info($"{this} tried to pick up {pickup}, but there was no inventory to put it on.");
			return;
		}

		//Get the item
		IInventoryItem? item = pickup.Pickup();

		//Add it to the inventory.
		InventoryComponent.ChangeItem(
			new(IInventory.INVALID_SLOT, item)
			);
		Log.Info($"{this} picked up {pickup}.");
	}

	public void ReceiveInput(string action)
	{
		var collider = PickupRaycast.GetCollider() as ItemPickupArea3D;
		if (collider is null)
		{
			Log.Info($"{GetPath()} tried to pick up {collider?.GetPath() ?? "nothing"} and failed as it was not a valid item.");
			return;
		}

		AttemptPickup(collider);
		Log.Info($"{GetPath()} picked up {collider.GetItem()}");
	}
}
