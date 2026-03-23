using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public partial class Fish : Node2D
{

    [Export]
    public float MaxSpeed = 400;

    [Export]
    public float MaxForce = 40;

    [Export]
    public Vector2 Velocity = new Vector2(400, 0);

    List<Fish> neighbors = new List<Fish>();
    float maxAngularVelocity = 1;

    SeekSteeringBehavior seekSteeringBehavior;
    SeparationSteeringBehavior separationSteeringBehavior;
    ArrivalSteeringBehavior arrivalSteeringBehavior;
    CohesionSteeringBehavior cohesionSteeringBehavior;
    AlignmentSteeringBehavior alignmentSteeringBehavior;

    CollisionAvoidanceSteeringBehavior collisionAvoidanceSteeringBehavior;
    Area2D area2D;

    public override void _Ready()
    {
        seekSteeringBehavior = GetNode<SeekSteeringBehavior>("SeekSteeringBehavior");
        separationSteeringBehavior = GetNode<SeparationSteeringBehavior>("SeparationSteeringBehavior");
        arrivalSteeringBehavior = GetNode<ArrivalSteeringBehavior>("ArrivalSteeringBehavior");
        cohesionSteeringBehavior = GetNode<CohesionSteeringBehavior>("CohesionSteeringBehavior");
        alignmentSteeringBehavior = GetNode<AlignmentSteeringBehavior>("AlignmentSteeringBehavior");
        collisionAvoidanceSteeringBehavior = GetNode<CollisionAvoidanceSteeringBehavior>("CollisionAvoidanceSteeringBehavior");
        area2D = GetNode<Area2D>("DetectionArea");
        area2D.AreaEntered += OnAreaEntered;
        area2D.AreaExited += OnAreaExited;
    }

    private void OnAreaEntered(Area2D area2D)
    {
        if (area2D.GetParent() is Fish fish && fish != this)
        {
            neighbors.Add(fish);
        }
    }

    private void OnAreaExited(Area2D area2D)
    {
        if (area2D.GetParent() is Fish fish)
        {
            neighbors.Remove(fish);
        }
    }


    public override void _PhysicsProcess(double delta)
    {
        // if (Position.X > GetViewportRect().Size.X) Position = new Vector2(0, Position.Y);
        // if (Position.X < 0) Position = new Vector2(GetViewportRect().Size.X, Position.Y);
        // if (Position.Y > GetViewportRect().Size.Y) Position = new Vector2(Position.X, 0);
        // if (Position.Y < 0) Position = new Vector2(Position.X, GetViewportRect().Size.Y);

        seekSteeringBehavior.TargetPosition = GetGlobalMousePosition();
        arrivalSteeringBehavior.TargetPosition = GetGlobalMousePosition();
        Vector2 steeringForce = Vector2.Zero;

        // steeringForce += seekSteeringBehavior.CalculateSteeringForce(neighbors);
        steeringForce += separationSteeringBehavior.CalculateSteeringForce(neighbors);
        steeringForce += alignmentSteeringBehavior.CalculateSteeringForce(neighbors);
        // steeringForce += arrivalSteeringBehavior.CalculateSteeringForce(neighbors);
        steeringForce += cohesionSteeringBehavior.CalculateSteeringForce(neighbors);
        steeringForce += collisionAvoidanceSteeringBehavior.CalculateSteeringForce(neighbors);
        Velocity = Velocity.Lerp(Velocity+steeringForce, .1f);

        Velocity = Velocity.LimitLength(MaxSpeed);
        LookAt(Position + Velocity.Normalized());

        Position += Velocity * (float) delta;

    }
    private Vector2 truncate(Vector2 vector, float max)
    {
        if (vector.Length() > max)
        {
            return vector.Normalized() * max;
        }
        return vector;
    }


}
