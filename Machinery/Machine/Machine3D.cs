using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGame.Machinery;

public abstract partial class Machine3D : Node3D
{
	public delegate void PartChangeEvent(Machine3D machine, MachinePart3D part);
	public event PartChangeEvent? PartAdded;
	public event PartChangeEvent? PartRemoved;

	public override void _Ready()
	{
		base._Ready();
		AddToGroup(NodeGroups.MACHINE);
	}

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
