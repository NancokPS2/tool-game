using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Context;

public class PowerMetricsContext
{
	public ulong EntityId;
	public double PowerCurrent;
	public double PowerMax;

	public PowerMetricsContext(ulong entityId, double powerCurrent, double powerMax)
	{
		EntityId = entityId;
		PowerCurrent = powerCurrent;
		PowerMax = powerMax;
	}
}
