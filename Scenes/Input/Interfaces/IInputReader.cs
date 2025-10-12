public interface IInputReader
{
	public List<IInputReceiver> Receivers { set; get; }
	public int InputDevice { set; get; }
	//public string[] Actions { set; get; }
}
