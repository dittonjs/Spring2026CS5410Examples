using Godot;

public partial class Projectile : Node2D, IProjectile
{
    [Export]
    public int Speed = 2000;
    private Vector2 direction;
    private HurtboxComponent hurtbox;

    private GpuParticles2D explosionEffect;
    private Timer lifeTimer = new Timer();
    private Sprite2D sprite;
    public void Initialize(Vector2 direction, Vector2 position)
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        hurtbox = GetNode<HurtboxComponent>("HurtboxComponent");
        hurtbox.HurtboxHit += OnHurtboxHit;
        explosionEffect = GetNode<GpuParticles2D>("Explosion");
        Position = position;
        this.direction = direction.Normalized();
        // ApplyCentralImpulse(direction * Speed);
        // GravityScale = 0;
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

    public override void _Process(double delta)
    {
        Position += direction * Speed * (float)delta;
    }

    public void OnHurtboxHit(HitboxComponent hitbox)
    {
        sprite.Visible = false;
        hurtbox.SetDeferred("Monitoring", false);
        explosionEffect.Emitting = true;
        lifeTimer.Stop();
        lifeTimer.WaitTime = explosionEffect.Lifetime;
        lifeTimer.Start();
        GD.Print("Projectile hit something");
    }
}
