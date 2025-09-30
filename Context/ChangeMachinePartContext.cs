namespace ToolGame.Context;

public class ChangeMachinePartContext
{
	public enum EPartChange {INSERTED, REMOVED}
    public Machine3D Machine;
    public MachineSlot Slot;
    public MachinePart3D Part;
	public EPartChange Change;
	public string Result;

	public ChangeMachinePartContext(Machine3D machine, MachineSlot slot, MachinePart3D part, EPartChange change)
	{
		Machine = machine;
		Slot = slot;
		Part = part;
		Change = change;
	}
}
