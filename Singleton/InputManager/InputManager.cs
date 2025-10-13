[GlobalClass]
public partial class InputManager : Node
{
	/// <summary>
	/// Used to signify that the default actions are being targeted.
	/// </summary>
	public const int NO_DEVICE = int.MaxValue;
	public const string INPUT_INTERACT = "interact";
	public const string INPUT_MOVE_FORWARD = "move_forward";
	public const string INPUT_MOVE_BACKWARD = "move_back";
	public const string INPUT_MOVE_LEFT = "move_left";
	public const string INPUT_MOVE_RIGHT = "move_right";
	public const string INPUT_NEXT_ITEM = "next_item";
	public const string INPUT_PREV_ITEM = "prev_item";
	public const string INPUT_USE = "use";
	public static InputManager Instance = null!;
	public static readonly string[] ACTION_NAMES =
	{
		INPUT_INTERACT,
		INPUT_MOVE_FORWARD,
		INPUT_MOVE_BACKWARD,
		INPUT_MOVE_LEFT,
		INPUT_MOVE_RIGHT,
		INPUT_NEXT_ITEM,
		INPUT_PREV_ITEM,
	};

	public override void _Ready()
	{
		base._Ready();
		Instance = this;
		ValidateActions();
	}

	#region Static
	public static void AddReceiver(int device, string[]? actions = null)
	{
		//Check if it is a valid device.
		if (device == NO_DEVICE)
			throw new Exception();

		actions ??= ACTION_NAMES;

		foreach (var baseAction in actions)
		{
			string actionName = GetActionName(device, baseAction);
			InputMap.AddAction(actionName);
			foreach (var inputEvent in GetDefaultInputEvents(baseAction))
			{
				inputEvent.Device = device;
				InputMap.ActionAddEvent(actionName, inputEvent);
			}
		}
	}

	public static void RemoveReceiver(int device, string[]? actions = null)
	{
		actions ??= ACTION_NAMES;
		foreach (var baseAction in actions)
		{
			string actionName = GetActionName(device, baseAction);
			InputMap.EraseAction(actionName);
		}
	}

	public static string GetActionName(int device, string action)
		=> device == NO_DEVICE ? $"{action}" : $"{action}_FROMDEVICE_{device}";

	protected static InputEvent[] GetDefaultInputEvents(string actionName)
	{
		return [.. InputMap.ActionGetEvents(actionName)];
	}

	public static void EmulateInput(int device, string action)
	{
		InputEvent inputEvent = (InputEvent)GetDefaultInputEvents(action)[0].Duplicate();
		inputEvent.Device = device;
		Instance.GetViewport().PushInput(inputEvent);
	}

	public static float GetInputStrength(int device, string action)
		=> Input.GetActionStrength(GetActionName(device, action));

	protected static void ValidateActions(int device = NO_DEVICE, string[]? actions = null)
	{
		actions ??= ACTION_NAMES;

		foreach (var action in actions)
		{
			if (!InputMap.HasAction(GetActionName(device, action)))
			{
				throw new Exception($"{action} of device {device} action is not defined in the InputMap");
			}
		}
	}
	#endregion

}
