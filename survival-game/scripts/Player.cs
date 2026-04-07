using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
  [Export]
  public float Speed = 200.0f;

  [Export]

  float Gravity = 981f;

  private Sprite2D _sprite;

  public Dictionary<string, PackedScene> StatesScenes = new Dictionary<string, PackedScene>() {
    { "Grounded", GD.Load<PackedScene>("res://scenes/player_states/Grounded.tscn") },
    { "Falling", GD.Load<PackedScene>("res://scenes/player_states/Falling.tscn") },
    { "Jump", GD.Load<PackedScene>("res://scenes/player_states/Jump.tscn") },
    { "DoubleJump", GD.Load<PackedScene>("res://scenes/player_states/DoubleJump.tscn") },
  };
  private Node2D _currentState;

  public override void _Ready()
  {
    _sprite = GetNode<Sprite2D>("Sprite2D");
    ChangeState("Grounded");
  }

  public override void _Process(double delta)
  {
    if (Velocity.X > 0) {
      _sprite.FlipH = false;
    } else if (Velocity.X < 0) {
      _sprite.FlipH = true;
    }
  }

  public void ChangeState(string stateName)
  {
    if (_currentState != null) {
      _currentState.QueueFree();
    }
    _currentState = StatesScenes[stateName].Instantiate<Node2D>();
    AddChild(_currentState);
  }
}
