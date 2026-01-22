using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export]
    public int MaxHealth = 100;

    private int currentHealth;

    public override void _Ready()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        GD.Print("Ouch!");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Died?.Invoke();
            GD.Print("Entity died");
        }
    }

    public event Action Died;
}
