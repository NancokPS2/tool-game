using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using ToolGame.Components;
using ToolGame.Entity;

namespace ToolGame.System;

public partial class ConnectionSystem : Node
{
	public void Connect(IEntity connector, IEntity target)
	{
		var source = connector.GetComponent<ConnectorComponent>();
		var receiver = target.GetComponent<ConnectorComponent>();
		source.ConnectedEntity = target;
	}
}
