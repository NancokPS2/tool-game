[GlobalClass]
public partial class ItemUse3D : Node3D, IInputReceiver
{
	public IItemHolder Holder;

	[Export]
	public RayCast3D DetectionRaycast = null!;

	public string[] TriggeringActions { get; set; } = [InputManager.INPUT_USE];

	public void ReceiveInput(string action)
	{
		IItemReceiver? receiver = DetectionRaycast.GetCollider() as IItemReceiver;
		IInventoryItem? item = Holder.GetHeldItem();
		if (receiver is null || item is null)
			return;

		receiver.ReceiveItem(item);
	}
}
