using Godot;
using System;

public partial class ProjectileSpawner : Node2D
{
    [Export]
    public PackedScene ProjectileScene;

    public void SpawnProjectile(Vector2 direction)
    {
        Node node = ProjectileScene.Instantiate<Node>();
        if (node is IProjectile projectileInstance)
        {
            projectileInstance.Initialize(direction, GlobalPosition);
            GetTree().Root.AddChild(node);
        }
    }

}
