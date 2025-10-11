namespace ToolGame.Scenes.MachinePart.Power.Contexts;

public class PowerTransferContext
{
	public IPowerContainer Source;
	public IPowerContainer Target;
	public double TransferRate;
	public double Delta;

	public PowerTransferContext(IPowerContainer source, IPowerContainer target, double transferRate, double delta)
	{
		Source = source;
		Target = target;
		TransferRate = transferRate;
		Delta = delta;
	}
}
