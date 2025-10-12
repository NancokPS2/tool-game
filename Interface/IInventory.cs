using System.Collections.Generic;

namespace ToolGame.Interface;

public interface IInventory
{
	public IPickup Selected { set; get; }
	public List<IPickup> Contents { set; get; }
}
