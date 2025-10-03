using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ToolGame.Machinery;

public abstract partial class Machine3D : Node3D, IEntity
{
	protected List<MachineSlot> MachineSlots = new();
	[Export]
	protected Godot.Collections.Array<MachineSlot> machineSlots
	{
		set => MachineSlots = new(value);
		get => new(MachineSlots);
	}

	#region Part Management
    public virtual MachinePart? GetPart<TPart>() where TPart : MachinePart
	{
		return GetParts().OfType<TPart>().Single();
	}

    public virtual List<MachinePart> GetParts()
    {
        return (from slot in MachineSlots select slot.Part).ToList();
    }

    public virtual List<TMachinePart> GetParts<TMachinePart>()
    {
        return (from slot in MachineSlots select slot.Part).OfType<TMachinePart>().ToList();
    }

    public virtual bool HasPart<TPart>() where TPart : MachinePart
    {
        return GetParts().OfType<TPart>().Count() > 0;
    }
    #endregion

    #region Power
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
