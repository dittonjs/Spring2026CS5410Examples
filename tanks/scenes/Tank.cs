using Godot;
using System;

public partial class Tank : CharacterBody2D
{
    [Export]
    public int Speed = 200;
    private Node2D turret;

    public override void _Ready()
    {
        turret = GetNode<Node2D>("Turret");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        velocity.X = Input.GetAxis("move_left", "move_right");
        velocity.Y = Input.GetAxis("move_up", "move_down");

        Velocity = velocity.Normalized() * Speed;

        // Rotate turret to face the mouse position

        if (Velocity.Length() > 0)
        {
            // lerp tank body rotation towards movement direction
            Rotation = Mathf.LerpAngle(Rotation, Velocity.Angle(), 0.1f);
        }
        Vector2 mousePosition = GetGlobalMousePosition();
        Vector2 directionToMouse = (mousePosition - GlobalPosition).Normalized();
        turret.Rotation = directionToMouse.Angle() - Rotation;

        MoveAndSlide();
    }
}
