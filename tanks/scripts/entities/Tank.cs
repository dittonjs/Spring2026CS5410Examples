using Godot;
using System;

public partial class Tank : CharacterBody2D
{
    [Export]
    public int Speed = 200;
    private Node2D turret;
    private ProjectileSpawner projectileSpawner;
    private HealthComponent healthComponent;


    public override void _Ready()
    {
        turret = GetNode<Node2D>("Turret");
        projectileSpawner = GetNode<ProjectileSpawner>("Turret/ProjectileSpawner");
        healthComponent = GetNode<HealthComponent>("HealthComponent");
        healthComponent.Died += OnDied;
        if (HasNode("TankController"))
        {
            ITankController controller = GetNode<ITankController>("TankController");
            controller.ChangeDirection += OnChangeDirection;
            controller.ChangeTurretFocus += OnChangeTurretFocus;
            controller.FireProjectile += OnFireProjectile;
        }
    }

    public override void _PhysicsProcess(double delta)
    {

        // Rotate turret to face the mouse position

        if (Velocity.Length() > 0)
        {
            // lerp tank body rotation towards movement direction
            Rotation = Mathf.LerpAngle(Rotation, Velocity.Angle(), 0.1f);
        }


    }

    public void OnChangeDirection(Vector2 direction)
    {
        Velocity = direction * Speed;
        MoveAndSlide();
    }

    public void OnChangeTurretFocus(Vector2 focusPoint)
    {
        Vector2 directionToMouse = focusPoint - GlobalPosition;
        turret.Rotation = directionToMouse.Angle() - Rotation;
    }

    public void OnFireProjectile()
    {
        // Implement firing logic here
        projectileSpawner.SpawnProjectile(
            Vector2.Right.Rotated(turret.GlobalRotation)
        );
    }

    public void OnDied()
    {
        QueueFree();
    }
}
