using System.ComponentModel.Design.Serialization;
using System.Linq;
using ToolGame.Scenes.MachinePart.Power.Contexts;

public interface IPowerContainer
{
	public double Stored { set; get; }
	public double Max { set; get; }

	public void Empty() => Stored = 0;

	public void AddPower(PowerCharge charge)
	{
		Stored += charge.DrawPower(GetCapacityLeft());
	}

	/// <summary>
	/// Removes power from the IPowerContainer and returns a PowerCharge with said power, or the value of Stored if there is not enough stored.
	/// </summary>
	/// <param name="powerDesired">The power to take and put in the PowerCharge</param>
	/// <returns>A PowerCharge with the amount of power that was possible to take.</returns>
	public PowerCharge TakePower(double powerDesired)
	{
		double powerTaken = Math.Clamp(Stored, 0, powerDesired);
		Stored -= powerTaken;
		return new(powerTaken);
	}

	public double GetCapacityLeft() => Max - Stored;

	public static void TransferPower(PowerTransferContext context)
	{
		//Get how much to take from A.
		double desiredTransfer = context.TransferRate * context.Delta;
		desiredTransfer = Math.Clamp(desiredTransfer, 0, context.Target.GetCapacityLeft());

		//Try to take as much power from A as possible to fill up B
		PowerCharge charge = context.Source.TakePower(desiredTransfer);

		//The power taken should not exceed what B can hold.
		if (charge.Power > context.Target.GetCapacityLeft())
			throw new Exception($"The target can hold {context.Target.GetCapacityLeft()} more power, but {charge.Power} power was drawn.");

		//Give it to B
		double priorPower = context.Target.Stored;
		context.Target.AddPower(charge);
		if (context.Target.Stored <= priorPower && context.Target.Stored != context.Target.Max && context.Target.Stored != 0)
			throw new Exception($"This had {priorPower} power, and after receiving a PowerCharge of {charge.Power} power it ended up at {context.Target.Stored}");
	}

	[Obsolete]
	public static void EqualizePower(IPowerContainer[] containers, double delta)
	{
		//Get all the power available
		PowerCharge totalPower = new(0);
		foreach (var container in containers)
		{
			totalPower.AddChargePower(
				container.TakePower(container.Stored * delta)
				);
		}

		totalPower.SetDrawCap(totalPower.Power / containers.Count());

		foreach (var container in containers)
		{
			container.AddPower(totalPower);
		}

		if (totalPower.Power > 0)
			throw new Exception($"After equalizing a total of {totalPower.InitialPower} units of power among {containers.Count()} containers (for {totalPower.DrawCap} power per container). There was still {totalPower.Power} left.");
	}
}
