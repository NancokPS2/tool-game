using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Entity;

public interface IEntity
{
	public IEnumerable<IComponent> GetComponents();
	
}
