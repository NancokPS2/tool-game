using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Inputs;
using ToolGame.Singleton;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class InteractionSystem : BaseSystem
{
	public delegate void InteractionEvent(InteractionContext context);
	public static event InteractionEvent? Interacted;

	public InteractionSystem()
	{

	}

	public override void _SystemProcess(double delta)
	{
		base._SystemProcess(delta);

		foreach (IEntity entity in GetInGroup(CompGroups.INTERACTION))
		{
			//Enable it based on input.
			IInteractionSource? source = entity.GetComponent<IInteractionSource>();
			if (source is null) continue;

			source.InteractionActive = IsSourceActive(entity);
			source.CurrentDetected = GetSourceDetected(source);

			//The target is valid and the interaction is active, interact!
			if (source.CurrentDetected is not null && source.InteractionActive)
			{
				//Anounce that it was interacted with.
				Interacted?.Invoke(
					new InteractionContext(
						source.Responsible ?? throw new Exception(),
						null,
						source.CurrentDetected
					)
				);
			}
		}
	}

	private IInteractionTarget? GetSourceDetected(IInteractionSource source)
	{
		if (source is RayCast3D raycast)
		{
			return raycast.GetCollider() as IInteractionTarget;
		}
		else
			return null;
	}

	public bool IsSourceActive(IEntity entity)
	{
		bool inputActive = entity.GetComponent<InputComponent>()?.InputsActive[InputNames.INTERACT] ?? false;
		return inputActive;
	}
}
