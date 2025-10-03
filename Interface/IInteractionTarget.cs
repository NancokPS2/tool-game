namespace ToolGame.Interface;

public interface IInteractionTarget : IComponent
{
	//public IMob? CurrentInteractor { set; get; }
	public double InteractionCooldownCurrent { set; get; }
	public double InteractionCooldown { set; get; }
}
