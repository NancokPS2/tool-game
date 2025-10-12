public interface IDataProvider
{
	public const string NO_DATA = "ERROR_NO_DATA";
	public string GetData();

	public static void TransferData(DataTransferContext context)
	{
		if (context.Provider is null || context.Receiver is null)
			throw new Exception();

		context.Receiver.Receive(context.Provider.GetData());
	}
}
