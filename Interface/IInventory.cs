using System.Collections.Generic;

namespace ToolGame.Interface;

public interface IInventory
{
	public IItem Selected { set; get; }
	public List<IItem> Contents { set; get; }
}
