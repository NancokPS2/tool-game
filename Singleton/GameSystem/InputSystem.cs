using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGame.Inputs;
using ToolGame.Singleton;

namespace ToolGame.Singleton.GameSystem;

[GlobalClass]
public partial class InputSystem : BaseSystem
{
	public override void _SystemProcess(double delta)
	{
		base._SystemProcess(delta);
		foreach (var entity in GetInGroup(CompGroups.INPUT))
		{
			var inputComp = entity.GetComponent<InputComponent>();
			if (inputComp is null) continue;

			foreach (var input in InputNames.INPUTS_ARRAY)
			{
				inputComp.InputsActive[input] = Input.IsActionPressed(input);

			}
		}
	}
}
