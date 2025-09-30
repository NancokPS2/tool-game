using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interaction;

[GlobalClass]
public partial class InteractionArea3D : Area3D, IInteractionTarget
{
	public ICreature? CurrentInteractor { get; set; }
}
