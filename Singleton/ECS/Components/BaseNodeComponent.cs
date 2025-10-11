using System.ComponentModel;
using Arch.Core;

namespace ToolGame.ECS;

public partial class BaseNodeComponent : Node, IComponent
{
	public void RegisterToEntity(Entity entity)
	{
		entity.Add(this, this);
	}
}
