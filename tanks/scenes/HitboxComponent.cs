using Godot;
using System;

public partial class HitboxComponent : Area2D
{
    [Export]
    public HealthComponent healthComponent;
    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void OnAreaEntered(Node2D body)
    {
        if (body is HurtboxComponent hurtbox)
        {
            healthComponent?.TakeDamage(hurtbox.DamageAmount);
            HitboxHit?.Invoke(hurtbox);
        }
    }

    public event Action<HurtboxComponent> HitboxHit;
}
