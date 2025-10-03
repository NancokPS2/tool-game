using ToolGame.Container;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class PartSystem : MachinerySystem
{
	public override void _Ready()
	{
		base._Ready();
		InteractionSystem.Interacted += ParseUseContext;
	}

	public void ParseUseContext(InteractionContext context)
	{
		if (context.Target is MachineSlot3D slot)
		{
			MachinePart3D? heldItem = InventorySystem.GetSelectedItem(context.Responsible) as MachinePart3D;
			if (heldItem is null)
				return;

			TryInsertPart(new ChangeMachinePartContext(slot, heldItem, ChangeMachinePartContext.EPartChange.INSERTED));
			Log.Info($"Inserted {heldItem} into {slot}");
		}
		Log.Info($"Failed to insert part into {context.Target}");
	}

	public void TryInsertPart(ChangeMachinePartContext context)
	{
		if (SlotIsPartCompatible(context.Slot, context.Part))
		{
			context.Slot.Part = context.Part;
		}
	}

	protected bool SlotIsPartCompatible(MachineSlot3D slot, MachinePart3D part)
	{
		bool allowedCategory = slot.CategoriesAllowed.Contains(part.Category);

		return allowedCategory;
	}
}
