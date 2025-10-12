using ToolGame.Scenes.Inventory.Interfaces;

public partial class ItemPlaceable : Item, IPlaceable
{
	public Node? ContainedNode;
	[Export]
	protected PackedScene? ContainedScene
	{
		set
		{
			containedScene = value;
			ContainedNode = value?.Instantiate() ?? null;
		}

		get => containedScene;
	}
	private PackedScene? containedScene;

	public void Place(ItemPlacementContext context)
	{
		if (ContainedNode is null)
			throw new Exception($"Cannot place item at {context.NewParent.GetPath()} because this ItemPlaceable does not have any nodes inside of it.");

		context.NewParent.AddChild(ContainedNode);

		if (context.GlobalPosition != context.NewParent.GlobalPosition)
			SetDeferred("global_position", context.GlobalPosition);
	}

}
