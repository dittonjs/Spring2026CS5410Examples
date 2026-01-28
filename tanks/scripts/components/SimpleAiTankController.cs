using Godot;
using System;

public partial class SimpleAiTankController : Node2D, ITankController
{

    private enum State
    {
        Patrol,
        Chase,
        Attack
    }

    private State currentState = State.Patrol;

    private bool canFire = true;
    private Tank playerTank;

    private Timer fireCooldownTimer;

    private int patrolIndex = 0;


    public override void _Ready()
    {
        playerTank = GetTree().GetNodesInGroup("PlayerTank")[0] as Tank;

        Timer patrolTimer = new Timer();

        patrolTimer.WaitTime = 2.0f; // Change direction every 2 seconds
        patrolTimer.Timeout += () =>
        {
            patrolIndex = (patrolIndex + 1) % 4;
        };
        AddChild(patrolTimer);
        patrolTimer.Start();
    }

    public override void _Process(double delta)
    {
        if (playerTank == null) return;


        if (currentState == State.Chase)
        {
            HandleChaseState();
        }
        else if (currentState == State.Patrol)
        {
            HandlePatrolState(delta);
        }
        else if (currentState == State.Attack)
        {
            // Implement attack behavior if needed
            HandleAttackState();
        }
    }

    private void HandleChaseState()
    {
        Vector2 directionToPlayer = (playerTank.GlobalPosition - GlobalPosition).Normalized();
        ChangeDirection?.Invoke(directionToPlayer);

        ChangeTurretFocus?.Invoke(playerTank.GlobalPosition);
        float distanceToPlayer = GlobalPosition.DistanceTo(playerTank.GlobalPosition);
        if (distanceToPlayer < 800)
        {
            currentState = State.Attack;
        }
        else if (distanceToPlayer > 2000)
        {
            currentState = State.Patrol;
        }
    }

    private void HandlePatrolState(double delta)
    {
        // Simple patrol logic: move in a square pattern
        Vector2[] patrolDirections = new Vector2[]
        {
            Vector2.Right,
            Vector2.Down,
            Vector2.Left,
            Vector2.Up
        };


        ChangeDirection?.Invoke(patrolDirections[patrolIndex]);
        if (GlobalPosition.DistanceTo(playerTank.GlobalPosition) < 1500)
        {
            currentState = State.Chase;
        }
    }

    private void HandleAttackState()
    {
        Vector2 directionToPlayer = (playerTank.GlobalPosition - GlobalPosition).Normalized();
        ChangeDirection?.Invoke(Vector2.Zero); // Stop moving

        ChangeTurretFocus?.Invoke(playerTank.GlobalPosition);

        if (canFire)
        {
            FireProjectile?.Invoke();
            canFire = false;
            // Start cooldown timer
            fireCooldownTimer = new Timer();
            fireCooldownTimer.WaitTime = 1.5f; // 1.5 seconds cooldown
            fireCooldownTimer.OneShot = true;
            fireCooldownTimer.Timeout += () =>
            {
                canFire = true;
                fireCooldownTimer.QueueFree();
            };
            AddChild(fireCooldownTimer);
            fireCooldownTimer.Start();
        }

        float distanceToPlayer = GlobalPosition.DistanceTo(playerTank.GlobalPosition);
        if (distanceToPlayer > 1000)
        {
            currentState = State.Chase;
        }
    }



    public event Action<Vector2> ChangeDirection;
    public event Action<Vector2> ChangeTurretFocus;
    public event Action FireProjectile;
}
