using Godot;
using System;

public partial class PlayerTankController : Node2D, ITankController
{

    public override void _Process(double delta)
    {
        Vector2 direction = new Vector2();
        direction.X = Input.GetAxis("move_left", "move_right");
        direction.Y = Input.GetAxis("move_up", "move_down");
        ChangeDirection?.Invoke(direction.Normalized());

        Vector2 mousePosition = GetGlobalMousePosition();
        ChangeTurretFocus?.Invoke(mousePosition);
        // tell the tank about its new direction

        if (Input.IsActionJustPressed("fire"))
        {
            FireProjectile?.Invoke();
        }
    }

    public event Action<Vector2> ChangeDirection;
    public event Action<Vector2> ChangeTurretFocus;

    public event Action FireProjectile;
}
