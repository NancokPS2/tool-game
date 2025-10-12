[GlobalClass]
public partial class ConnectorDataStringCable3D : ConnectorCable3D
{
	[Export]
	protected Node3D? DataProvider;

	[Export]
	protected DataDisplay3D? DataReceiver;

	public override void _Ready()
	{
		base._Ready();
		if (DataProvider is IConnectorPort providerPort && providerPort is IDataProvider)
			Connect(providerPort, 0);
		else if (DataProvider is not null)
			throw new Exception();
			
		if (DataReceiver is IConnectorPort receiverPort && receiverPort is IDataReceiver)
			Connect(receiverPort, 1);
		else if (DataProvider is not null)
			throw new Exception();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		IDataProvider? dataProvider = GetProvider();
		IDataReceiver? dataReceiver = GetReceiver();
		if (dataProvider is null || dataReceiver is null)
			return;

		IDataProvider.TransferData(
			new DataTransferContext(
				dataProvider,
				dataReceiver
			)
		);
	}

	protected IDataProvider? GetProvider()
		=> Connections[0] as IDataProvider;

	protected IDataReceiver? GetReceiver()
		=> Connections[1] as IDataReceiver;

	public override uint[] GetSides() => [0, 1];

	public override bool IsValid<TConnected>(TConnected connected, uint side)
	{
		if (side == 0 && connected is IDataProvider)
			return true;
		else if (side == 1 && connected is IDataReceiver)
			return true;
		else
			return false;
	}
}
