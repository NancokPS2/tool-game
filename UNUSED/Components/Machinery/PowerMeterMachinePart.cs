using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Machinery;

[GlobalClass]
public partial class PowerMeterMachinePart : MachinePart
{
	[Export]
	public PowerMeter3D? PowerMeter;

	public override string[] GetProcessingGroups() => [CompGroups.POWER_METER];
}
