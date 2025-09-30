using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interaction;

public partial class InteractionRay3D : RayCast3D
{
	public override void _EnterTree()
	{
		base._EnterTree();
		CollideWithAreas = true;
		AddToGroup(NodeGroups.INTERACTION_RAY);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		var collider = GetCollider();
		if (collider is InteractionArea3D inter)
		{
		}
	}

	public void ForceInteraction(IInteractionTarget target)
	{

	}
}
