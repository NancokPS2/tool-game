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

	public Node GetEntityInstance()
	{
		if (this is Node node)
		{
			if (node.GetParent() is IComponent || node.GetParent() is not IEntity)
				throw new Exception();

			//Debug.Assert(node.GetParent() is not IComponent && node.GetParent() is IEntity);
			return node.GetParent();
		}
		else
			throw new NotImplementedException();
	}
}
public static class IComponentExtension
{
	public static TComponent? GetComponent<TComponent>(this Node node) where TComponent : IComponent
	{
		return ECSManagementSystem.GetComponent<TComponent>(node.GetInstanceId());
	}

	public static ulong GetEntityId(this Node node)
	{
		return node.GetInstanceId();
	}

	public static ulong GetEntityId(this IComponent comp)
	{
		return comp.GetEntityInstance().GetEntityId();
	}
}
