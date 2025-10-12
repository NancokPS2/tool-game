using System.Linq;

[GlobalClass]
public partial class Inventory : Node, IComponent
{
	[Export]
	protected Godot.Collections.Dictionary<int, Node?> contents
	{
		set
		{
			Contents = value.ToDictionary(
				pair => pair.Key,
				pair => pair.Value as IInventoryItem ?? null
			);
			foreach (var item in Contents.Values)
			{
				(item as Node)?.GetParent().RemoveChild(item as Node);
			}
		}

		get
		{
			return new(Contents.ToDictionary(
			pair => pair.Key,
			pair => pair.Value as Node
				)
			);
		}
	}
	public Dictionary<int, IInventoryItem?> Contents = new();

	[Export]
	public int Selected
	{
		get
		{
			return Math.Clamp(selected, 0, Contents.Count());
		}
		set
		{
			selected = Math.Clamp(value, 0, Contents.Count());
		}
	}

	private int selected;

	public string[] GetProcessingGroups() => [CompGroups.INVENTORY];

	public int GetEmptySlot()
		=> Contents.FirstOrDefault(x => x.Value == null).Key;
}
