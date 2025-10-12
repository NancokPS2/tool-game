
using System.Linq;
using ToolGame.Scenes.MachinePart.Connector;

[GlobalClass]
public abstract partial class ConnectorCable3D : Node3D, IConnector
{
	[Export]
	public EConnectorType Filter { get; set; }
	public Dictionary<uint, IConnectorPort> Connections { get; set; } = new();

	public bool IsFitForPort(IConnectorPort port)
		=> port.ConnectorFilters.Contains(Filter);

	public virtual void Connect(IConnectorPort port, uint side)
	{
		if (!IsValid(port, side))
			throw new Exception($"{port} is not valid.");

		if (!GetSides().Contains(side))
			throw new IndexOutOfRangeException($"Invalid side {side}, valid ones are {GetSides()}");

		if (!IsFitForPort(port))
			throw new Exception($"This connector has a filter {Filter} but the port has {port.ConnectorFilters.ToStringList()}");

		Connections[side] = port;
	}
	public abstract uint[] GetSides();

	public abstract bool IsValid<TConnected>(TConnected connected, uint side) where TConnected : IConnectorPort;
}
