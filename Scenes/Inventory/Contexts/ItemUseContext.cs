public class ItemUseContext
{
	public IItemHolder Source;
	public IItemReceiver Receiver;

	public ItemUseContext(IItemHolder source, IItemReceiver receiver)
	{
		Source = source;
		Receiver = receiver;
	}
}
