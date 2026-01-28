using Godot;
using System;

public partial interface ITankController
{
    event Action<Vector2> ChangeDirection;
    event Action<Vector2> ChangeTurretFocus;
    event Action FireProjectile;
}
