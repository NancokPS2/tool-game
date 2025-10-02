using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interface;

public interface IEntity
{
	public TComponent? GetComponent<TComponent>() where TComponent : IComponent
	{
		if (this is Node node)
		{
			return node.GetComponent<TComponent>();
		}
		else
			throw new NotImplementedException();
	}
}
