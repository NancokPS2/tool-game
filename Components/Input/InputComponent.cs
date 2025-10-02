using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Inputs;

[GlobalClass]
public partial class InputComponent : Node, IComponent
{
	public Dictionary<string, bool> InputsActive = new();

	public string[] GetProcessingGroups() => [CompGroups.INPUT];
}
