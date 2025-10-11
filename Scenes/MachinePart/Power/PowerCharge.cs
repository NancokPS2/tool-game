using System.Numerics;

public class PowerCharge
{
	/// <summary>
	/// The power this started with.
	/// </summary>
	public double InitialPower { protected set; get; }
	/// <summary>
	/// Current power stored.
	/// </summary>
	public double Power { protected set; get; }
	/// <summary>
	/// The amount of power that can be drawn per call, meant to limit the amount of power each recipient can take.
	/// </summary>
	public double DrawCap { protected set; get; } = double.MaxValue;

	public PowerCharge(double initialPower)
	{
		InitialPower = initialPower;
		Power = InitialPower;
	}

	public double DrawPower(double power)
	{
		double totalDraw = Math.Clamp(power, 0, Math.Min(Power, DrawCap));
		Power -= totalDraw;
		return totalDraw;
	}

	public void SetDrawCap(double cap)
		=> DrawCap = cap;

	public void AddChargePower(PowerCharge b)
	{
		Power += b.Power;
	}
}
