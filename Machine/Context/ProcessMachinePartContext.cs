namespace ToolGame.Machine.Context;

public class ProcessMachinePartContext
{
    public long Delta;
    public Machine3D Machine;

    public ProcessMachinePartContext(long delta, Machine3D machine)
    {
        Delta = delta;
        Machine = machine;
    }
}
