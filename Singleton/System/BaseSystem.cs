using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Singleton.System;

[GlobalClass]
public abstract partial class BaseSystem : Node
{
	public enum EProcessMode { SYSTEM, PROCESS, PHYSICS }

	[Export]
	public EProcessMode SyncToProcess
	{
		get
		{
			return SyncToProcess;
		}
		set
		{
			SyncToProcess = value;
			SetProcess(SyncToProcess == EProcessMode.PROCESS);
			SetPhysicsProcess(SyncToProcess == EProcessMode.PHYSICS);
			SystemProcessEnabled = SyncToProcess == EProcessMode.SYSTEM;
		}
	}
	private bool SystemProcessEnabled;

	public void SystemProcess(long delta)
	{
		if (!SystemProcessEnabled) return;
		_SystemProcess(delta);
	}

	public virtual void _SystemProcess(long delta)
	{
	}
}
