namespace ToolGame.Context;

public class InteractionContext
{
	public ulong Responsible;
	public IInteractionTarget Target;

	public InteractionContext(ulong responsible, IInteractionTarget target)
	{
		Responsible = responsible;
		Target = target;
	}
}
