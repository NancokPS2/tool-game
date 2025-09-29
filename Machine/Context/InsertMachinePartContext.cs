namespace ToolGame.Machine.Context;

public class InsertMachinePartContext
{
    public Machine3D Machine;
    public MachineSlot Slot;
    public MachinePart3D Part;

    public InsertMachinePartContext(Machine3D machine, MachineSlot slot, MachinePart3D part)
    {
        Machine = machine;
        Slot = slot;
        Part = part;
    }
}
