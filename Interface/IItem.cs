namespace ToolGame.Interface;

public interface IItem
{
	public PackedScene? ItemScene { set; get; }
	public Texture2D? Icon { set; get; }
}