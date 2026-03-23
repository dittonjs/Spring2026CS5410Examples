using Godot;
using System;
using System.Collections.Generic;

public partial class ArrivalSteeringBehavior : Node2D, ISteeringBehavior
{
  [Export]
  public Vector2 TargetPosition = new Vector2(500, 400);

  [Export]
  public float slowingDistance = 200;

  public Vector2 CalculateSteeringForce(List<Fish> neighbors)
  {
    Fish fish = GetParent<Fish>();
    Vector2 targetOffset = TargetPosition - fish.Position;
    float distance = targetOffset.Length();
    float clippedSpeed = Math.Min(fish.MaxSpeed, fish.MaxSpeed * distance / slowingDistance);
    Vector2 desiredVelocity = clippedSpeed / distance * targetOffset;
    return desiredVelocity - fish.Velocity;
  }
}
