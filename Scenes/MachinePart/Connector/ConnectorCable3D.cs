
using System.Linq;
using ToolGame.Scenes.MachinePart.Connector;

[GlobalClass]
public abstract partial class ConnectorCable3D : Node3D, IConnector
{
	[Export]
	public EConnectorType Filter { get; set; }

	public bool IsFitForPort(IConnectorPort port)
		=> port.ConnectorFilters.Contains(Filter);

	public virtual void Connect(IConnectorPort port, uint side)
	{
		if (!GetSides().Contains(side))
			throw new IndexOutOfRangeException($"Invalid side {side}, valid ones are {GetSides()}");

		if (!IsFitForPort(port))
			throw new Exception();
	}
	public abstract uint[] GetSides();
}
