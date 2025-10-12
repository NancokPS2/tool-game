public partial class InventoryChangeContext
{
	public int Slot { protected set; get; }
	public IInventoryItem? NewContent { protected set; get; }
	public IInventoryItem? OldContent { protected set; get; }
	public EInventoryChangeResult Result { protected set; get; }
	public bool AllowReplacing = false;

	public InventoryChangeContext(int slot, IInventoryItem? newContent)
	{
		Slot = slot;
		NewContent = newContent;
	}

	public void SetOldContent(IInventoryItem? item)
	{
		OldContent = item;
	}

	public void SetResult(EInventoryChangeResult result)
	{
		Result = result;
	}
}
