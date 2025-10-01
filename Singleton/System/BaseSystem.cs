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
			return syncToProcess;
		}
		set
		{
			syncToProcess = value;
			SetProcess(syncToProcess == EProcessMode.PROCESS);
			SetPhysicsProcess(syncToProcess == EProcessMode.PHYSICS);
			SystemProcessEnabled = syncToProcess == EProcessMode.SYSTEM;
		}
	}
	protected EProcessMode syncToProcess;
	private bool SystemProcessEnabled;

	public void SystemProcess(double delta)
	{
		if (!SystemProcessEnabled) return;
		_SystemProcess(delta);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		_SystemProcess(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		_SystemProcess(delta);
	}

	public virtual void _SystemProcess(double delta)
	{
	}
}
