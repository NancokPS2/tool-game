namespace ToolGame.Machinery;

[GlobalClass]
public partial class BatteryMachinePart : MachinePart, IPowerContainer
{
	private const double POWER_DEFAULT = 100;

	[Export]
	public double Stored { get => EndlessPower ? Max : powerStored; set => powerStored = value; }

	[Export]
	public double Max { get; set; } = POWER_DEFAULT;
	
	[Export]
	public double PowerDrain { get; set; } = 0.5;

	[Export]
	public bool EndlessPower;

	private double powerStored = POWER_DEFAULT;

	public void AddPower(PowerCharge context)
	{
		throw new NotImplementedException();
	}
}
