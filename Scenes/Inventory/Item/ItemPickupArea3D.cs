[GlobalClass]
public partial class ItemPickupArea3D : Area3D
{

	public IPickup Itemizer = null!;
	[Export]
	protected NodeItemizer3D? itemizer
	{
		set => Itemizer = value as IPickup ?? throw new Exception();
		get => Itemizer as NodeItemizer3D;
	}

	public IPickup GetItem()
		=> Itemizer;
}
