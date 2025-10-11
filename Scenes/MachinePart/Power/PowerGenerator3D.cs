using System.Linq;

[GlobalClass]
public partial class PowerGenerator3D : Node3D, IPowerSource
{
	[Export]
	public double Generated { set; get; } = 100;

	public List<IPowerContainer> ContainersConnected = new();
	[Export]
	protected Godot.Collections.Array<PowerContainer3D> containersConnected
	{
		set => ContainersConnected = new(value);
		get => new(ContainersConnected.OfType<PowerContainer3D>());
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		PowerCharge context = new(Generated);
		foreach (var item in ContainersConnected)
		{
			item.AddPower(context);

			if (context.Power == 0)
				break;
		}
	}
}
