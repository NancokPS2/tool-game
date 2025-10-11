using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arch.Core;
using ToolGame.ECS;

namespace ToolGame.Singleton.GameSystem;

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

	#region Arch
	public abstract QueryDescription QueryDefault { set; get; }

	public static Arch.Core.World GetWorld() => ECSManager.World;

	public abstract void ProcessEntity(Entity entity, double delta);

	#endregion

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
			GetWorld().Query(QueryDefault, (Entity entity) =>
			{
				ProcessEntity(entity, delta);
			}
		);
	}

}
