using Godot;

namespace ToolGame.Machinery;

public abstract partial class MachinePart3D : Node, IItem
{
	[Export]
	public EPartCategory Category;
}
