[GlobalClass]
public partial class ItemPickupArea3D : Area3D, IPickup
{

	/// <summary>
	/// The node that will be removed when this item is picked up, as to reflect that this happened.
	/// </summary>
	[Export]
	public Node ItemNode = null!;

	[Export]
	public Texture2D ItemIcon = null!;

	[Export]
	public string ItemName = Item.DEFAULT_NAME;

	public ItemPlaceable GetItem()
		=> new ItemPlaceable()
		{
			ContainedNode = ItemNode,
			Icon = ItemIcon,
			ItemName = ItemName
		};

	public Item Pickup()
	{
		if (!ItemNode.IsInsideTree())
			throw new Exception($"The item node ({ItemNode}) is not inside the tree, what happened?");

		ItemNode.GetParent().RemoveChild(ItemNode);
		
		if (ItemNode.IsInsideTree())
			throw new Exception($"The item node is still at {ItemNode.GetPath()}, couldn't pick up?");

		return GetItem();
	}
}
