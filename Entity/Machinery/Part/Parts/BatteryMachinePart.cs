namespace ToolGame.Machinery;

[GlobalClass]
public partial class BatteryMachinePart : MachinePart, IPowerContainer
{
	public bool ProvidesPower { get; set; }
	public long StoredPower { get => EndlessPower ? StoredPowerMax : storedPower; set => storedPower = value; }
	public long StoredPowerMax { get; set; }

	[Export]
	public bool EndlessPower;

	private long storedPower;
}
