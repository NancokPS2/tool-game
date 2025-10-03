using System.Collections.Generic;

namespace ToolGame.Interface;

public interface IMachine
{
    public bool HasPart<TPart>() where TPart : MachinePart;
    public MachinePart? GetPart<TPart>() where TPart : MachinePart;
    public List<MachinePart> GetParts();
    public bool TryInsertPart(ChangeMachinePartContext context);
}
