using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ToolGame.Creature;

[GlobalClass]
public partial class MobileComponent : Node, IComponent
{
	[Export]
	public float SpeedMax;

	public Vector3 Velocity;

	public string[] GetProcessingGroups() => [CompGroups.MOBILE];
}
