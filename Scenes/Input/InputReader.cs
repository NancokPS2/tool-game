using System.Linq;

[GlobalClass]
public partial class InputReader : Node, IInputReader
{
	[Export]
	public int InputDevice { set; get; } = InputManager.NO_DEVICE;

/* 
	public string[] Actions { set; get; } = InputManager.ACTION_NAMES;
	[Export]
	protected Godot.Collections.Array<string> actions
	{
		set => Actions = value.Count == 0 || value is null ? InputManager.ACTION_NAMES : [.. value];
		get => [.. Actions];
	}
 */
	public List<IInputReceiver> Receivers { get; set; } = new();
	[Export]
	protected Godot.Collections.Array<Node> receivers
	{
		set => Receivers = [.. value.OfType<IInputReceiver>()];
		get => [.. Receivers.OfType<Node>()];
	}

	public override void _EnterTree()
	{
		base._EnterTree();
		InputManager.AddReceiver(InputDevice, null);
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		InputManager.RemoveReceiver(InputDevice, null);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		foreach (var receiver in Receivers)
		{
			foreach (var action in receiver.TriggeringActions)
			{
				if (GetActionStrength(action) > 0.5)
				{
					receiver.ReceiveInput(action);
					break;
				}
			}
		}
	}

	public float GetActionStrength(string actionName)
	{
		return InputManager.GetInputStrength(InputDevice, actionName);
	}
}
