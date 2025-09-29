using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ToolGame.Components;
using System.Threading.Tasks;

namespace ToolGame.Entity;

public static class IEntityExtension
{
	public static TComponent? GetComponent<TComponent>(this IEntity entity) where TComponent : IComponent
	{
		return entity.GetComponents().OfType<TComponent>().Single();
	}

	public static IEnumerable<IComponent> GetComponents(this Entity3D entity)
	{
		return entity.GetChildren().OfType<IComponent>();
	}
	
}
