using Godot;
using System;
using System.Collections.Generic;
public partial class AlignmentSteeringBehavior : Node2D, ISteeringBehavior
{
    public Vector2 CalculateSteeringForce(List<Fish> neighbors)
    {
        if (neighbors.Count == 0) return Vector2.Zero;
        Fish fish = GetParent<Fish>();
        Vector2 desiredDirection = Vector2.Zero;
        foreach(Fish neighbor in neighbors) {
            desiredDirection += neighbor.Velocity.Normalized();
        }
        desiredDirection /= neighbors.Count;


        return (desiredDirection * fish.MaxSpeed) - fish.Velocity;
    }
}
