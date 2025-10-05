using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class PhysicsSystem : BaseSystem
{
	public PhysicsSystem()
	{
		SyncToProcess = EProcessMode.PHYSICS;
	}

	public override void _SystemProcess(double delta)
	{
		base._SystemProcess(delta);
		foreach (var entity in GetInGroup(CompGroups.PHYSICS))
		{

		}
	}
}
