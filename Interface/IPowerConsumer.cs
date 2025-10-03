namespace ToolGame.Machinery;

public interface IPowerConsumer : IComponent
{
	public long PowerConsumed { set; get; }
	public bool PowerSatisfied { set; get; }
}
