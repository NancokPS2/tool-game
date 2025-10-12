public interface IPowerConsumer
{
	public long PowerConsumed { set; get; }

	public bool IsPowerSatisfied();
}
