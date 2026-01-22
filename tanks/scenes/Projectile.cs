using Godot;

public partial class Projectile : RigidBody2D, IProjectile
{
    [Export]
    public int Speed = 1000;
    private Vector2 direction;
    HurtboxComponent hurtbox;
    public void Initialize(Vector2 direction, Vector2 position)
    {
        hurtbox = GetNode<HurtboxComponent>("HurtboxComponent");
        hurtbox.HurtboxHit += OnHurtboxHit;
        Position = position;
        this.direction = direction.Normalized();
        ApplyCentralImpulse(direction * Speed);
        GravityScale = 0;
        Timer lifeTimer = new Timer();
        lifeTimer.WaitTime = 3.0f; // Lifetime of 3 seconds
        lifeTimer.OneShot = true;
        lifeTimer.Timeout += () =>
        {
            QueueFree();
            GD.Print("Projectile expired");
        };
        AddChild(lifeTimer);
        lifeTimer.Autostart = true;
    }

    public void OnHurtboxHit(HitboxComponent hitbox)
    {
        QueueFree();
        GD.Print("Projectile hit something");
    }
}
