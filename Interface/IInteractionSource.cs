using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interface;

public interface IInteractionSource
{
	public ICreature? Responsible { set; get; }
	public IInteractionTarget? TargetCurrent { set; get; }
	public bool InteractionActive { set; get; }
}
