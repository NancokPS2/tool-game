using ToolGame.Scenes.MachinePart.Connector;

[GlobalClass]
public partial class DataDisplay3D : Node3D, IDataReceiver, IConnectorPort
{
	[Export]
	protected Label3D LabelNode = null!;

	public List<EConnectorType> ConnectorFilters { get; set; } = new();
	[Export]
	protected Godot.Collections.Array<EConnectorType> connectorFilters
	{
		set => ConnectorFilters = [.. value];
		get => [.. ConnectorFilters];
	}

	public void Receive(string data)
	{
		LabelNode.Text = data;
	}
}
