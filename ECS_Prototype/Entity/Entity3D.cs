using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Godot;

namespace ToolGame.Entity;

[GlobalClass]
public partial class Entity3D : Node3D, IEntity
{
	public IEnumerable<IComponent> GetComponents()
	{
		return IEntityExtension.GetComponents(this);
	}
}
