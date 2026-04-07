using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    private IEnemyState _currentState;


    public override void _Ready()
    {
        ChangeState(new WanderState());
    }

    public override void _Process(double delta)
    {
        _currentState.Process(delta);
    }

    public void ChangeState(IEnemyState state)
    {
        Character character = GetTree().GetFirstNodeInGroup("Player") as Character;
        if (_currentState != null) _currentState.Exit();
        _currentState = state;
        _currentState.Init(this, character);
        _currentState.Enter();
    }
}
