using Godot;

namespace ToolGame.Machinery;

public partial class MachinePart : Node3D, IComponent, IItem
{
	[Export]
	public EPartCategory Category;

	[Export]
	public PackedScene? ItemScene { get; set; }
	[Export]
	public Texture2D? Icon { get; set; }

	public virtual string[] GetProcessingGroups() => [CompGroups.MACHINERY];
}
