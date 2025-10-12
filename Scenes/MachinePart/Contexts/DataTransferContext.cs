public class DataTransferContext
{
	public IDataProvider Provider;
	public IDataReceiver Receiver;
	public string Data;

	public DataTransferContext(IDataProvider provider, IDataReceiver receiver)
	{
		Provider = provider;
		Receiver = receiver;
		Data = provider.GetData();
	}
}
