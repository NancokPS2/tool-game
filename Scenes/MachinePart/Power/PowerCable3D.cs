[GlobalClass]
public partial class PowerCable3D : Node3D
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
