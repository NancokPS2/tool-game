namespace ToolGame.Creature.Living;

public partial class Human : Living, ICreature, ILiving
{
    public float Speed;
    public Vector3 AccelerationDir;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Velocity += AccelerationDir * Speed;
        MoveAndSlide();
    }
}
