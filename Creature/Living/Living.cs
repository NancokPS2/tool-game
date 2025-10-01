namespace ToolGame.Creature.Living;

public partial class Living : CharacterBody3D, ICreature, ILiving
{
    public float HealthRatio { get; set; }
}
