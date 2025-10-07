namespace ToolGame.Interface;

public interface IItem
{
	public string ItemName { set; get; }
	public PackedScene? ItemScene { set; get; }
	public Texture2D? Icon { set; get; }

	public IItem Pickup();
	public void Place(Vector3 where);

}