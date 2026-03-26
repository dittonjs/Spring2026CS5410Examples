using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public override void _EnterTree()
    {
        this.SetMultiplayerAuthority(int.Parse(Name));
    }
    public override void _PhysicsProcess(double delta)
    {
        if (!IsMultiplayerAuthority()) return;

        Velocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down") * 500;
        MoveAndSlide();
    }
}
