using ToolGame.Scenes.MachinePart.Connector;

[Obsolete]
public abstract partial class ConnectorPort3D<TUser> : Node3D, IConnectorPort
{
	public List<EConnectorType> ConnectorFilters { set; get; } = new();

	[Export]
	protected Godot.Collections.Array<EConnectorType> connectorFilters
	{
		set => ConnectorFilters = new(value);
		get => new(ConnectorFilters);
	}


}
