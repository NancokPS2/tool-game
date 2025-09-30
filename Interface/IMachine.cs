using System.Collections.Generic;

namespace ToolGame.Interface;

public interface IMachine
{
    public bool HasPart<TPart>() where TPart : MachinePart3D;
    public MachinePart3D? GetPart<TPart>() where TPart : MachinePart3D;
    public List<MachinePart3D> GetParts();
    public bool TryInsertPart(ChangeMachinePartContext context);
}
