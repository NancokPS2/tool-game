using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using ToolGame.Singleton;

namespace ToolGame.Machinery;

public abstract partial class MachinerySystem : BaseSystem
{

	protected Dictionary<Machine3D, List<MachinePart3D>> MachinePartsTracked = new();
}
