using System.Collections.Generic;
using ToolGame.Machine.MachinePart;

namespace ToolGame.Machine.Context;

public class AddPowerMachineContext
{
    public Machine3D Machine;
    public List<IPowerContainer> PowerContainers;
    public long Amount;
    public long Remainder;

    public AddPowerMachineContext(Machine3D machine, List<IPowerContainer> powerContainers, long amount)
    {
        Machine = machine;
        PowerContainers = powerContainers;
        Amount = amount;
    }
}
