public interface IInventory
{
	public const int INVALID_SLOT = int.MaxValue;
	public delegate void ContentChange(InventoryChangeContext context);
	public event ContentChange ContentsChanged;

	public void ChangeItem(InventoryChangeContext context);
	public bool HasSlot(int slot);
	public IInventoryItem? GetItem(int slot);
	public int[] GetAllSlots();
}
