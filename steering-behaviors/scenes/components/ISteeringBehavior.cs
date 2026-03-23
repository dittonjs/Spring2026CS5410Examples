using Godot;
using System.Collections.Generic;

public interface ISteeringBehavior
{
    Vector2 CalculateSteeringForce(List<Fish> neighbors);
}
