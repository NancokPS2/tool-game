using System.Linq;

[GlobalClass]
public partial class PowerCable3D : ConnectorCable3D, IConnector
{
	public enum EPowerDirection
	{
		FORWARD,
		REVERSE,
		EQUALIZE,
	}

	[Export]
	public EPowerDirection PowerDirection;

	[Export]
	protected PowerContainer3D? ContainerNodeA;

	[Export]
	protected PowerContainer3D? ContainerNodeB;

	[Export]
	public double TransferRate = 10;

	public override bool IsValid<TConnected>(TConnected connected, uint side)
		=> connected is IPowerContainer;

	public override uint[] GetSides() => [0, 1];

	public override void _Ready()
	{
		base._Ready();
		if (ContainerNodeA is IPowerContainer)
			Connect(ContainerNodeA, 0);

		if (ContainerNodeB is IPowerContainer)
			Connect(ContainerNodeB, 1);
	}

	protected IPowerContainer? GetContainer(uint side)
	{
		return Connections[side] as IPowerContainer ?? null;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		IPowerContainer? containerA = GetContainer(0);
		IPowerContainer? containerB = GetContainer(1);
		
		//Abort if there's no containers connected.
		if (containerA is null || containerB is null)
			return;

		switch (PowerDirection)
		{
			case EPowerDirection.FORWARD:
				IPowerContainer.TransferPower(
					new(
						containerA,
						containerB,
						TransferRate,
						delta)
					);
				break;

			case EPowerDirection.REVERSE:
				IPowerContainer.TransferPower(
					new(
						containerB,
						containerA,
						TransferRate,
						delta)
					);
				break;

			case EPowerDirection.EQUALIZE:
				if (containerA.Stored > containerB.Stored)
					IPowerContainer.TransferPower(
					new(
						containerA,
						containerB,
						TransferRate,
						delta)
					);
				else if (containerA.Stored < containerB.Stored)
					IPowerContainer.TransferPower(
					new(
						containerB,
						containerA,
						TransferRate,
						delta)
					);
				break;

			default:
				throw new Exception();
		}
	}
}
