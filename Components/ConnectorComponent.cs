using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using ToolGame.Entity;

namespace ToolGame.Components;

public partial class ConnectorComponent : Node, IComponent
{
	[Export]
	protected Entity3D? connectedEntity
	{
		set
		{
			ConnectedEntity = value;
		}
		get
		{
			return (Entity3D?)ConnectedEntity ?? null;
		}
	}
	public IEntity? ConnectedEntity;
}
