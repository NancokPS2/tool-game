namespace ToolGame.Context;

public class ProcessMachinePartContext
{
    public double Delta;
    public Machine3D Machine;

    public ProcessMachinePartContext(double delta, Machine3D machine)
    {
        Delta = delta;
        Machine = machine;
    }
}
