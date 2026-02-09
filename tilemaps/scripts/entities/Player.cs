using Godot;
using System;

public partial class Player : CharacterBody2D
{

    private AnimatedSprite2D sprite;
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    public override void _Process(double delta)
    {
        float xAxis = Input.GetAxis("ui_left", "ui_right");
        float yAxis = Input.GetAxis("ui_up", "ui_down");

        Velocity = new Vector2(xAxis, yAxis).Normalized() * 300;
        if (Velocity.Length() > 0)
        {
            sprite.Play("run");
        }
        else
        {
            sprite.Pause();
        }
        if (Velocity.X > 0)
        {
            sprite.FlipH = false;

        }
        else if (Velocity.X < 0)
        {
            sprite.FlipH = true;
        }
        MoveAndSlide();
    }
}
