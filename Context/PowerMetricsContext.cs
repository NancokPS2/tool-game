using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Context;

public class PowerMetricsContext
{
	public Arch.Core.Entity Entity;
	public double PowerCurrent;
	public double PowerMax;

	public PowerMetricsContext(Arch.Core.Entity entity, double powerCurrent, double powerMax)
	{
		Entity = entity;
		PowerCurrent = powerCurrent;
		PowerMax = powerMax;
	}
}
