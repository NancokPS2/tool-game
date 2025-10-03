using System.Collections.Generic;

namespace ToolGame.Context;

public class ChangePowerMachineContext
{
    public ulong EntityId;
    public IPowerContainer[] PowerContainers;
    public double Amount;

    public ChangePowerMachineContext(ulong entityId, IPowerContainer[] powerContainers, double amount)
    {
		EntityId = entityId;
        PowerContainers = powerContainers;
        Amount = amount;
    }
}
