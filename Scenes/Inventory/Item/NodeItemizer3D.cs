/// <summary>
/// Allows holding a node like an item.
/// </summary>
[GlobalClass]
public partial class NodeItemizer3D : Node3D, IPickup, IInventoryItem
{
	[Export]
	public Node ToItemize = null!;

	protected Node? PreviousParent;

	[Export]
	public string ItemName { get; set; } = "UNNAMED ITEM";
	
	[Export]
	public Texture2D? Icon { get; set; }

	public IPickup Pickup()
	{
		PreviousParent = GetParent();

		ToItemize.GetParent().RemoveChild(ToItemize);

		if (ToItemize.IsInsideTree())
			throw new Exception($"Failed to remove {ToItemize}");

		return this;
	}

	public void Place(ItemPlacementContext context)
	{
		context.NewParent.AddChild(ToItemize);

		SetDeferred("global_position", context.GlobalPosition);
	}
}
