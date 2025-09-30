using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Entity;

namespace ToolGame.Context;

public class UseContext
{
	public ICreature Responsible;
	public IInventory Hand;
	public IInteractionTarget Target;

	public UseContext(ICreature responsible, IInventory hand, IInteractionTarget target)
	{
		Responsible = responsible;
		Hand = hand;
		Target = target;
	}
}
