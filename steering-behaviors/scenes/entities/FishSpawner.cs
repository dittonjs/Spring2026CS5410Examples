using Godot;
using System;

public partial class FishSpawner : Node2D
{
    PackedScene fishScene = GD.Load<PackedScene>("res://scenes/entities/Fish.tscn");

    [Export]
    public int FishCount = 40;

    [Export]
    public float FishSpeed = 400;

    public override void _Ready()
    {
        for (int i = 0; i < FishCount; i++)
        {
            var fish = fishScene.Instantiate<Fish>();
            fish.Position = new Vector2((float)GD.RandRange(0, GetViewportRect().Size.X), (float)GD.RandRange(0, GetViewportRect().Size.Y));
            fish.Velocity = new Vector2(0,1).Rotated((float)GD.RandRange(0,Mathf.Tau)) * fish.MaxSpeed;
            // fish.Velocity = new Vector2(200, 0);
            GetParent<Node2D>().CallDeferred("add_child", fish);
        }
    }

}
