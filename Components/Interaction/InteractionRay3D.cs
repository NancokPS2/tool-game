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
		set => Responsible = value as IEntity ?? throw new Exception($"Node at path {value?.GetPath()} is not an IEntity.");
		get => Responsible as Node;
	}

	public IEntity? Responsible { get; set; }

	public IInteractionTarget? CurrentDetected { get; set; }

	public bool InteractionActive { get; set; }

	[Export]
	protected bool UseGlobalInput = true;

	public override void _EnterTree()
	{
		base._EnterTree();
		CollideWithAreas = true;

	}

	public string[] GetProcessingGroups() => [CompGroups.INTERACTION];
}
