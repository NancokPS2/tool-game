using System;

namespace ToolGame.Machinery;

public interface IPowerContainer : IComponent
{
    public double PowerStored { set; get; }
    public double PowerStoredMax { set; get; }
	public double PowerDrain { set; get; }

    public double GetCapacityRemaining() => Mathf.Clamp(PowerStoredMax - PowerStored, 0, long.MaxValue);
}
