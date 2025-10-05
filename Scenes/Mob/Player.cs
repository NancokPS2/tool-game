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
	public Godot.Collections.Array<Vector3> RotatedNodes = new();


	public Vector3 Rotation;
	public Vector3 MovementInput;


	public override void _Process(double delta)
	{
		base._Process(delta);
		RotatedNodes.For
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
}
