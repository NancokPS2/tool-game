public interface IInputReceiver
{
	//public IInputReader? Reader { set; get; }
	public string[] TriggeringActions { set; get; }

	public void ReceiveInput(string action);
}
