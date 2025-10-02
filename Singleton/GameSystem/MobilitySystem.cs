using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Creature;

namespace ToolGame.Singleton.GameSystem;

public partial class CreatureMovementSystem : BaseSystem
{
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		foreach (var entity in GetInGroup(CompGroups.MOBILE))
		{
			if (entity is CharacterBody3D char3D)
			{
				var mobComp = entity.GetComponent<MobileComponent>();
				char3D.Velocity = mobComp?.Velocity ?? Vector3.Zero;

			}
		}
	}
}
