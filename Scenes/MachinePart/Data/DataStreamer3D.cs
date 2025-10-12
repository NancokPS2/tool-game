using ToolGame.Scenes.MachinePart.Connector;

[GlobalClass]
public partial class DataStreamer3D : Node3D, IDataProvider, IConnectorPort
{


	[Export]
	protected double TimePerData = 3;
	protected double Time = 0;

	public int DataIndex = 0;

	public List<string> DataSets = new();
	[Export]
	protected Godot.Collections.Array<string> dataSets
	{
		set => DataSets = [.. value];
		get => [.. DataSets];
	}

	public List<EConnectorType> ConnectorFilters { get; set; } = new();
	[Export]
	protected Godot.Collections.Array<EConnectorType> connectorFilters
	{
		set => ConnectorFilters = [.. value];
		get => [.. ConnectorFilters];
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		Time += delta;

		if (Time > TimePerData)
		{
			Time = 0;
			DataIndex++;
			if (DataIndex >= DataSets.Count)
				DataIndex = 0;
		}
	}

	public string GetData()
	{
		if (DataSets.Count == 0)
			return IDataProvider.NO_DATA;
		else
			return DataSets[DataIndex];
	}
}
