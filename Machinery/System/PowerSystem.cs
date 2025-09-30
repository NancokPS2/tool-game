using System;

namespace ToolGame.Machinery;

public partial class PowerSystem : MachinerySystem
{

	public void MachineAddPower(ChangePowerMachineContext context)
	{
		foreach (var item in context.PowerContainers)
		{
			long powerAdded = Math.Clamp(context.Amount, 0, item.GetCapacityRemaining());
			item.StoredPower += powerAdded;
			context.Amount -= powerAdded;

			if (context.Amount <= 0)
				break;
		}
	}
	
}
