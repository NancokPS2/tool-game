using System.Collections.Generic;
using ToolGame.Machine.Context;

namespace ToolGame.Machine;

public interface IMachine
{
    public bool HasPart<TPart>() where TPart : MachinePart3D;
    public MachinePart3D? GetPart<TPart>() where TPart : MachinePart3D;
    public List<MachinePart3D> GetParts();
    public bool TryInsertPart(InsertMachinePartContext context);
}
