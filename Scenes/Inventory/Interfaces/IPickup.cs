namespace ToolGame.Interface;

public interface IPickup
{
	public IPickup Pickup();
	public void Place(ItemPlacementContext context);

}