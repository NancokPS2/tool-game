using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Singleton;

namespace ToolGame.Singleton.System;

[GlobalClass]
public partial class InteractionSystem : BaseSystem
{
	public delegate void InteractionEvent(InteractionContext context);
	public static event InteractionEvent? Interacted;

	public InteractionSystem()
	{

	}

	public override void _SystemProcess(long delta)
	{
		base._SystemProcess(delta);

		var interactors =
			GetTree()
			.GetNodesInGroup(NodeGroups.INTERACTION_RAY)
			.OfType<IInteractionSource>()
			.Where(x => x.Responsible is not null);

		foreach (var source in interactors)
		{
			//The target is valid and the interaction is active, interact!
			if (source.TargetCurrent is IInteractionTarget target && source.InteractionActive)
			{
				target.CurrentInteractor = source.Responsible;

				//Anounce that it was interacted with.
				Interacted?.Invoke(
					new InteractionContext(
						source.Responsible ?? throw new Exception(),
						null,
						target
					)
				);
			}
		}
	}
}
