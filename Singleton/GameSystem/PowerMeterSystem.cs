using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class PowerMeterSystem : BaseSystem
{
	public override void _SystemProcess(double delta)
	{
		base._SystemProcess(delta);
		foreach (var item in GetInGroup(CompGroups.POWER_METER))
		{
			PowerMetricsContext context = PowerSystem.GetPowerMetrics(item.GetEntityId());

			PowerMeterMachinePart? meter = item.GetComponent<PowerMeterMachinePart>();
			if (meter is not null && meter.PowerMeter is not null)
			{
				meter.PowerMeter.Value = (float)Mathf.Remap(context.PowerCurrent, 0, context.PowerMax, 0, 1);
			}
		}
	}
}
