using Godot;

namespace ToolGame.Machinery;

public partial class MachinePart : Node, IComponent, IItem
{
	[Export]
	public EPartCategory Category;

	public string[] GetProcessingGroups() => [];
}
