using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame.Interface;

public interface IPowerGenerator : IComponent
{
    public double PowerProvided { set; get; }
}
