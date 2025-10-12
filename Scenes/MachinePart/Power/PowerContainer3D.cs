
using ToolGame.Scenes.MachinePart.Connector;

[GlobalClass]
public partial class PowerContainer3D : Node3D, IPowerContainer, IConnectorPort
{
	private const int BASE_POWER = 1000;

	[Export]
	public double Stored { get => stored; set => stored = Math.Clamp(value, 0, Max); }
	private double stored = BASE_POWER / 2;

	[Export]
	public double Max { set; get; } = BASE_POWER;


	public List<EConnectorType> ConnectorFilters { get; set; } = new(){EConnectorType.POWER_UNIVERSAL};

	[Export]
	protected Godot.Collections.Array<EConnectorType> connectorFilters
	{
		set => ConnectorFilters = [.. value];
		get => [.. ConnectorFilters];
	}
}
