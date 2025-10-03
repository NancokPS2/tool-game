using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interface;

public interface IInteractionSource : IComponent
{
	public IEntity? Responsible { set; get; }
	public IInteractionTarget? CurrentDetected { set; get; }
	public bool InteractionActive { set; get; }
}
