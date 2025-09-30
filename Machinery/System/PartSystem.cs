namespace ToolGame.Machinery;

public partial class PartSystem : MachinerySystem
{
	public void ParseUseContext(UseContext context)
	{
		if (
			context.Hand.Selected is MachinePart3D part
			&& context.Target is Machine3D machine
			)
		{
			TryInsertPart(new(machine));
		}
	}
	
	public void TryInsertPart(ChangeMachinePartContext context)
	{
		if (SlotIsPartCompatible(context.Slot, context.Part))
		{
			context.Slot.Part = context.Part;
		}
	}

	protected bool SlotIsPartCompatible(MachineSlot slot, MachinePart3D part)
	{
		bool allowedCategory = slot.CategoriesAllowed.Contains(part.Category);
		
		return allowedCategory;
	}
}
