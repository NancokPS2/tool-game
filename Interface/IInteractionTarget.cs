namespace ToolGame.Interface;

public interface IInteractionTarget
{
	public ICreature? CurrentInteractor { set; get; }
}
