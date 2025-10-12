using ToolGame.Scenes.MachinePart.Connector;

public interface IConnector
{
	public const string FILTER_UNIVERSAL = "UniversalCableFilter";
	public const string FILTER_POWER = "UniversalPowerCableFilter";

	public EConnectorType Filter { set; get; }
	public Dictionary<uint, IConnectorPort> Connections { set; get; }

	public uint[] GetSides();
	public void Connect(IConnectorPort port, uint side);
	public bool IsValid<TConnected>(TConnected connected, uint side) where TConnected : IConnectorPort;
	public bool IsFitForPort(IConnectorPort port);
}
