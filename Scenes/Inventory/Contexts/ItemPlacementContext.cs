public class ItemPlacementContext
{
	public Node3D NewParent { protected set; get; }
	public IPickup Item { protected set; get; }
	public Vector3 GlobalPosition { protected set; get; }

	public ItemPlacementContext(Node3D newParent, IPickup item)
	{
		if (!NewParent?.IsInsideTree() ?? false)
			throw new Exception();

		NewParent = newParent;
		Item = item;
		GlobalPosition = newParent.GlobalPosition;
	}

	public void SetGlobalPositionOverride(Vector3 vector)
	{
		GlobalPosition = vector;
	}
}
