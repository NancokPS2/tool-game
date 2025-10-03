using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Singleton;

namespace ToolGame.Interface;

public interface IEntity
{
	public TComponent? GetComponent<TComponent>() where TComponent : IComponent
	{
		return ECSManager.GetComponent<TComponent>(GetEntityId());
	}

	public TComponent[] GetComponents<TComponent>() where TComponent : IComponent
	{
		return ECSManager.GetComponents<TComponent>(GetEntityId());
	}

	public ulong GetEntityId()
	{
		return (this as Node ?? throw new Exception()).GetInstanceId();
	}

	public void AddComponent(IComponent component)
	{
		ECSManager.AddComponent(GetEntityId(), component);
	}
}
