using Godot;
using System;
using System.ComponentModel;

public partial class Player : CharacterBody3D
{
    [Export]
    public float Speed = 5.0f;
    [Export]
    public float JumpVelocity = 4.5f;
    [Export]
    public float Sensitivity = .5f;

    [Export]
    public Node3D RotationPoint;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion e)
        {
            RotateY(Mathf.DegToRad(-e.Relative.X * Sensitivity));
            RotationPoint.RotateX(Mathf.DegToRad(-e.Relative.Y * Sensitivity));
            RotationPoint.Rotation = new Vector3(
                Mathf.Clamp(RotationPoint.Rotation.X, Mathf.DegToRad(-90), Mathf.DegToRad(45)),
                RotationPoint.Rotation.Y,
                RotationPoint.Rotation.Z
            );
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        if (Input.IsActionJustPressed("exit"))
        {
            GetTree().Quit();
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;



        MoveAndSlide();
        var collision = GetLastSlideCollision();
        if (collision != null && collision.GetCollider() is RigidBody3D other)
        {
            other.ApplyCentralForce(-100 * collision.GetNormal());
        }
    }
}
