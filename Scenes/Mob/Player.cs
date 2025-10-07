using Godot;
using System;

[GlobalClass]
public partial class Player : CharacterBody3D, IEntity
{
	const float SPEED_MAX_DEFAULT = 5;

	[Export]
	public float Acceleration = SPEED_MAX_DEFAULT * 10;

	[Export]
	public float SpeedMax = SPEED_MAX_DEFAULT;

	[Export]
	public Godot.Collections.Array<Node3D> RotatedNodes = new();

	[Export]
	public float RotationSpeed = 0.5f;

	public Vector3 MovementInput;
	public Vector2 RotationInput;

	public Vector3 RotationToApply;

	public override void _Process(double delta)
	{
		base._Process(delta);
		RotationToApply = new Vector3(-(RotationInput.Y * (float)(delta * RotationSpeed)), -(RotationInput.X * (float)(delta * RotationSpeed)), 0);
		foreach (var item in RotatedNodes)
		{
			item.Rotation += RotationToApply;
			item.Orthonormalize();
		}
		RotationInput = Vector2.Zero;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		//Read input
		Vector2 inputVector = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
		MovementInput = new(inputVector.X, 0, inputVector.Y);

		//Apply input to velocity
		Velocity += MovementInput * (float)(Acceleration * delta);


		//Cap speed
		Velocity = Velocity.LimitLength(SpeedMax);

		//Decceleration when input is let go.
		if (MovementInput == Vector3.Zero)
		{
			Velocity = Velocity.MoveToward(Vector3.Zero, (float)(Acceleration * delta));
		}

		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseMotion motion)
		{
			RotationInput += motion.Relative;
		}
	}

}
