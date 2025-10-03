using System.Linq;
using ToolGame.Container;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class PartSystem : BaseSystem
{
	public delegate void MachinePartChange(ChangeMachinePartContext context);
	public event MachinePartChange? MachinePartChanged;

	public override void _Ready()
	{
		base._Ready();
		InteractionSystem.Interacted += ParseUseContext;
	}

	public void ParseUseContext(InteractionContext context)
	{
		//Check if it was a slot.
		MachineSlot? slot = context.Target is MachineSlot foundSlot ? foundSlot : null;

		//Check if there's a part in the entity's hand.
		MachinePart? heldPart = InventorySystem.GetSelectedItem(context.Responsible) is MachinePart part ? part : null;

		if (slot is not null && heldPart is not null)
		{
			InsertPart(context.Target.GetComponentId(), slot, heldPart);
		}
	}

	protected void InsertPart(ulong machineEntityId, MachineSlot slot, MachinePart heldPart)
	{
		ChangeMachinePartContext context = new ChangeMachinePartContext(
				machineEntityId,
				slot,
				heldPart,
				ChangeMachinePartContext.EPartChange.INSERTED);

		//Resolve if it should succeed or not.
		if (!SlotIsPartCompatible(slot, heldPart))
		{
			context.Result = ChangeMachinePartContext.EResult.DOES_NOT_FIT;
		}
		else
		{
			ECSManager.AddComponent(machineEntityId, heldPart);
			slot.Part = heldPart;
			heldPart.Position = slot.Position;
			context.Result = ChangeMachinePartContext.EResult.SUCCESS;
		}
		MachinePartChanged?.Invoke(context);
	}

	public void TryInsertPart(ChangeMachinePartContext context)
	{
		if (SlotIsPartCompatible(context.Slot, context.Part))
		{
			context.Slot.Part = context.Part;
		}
	}

	protected bool SlotIsPartCompatible(MachineSlot slot, MachinePart part)
	{
		bool allowedCategory = slot.CategoriesAllowed.Contains(part.Category);

		return allowedCategory;
	}

	protected void OnMachinePartChanged(ChangeMachinePartContext context)
	{
		string logMsg = context.Result switch
		{
			ChangeMachinePartContext.EResult.SUCCESS => $"Inserted {context.Part} into {context.Slot}",
			ChangeMachinePartContext.EResult.DOES_NOT_FIT => $"{context.Part} does not fit into {context.Slot}",
			ChangeMachinePartContext.EResult.UNDEFINED => $"Failed to insert {context.Part} into {context.Slot} for some reason",
			_ => throw new Exception(),
		};
		Log.Info(logMsg);
	}
}
