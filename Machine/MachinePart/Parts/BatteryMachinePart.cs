using ToolGame.Machine.Context;

namespace ToolGame.Machine.MachinePart;

public class BatteryMachinePart : MachinePart3D, IPowerContainer
{
    public bool ProvidesPower { get; set; }
    public long StoredPower { get; set; }
    public long StoredPowerMax { get; set; }
    public bool EndlessPower;

    public override void PartProcess(ProcessMachinePartContext context)
    {
        context.Machine.PowerAdd( new AddPowerMachineContext(context.Machine, new(){this}, StoredPower) );
    }
}
