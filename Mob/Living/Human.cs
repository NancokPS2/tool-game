
namespace ToolGame.Creature.Living;

public partial class Human : Living, IMob, ILiving, IEntity
{
    public float Speed;
    public Vector3 AccelerationDir;

	public override void _Ready()
	{
		base._Ready();

	}

	public void Move(MobMovementContext context)
	{
		Velocity = context.Velocity;
		MoveAndSlide();
	}
}
