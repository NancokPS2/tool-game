namespace ToolGame.Machinery;

[GlobalClass]
public partial class BatteryMachinePart : MachinePart, IPowerContainer
{
	private const double POWER_DEFAULT = 100;

	[Export]
	public double PowerStored { get => EndlessPower ? PowerStoredMax : powerStored; set => powerStored = value; }

	[Export]
	public double PowerStoredMax { get; set; } = POWER_DEFAULT;
	
	[Export]
	public double PowerDrain { get; set; } = 0.5;

	[Export]
	public bool EndlessPower;

	private double powerStored = POWER_DEFAULT;
}
