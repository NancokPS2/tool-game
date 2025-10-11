
[GlobalClass]
public partial class PowerMeter3D : Node3D
{
	public static readonly Vector2 DEFAULT_SIZE = new(0.5f, 1.5f);

	[Export]
	protected MeshInstance3D? PowerIndicatorMeshNode
	{
		set
		{
			powerIndicatorMeshNode = value;
			UpdateMesh();
		}
		get => powerIndicatorMeshNode;
	}
	protected MeshInstance3D? powerIndicatorMeshNode;

	[Export]
	protected PlaneMesh PowerIndicatorMesh
	{
		set
		{
			powerIndicatorMesh = value;
			UpdateMesh();
		}
		get => powerIndicatorMesh;
	}
	protected PlaneMesh powerIndicatorMesh = new() { Size = DEFAULT_SIZE };

	[Export(PropertyHint.Range, "0.0,1.0")]
	public float Value
	{
		set
		{
			this.value = value;
			UpdateMesh();
		}
		get => value;
	}
	protected float value = 1;

	[Export]
	public Vector2 MaxSize
	{
		set
		{
			maxSize = value;
			UpdateMesh();
		}
		get => maxSize;
	}
	protected Vector2 maxSize = DEFAULT_SIZE;

	[Export]
	public Godot.Collections.Array<PowerContainer3D> PowerContainers = new();

	protected void UpdateMesh()
	{
		if (PowerIndicatorMeshNode is null)
			return;

		PowerIndicatorMeshNode.Mesh = PowerIndicatorMesh;
		PowerIndicatorMesh.Size = new(MaxSize.X, MaxSize.Y * value);
		PowerIndicatorMeshNode.Mesh = PowerIndicatorMesh;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		double stored = 0;
		double max = 0;
		foreach (var item in PowerContainers)
		{
			stored += item.Stored;
			max += item.Max;
		}
		Value = (float)(stored / max);
	}

}
