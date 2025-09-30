using System.Collections.Generic;

namespace ToolGame.Context;

public class ChangePowerMachineContext
{
    public Machine3D Machine;
    public List<IPowerContainer> PowerContainers;
    public long Amount;

    public ChangePowerMachineContext(Machine3D machine, List<IPowerContainer> powerContainers, long amount)
    {
        Machine = machine;
        PowerContainers = powerContainers;
        Amount = amount;
    }
}
