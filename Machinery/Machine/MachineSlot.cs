namespace ToolGame.Machinery;

public partial class MachineSlot : Resource
{
	public MachinePart3D? Part;

	[Export]
	public Godot.Collections.Array<EPartCategory> CategoriesAllowed = new();

}
