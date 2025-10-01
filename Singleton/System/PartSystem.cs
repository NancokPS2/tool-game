namespace ToolGame.Singleton.System;

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
		if (
			context.Hand?.Selected is MachinePart3D part
			&& context.Target is MachineSlot3D slot
			)
		{
			TryInsertPart(new ChangeMachinePartContext(slot, part, ChangeMachinePartContext.EPartChange.INSERTED));
			Log.Info($"Inserted {part} into {slot}");
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
