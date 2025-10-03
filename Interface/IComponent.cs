using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Singleton;

namespace ToolGame.Interface;

public interface IComponent
{
	public string[] GetProcessingGroups();

	public ulong GetComponentId()
	{
		return GetEntityInstanceNode().GetEntityId();
	}

	public Node GetEntityInstanceNode()
	{
		if (this is Node node)
		{
			if (node.GetParent() is not IEntity)
				throw new Exception($"The parent at path {node.GetParent().GetPath()} should be an IEntity");

			return node.GetParent();
		}
		else
			throw new NotImplementedException();
	}
}
public static class IComponentExtension
{
	public static TComponent? GetComponent<TComponent>(this IEntity entity) where TComponent : IComponent
	{
		return ECSManager.GetComponent<TComponent>((entity as Node ?? throw new Exception($"This entity is not a Node")).GetInstanceId());
	}

	public static ulong GetEntityId(this Node node)
	{
		return (node as IEntity ?? throw new Exception()).GetEntityId();
	}

	public static ulong GetComponentId(this Node node)
	{
		return (node as IComponent ?? throw new Exception()).GetComponentId();
	}
}
