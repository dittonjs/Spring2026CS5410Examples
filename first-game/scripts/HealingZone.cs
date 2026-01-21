using Godot;
using System;

public partial class HealingZone : Area2D
{
    public override void _Ready()
    {
        this.AreaEntered += OnAreaEntered;
        this.AreaExited += OnAreaExited;
    }

    private void OnAreaEntered(Node2D body)
    {
        GD.Print("A body entered the healing zone: " + body.Name);
    }

    private void OnAreaExited(Node2D body)
    {
        GD.Print("A body exited the healing zone: " + body.Name);
    }

}
