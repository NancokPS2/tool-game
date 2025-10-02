namespace ToolGame.Context;

public class InteractionContext
{
	public IMob Responsible;
	public IInventory? Hand;
	public IInteractionTarget Target;

	public InteractionContext(IMob responsible, IInventory? hand, IInteractionTarget target)
	{
		Responsible = responsible;
		Hand = hand;
		Target = target;
	}
}
