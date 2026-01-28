using Godot;
using System;

[Tool]
public partial class HealthComponent : Node2D
{
    [Export]
    public int MaxHealth = 100;

    public int currentHealth { get; private set; }

    public override void _Ready()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        HealthChanged?.Invoke(currentHealth, MaxHealth);
        GD.Print("Ouch!");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Died?.Invoke();
            GD.Print("Entity died");
        }
    }

    public event Action<int, int> HealthChanged;

    public event Action Died;
}
