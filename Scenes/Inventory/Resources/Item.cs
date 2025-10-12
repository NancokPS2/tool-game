public partial class Item : Resource, IInventoryItem
{
	public const string DEFAULT_NAME = "UNNAMED ITEM";

	[Export]
	public string ItemName { get; set; } = DEFAULT_NAME;
	[Export]
	public Texture2D? Icon { get; set; }
	
}
