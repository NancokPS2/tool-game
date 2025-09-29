using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using ToolGame.Machine.Context;
using ToolGame.Machine.MachinePart;

namespace ToolGame.Machine;

public abstract partial class Machine3D : Node3D, IMachine
{

    #region Part Management
    protected List<MachineSlot> MachineSlots = new();

    public virtual MachinePart3D? GetPart<TPart>() where TPart : MachinePart3D
    {
        return GetParts().OfType<TPart>().Single();
    }

    public virtual List<MachinePart3D> GetParts()
    {
        return (from slot in MachineSlots select slot.Part).ToList();
    }

    public virtual List<TMachinePart> GetParts<TMachinePart>()
    {
        return (from slot in MachineSlots select slot.Part).OfType<TMachinePart>().ToList();
    }

    public virtual bool HasPart<TPart>() where TPart : MachinePart3D
    {
        return GetParts().OfType<TPart>().Count() > 0;
    }

    public virtual bool TryInsertPart(InsertMachinePartContext context)
    {
        if (context.Machine != this || !MachineSlots.Contains(context.Slot))
            throw new Exception();

        context.Slot.Part = context.Part;
        return true;
    }

    public virtual void ProcessParts(long delta)
    {
        foreach (MachinePart3D item in GetParts())
        {
            item.PartProcess(new (delta, this));
        }
    }
    #endregion

    #region Power
    public void PowerAdd(AddPowerMachineContext context)
    {
        foreach (var item in context.PowerContainers)
        {
            long powerAdded = Math.Clamp(context.Amount, 0, item.GetCapacityRemaining());
            item.StoredPower += powerAdded;
            context.Amount -= powerAdded;

            if(context.Amount <= 0)
                break;
        }
    }



    public long GetPowerRemaining()
    {
        long total = 0;
        foreach (var item in GetParts<IPowerContainer>())
        {
            total += item.StoredPower;
        }
        return total;
    }

    public long GetPowerRequired()
    {
        long total = 0;
        GetParts<IPowerConsumer>().ForEach(x => total += x.PowerConsumed);
        return total;
    }
    #endregion
}
