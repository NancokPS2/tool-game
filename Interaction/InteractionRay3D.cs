using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interaction;

/// <summary>
/// Detects physical objects like areas and physics bodies.
/// </summary>
[GlobalClass]
public partial class InteractionRay3D : RayCast3D, IInteractionSource
{
	[Export]
	protected Node? responsible
	{
		set => Responsible = value as ICreature;
		get => Responsible as Node;
	}
	public ICreature? Responsible { get; set; }

	public IInteractionTarget? TargetCurrent { get; set; }

	public bool InteractionActive { get; set; }

	[Export]
	protected bool UseGlobalInput = true;

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
		if (collider is IInteractionTarget inter)
		{
			TargetCurrent = inter;
		}
	}
}
