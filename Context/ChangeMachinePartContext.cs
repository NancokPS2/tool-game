namespace ToolGame.Context;

public class ChangeMachinePartContext
{
	public enum EPartChange {INSERTED, REMOVED}
    public Machine3D? Machine;
    public MachineSlot3D Slot;
    public MachinePart3D Part;
	public EPartChange Change;

	public ChangeMachinePartContext(MachineSlot3D slot, MachinePart3D part, EPartChange change)
	{
		Slot = slot;
		Part = part;
		Change = change;
	}
}
