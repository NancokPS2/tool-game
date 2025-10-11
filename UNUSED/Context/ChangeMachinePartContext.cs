using System.Reflection.PortableExecutable;

namespace ToolGame.Context;

public class ChangeMachinePartContext
{
	public enum EPartChange { INSERTED, REMOVED }
	public enum EResult { UNDEFINED, SUCCESS, DOES_NOT_FIT }
	public ulong MachineEntityId;
	public MachineSlot Slot;
	public MachinePart Part;
	public EPartChange Change;
	public EResult Result;

	public ChangeMachinePartContext(ulong machineEntityId, MachineSlot slot, MachinePart part, EPartChange change)
	{
		MachineEntityId = machineEntityId;
		Slot = slot;
		Part = part;
		Change = change;
	}
}
