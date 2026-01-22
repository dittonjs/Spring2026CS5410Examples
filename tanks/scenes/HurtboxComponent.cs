using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
    [Export]
    public int DamageAmount = 10;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void OnAreaEntered(Node2D body)
    {
        if (body is HitboxComponent hitbox)
        {
            // we hit something
            HurtboxHit?.Invoke(hitbox);
        }
    }

    public event Action<HitboxComponent> HurtboxHit;


}
