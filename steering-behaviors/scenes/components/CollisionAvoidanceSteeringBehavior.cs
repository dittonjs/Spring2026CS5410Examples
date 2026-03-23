using Godot;
using System;
using System.Collections.Generic;

public partial class CollisionAvoidanceSteeringBehavior : Node2D, ISteeringBehavior
{
  [Export]
  public RayCast2D rayCast2D;

  public Vector2 CalculateSteeringForce(List<Fish> neighbors) {



    Fish fish = GetParent<Fish>();
    if (rayCast2D.IsColliding()) {
      GD.Print("colliding");
      return rayCast2D.GetCollisionNormal() * 500;
    } else {
      Vector2 desiredVelocity = fish.Velocity.Normalized() * fish.MaxSpeed;
      return desiredVelocity - fish.Velocity;
    }
  }
}
