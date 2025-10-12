using System.Numerics;
using System.Runtime.CompilerServices;

[Obsolete]
public struct InputActionSet
{
	public static readonly InputActionSet DEFAULT = new(
		new()
		{
			"interact",
			"move_forward",
			"move_backward",
			"move_left",
			"move_right"
		}
	);
	private List<string> Actions = new();

	public InputActionSet(List<string> actions)
	{
		Actions = actions;
	}

	public string this[int index]
	{
		set => Actions[index] = value;
		get => Actions[index];
	}
}

