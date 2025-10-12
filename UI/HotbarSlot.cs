[GlobalClass]
public partial class HotbarSlot : TextureRect
{
	public delegate void ItemChangeEventHandler(IInventoryItem? item);
	public event ItemChangeEventHandler? ItemChanged;

	[Export]
	public Texture2D? EmptyTexture { get => emptyTexture; set => emptyTexture = value; }
	private Texture2D? emptyTexture;

	public IInventoryItem? Item { protected set; get; }

	public void SetItem(IInventoryItem? item)
	{
		Item = item;
		Texture = item?.Icon ?? EmptyTexture;
		ItemChanged?.Invoke(item);
	}
}
