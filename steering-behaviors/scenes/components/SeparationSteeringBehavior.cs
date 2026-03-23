using Godot;
using System;
using System.Collections.Generic;

public partial class SeparationSteeringBehavior : Node2D, ISteeringBehavior
{

    public Vector2 CalculateSteeringForce(List<Fish> neighbors)
    {
        Fish fish = GetParent<Fish>();
        Vector2 steeringForce = Vector2.Zero;
        foreach (Fish neighbor in neighbors) {
            Vector2 offset = (fish.Position - neighbor.Position).Normalized();
            if (offset.Length() == 0) continue;
            steeringForce += offset * 200;
        }
        return steeringForce;
    }
}
