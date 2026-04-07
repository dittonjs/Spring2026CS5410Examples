using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export]
    public float Speed = 200.0f;

    private Sprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _Process(double delta)
    {
        float moveX = Input.GetAxis("ui_left", "ui_right");
        float moveY = Input.GetAxis("ui_up",  "ui_down");
        Velocity = new Vector2(moveX, moveY).Normalized() * Speed;
        if (moveX != 0)
        {
            _sprite.FlipH = moveX < 0;
        }
        MoveAndSlide();
    }
}
