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

	public IPowerContainer? ContainerA;
	[Export]
	protected PowerContainer3D? containerA
	{
		set => ContainerA = value;
		get => ContainerA as PowerContainer3D ?? null;
	}

	public IPowerContainer? ContainerB;
	[Export]
	protected PowerContainer3D? containerB
	{
		set => ContainerB = value;
		get => ContainerB as PowerContainer3D ?? null;
	}

	[Export]
	public double TransferRate = 10;

	public override void Connect(IConnectorPort port, uint side)
	{
		base.Connect(port, side);

		if (port is IPowerContainer container)
		{
			switch (side)
			{
				case 0:
					ContainerA = container;
					break;

				case 1:
					ContainerB = container;
					break;

				default:
					throw new Exception();
			}
		}

	}

	public override uint[] GetSides() => [0, 1];

	public override void _Ready()
	{
		base._Ready();
		if (containerA is IPowerContainer)
			Connect(containerA, 0);

		if (containerB is IPowerContainer)
			Connect(containerB, 1);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		//Abort if there's no containers connected.
		if (ContainerA == null || ContainerB == null)
			return;

		switch (PowerDirection)
		{
			case EPowerDirection.FORWARD:
				IPowerContainer.TransferPower(
					new(
						ContainerA,
						ContainerB,
						TransferRate,
						delta)
					);
				break;

			case EPowerDirection.REVERSE:
				IPowerContainer.TransferPower(
					new(
						ContainerB,
						ContainerA,
						TransferRate,
						delta)
					);
				break;

			case EPowerDirection.EQUALIZE:
				if (ContainerA.Stored > ContainerB.Stored)
					IPowerContainer.TransferPower(
					new(
						ContainerA,
						ContainerB,
						TransferRate,
						delta)
					);
				else if (ContainerA.Stored < ContainerB.Stored)
					IPowerContainer.TransferPower(
					new(
						ContainerB,
						ContainerA,
						TransferRate,
						delta)
					);
				break;

			default:
				throw new Exception();
		}
	}
}
