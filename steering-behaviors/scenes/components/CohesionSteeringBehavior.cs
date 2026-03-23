using Godot;
using System.Collections.Generic;

public partial class CohesionSteeringBehavior : Node2D, ISteeringBehavior
{
    public Vector2 CalculateSteeringForce(List<Fish> neighbors)
    {

        if (neighbors.Count == 0) return Vector2.Zero;
        Fish fish = GetParent<Fish>();
        Vector2 steeringForce;
        Vector2 averagePosition = Vector2.Zero;
        foreach (Fish neighbor in neighbors)
        {
            averagePosition += neighbor.Position;
        }
        averagePosition /= neighbors.Count;
        steeringForce = averagePosition - fish.Position;
        return steeringForce * 10;
    }
}
