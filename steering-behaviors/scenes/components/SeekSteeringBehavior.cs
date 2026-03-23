using Godot;
using System;
using System.Collections.Generic;

public partial class SeekSteeringBehavior : Node2D, ISteeringBehavior
{
    [Export]
    public Vector2 TargetPosition = new Vector2(500, 400);

    public Vector2 CalculateSteeringForce(List<Fish> neighbors)
    {
      Fish fish = GetParent<Fish>();
      Vector2 desiredVelocity = (TargetPosition - fish.Position).Normalized() * fish.MaxSpeed;
      return desiredVelocity - fish.Velocity;
    }
}
