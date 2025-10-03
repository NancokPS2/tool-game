using Godot;
using System;

[GlobalClass, Tool]
public partial class PowerMeter3D : Node3D
{
	public static readonly Vector2 DEFAULT_SIZE = new(0.5f, 1.5f);

	[Export]
	protected MeshInstance3D? PowerIndicatorMeshNode
	{
		set
		{
			powerIndicatorMeshNode = value;
			Update();
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
			Update();
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
			Update();
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
			Update();
		}
		get => maxSize;
	}
	protected Vector2 maxSize = DEFAULT_SIZE;

	protected void Update()
	{
		if (PowerIndicatorMeshNode is null)
			return;

		PowerIndicatorMeshNode.Mesh = PowerIndicatorMesh;
		PowerIndicatorMesh.Size = new(MaxSize.X, MaxSize.Y * value);
		PowerIndicatorMeshNode.Mesh = PowerIndicatorMesh;
	}
}
