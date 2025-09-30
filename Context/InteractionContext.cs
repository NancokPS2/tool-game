namespace ToolGame.Context;

public class InteractionContext
{
	public ICreature Responsible;
	public IInventory? Hand;
	public IInteractionTarget Target;

	public InteractionContext(ICreature responsible, IInventory? hand, IInteractionTarget target)
	{
		Responsible = responsible;
		Hand = hand;
		Target = target;
	}
}
