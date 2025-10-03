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
			//Process target cooldowns.
			var target = entity.GetComponent<IInteractionTarget>();
			if (target is not null)
				ProcessTargetCooldown(target, delta);

			//Get the source component.
			IInteractionSource? source = entity.GetComponent<IInteractionSource>();
			if (source is null)
				continue;

			//Get the target.
			source.CurrentDetected = GetDetected(source);
			//Enable it based on input.
			source.InteractionActive = IsSourceInputActive(entity);
			
			//The target is valid and the interaction is active, interact!
			if (source.CurrentDetected is not null && source.InteractionActive && IsTargetCooldownReady(source.CurrentDetected))
			{
				source.CurrentDetected.InteractionCooldownCurrent = 0;
				
				//Anounce that it was interacted with.
				Interacted?.Invoke(
					new InteractionContext(
						entity.GetEntityId(),
						source.CurrentDetected
					)
				);
			}
		}
	}

	protected static void UpdateRaycastSource(IInteractionSource source)
	{
		if (source is RayCast3D raycast)
			source.CurrentDetected = raycast.GetCollider() is IInteractionTarget target ? target : null;
	}

	protected static IInteractionTarget? GetDetected(IInteractionSource source)
	{
		if (source is RayCast3D raycast)
		{
			source.CurrentDetected = raycast.GetCollider() is IInteractionTarget target ? target : null;
		}
		return source.CurrentDetected;
	}

	protected void ProcessTargetCooldown(IInteractionTarget target, double delta)
	{
		target.InteractionCooldownCurrent += delta;
	}

	public bool IsSourceInputActive(IEntity entity)
	{
		bool inputBool = false;
		entity.GetComponent<InputComponent>()?.InputsActive.TryGetValue(InputNames.INTERACT, out inputBool);

		return inputBool;
	}

	public bool IsTargetCooldownReady(IInteractionTarget target)
		=> target.InteractionCooldownCurrent > target.InteractionCooldown;
}
