using System;

namespace ToolGame.Machinery;

public interface IPowerContainer
{
    public bool ProvidesPower { set; get; }
    public long StoredPower { set; get; }
    public long StoredPowerMax { set; get; }

    public long GetCapacityRemaining() => Math.Clamp(StoredPowerMax - StoredPower, 0, long.MaxValue);
}
