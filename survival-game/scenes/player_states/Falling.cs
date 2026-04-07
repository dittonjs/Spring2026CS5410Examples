using Godot;
using System;

public partial class Falling : Node2D
{
  private float gravity = 300;
  public override void _Process(double delta)
  {
    Player player = GetParent<Player>();
    if (Input.IsActionJustPressed("ui_up")) {
      player.CallDeferred("ChangeState", "DoubleJump");
      return;
    }

    float moveX = Input.GetAxis("ui_left", "ui_right");
    player.Velocity = new Vector2(moveX * player.Speed, player.Velocity.Y + gravity * (float)delta);
    player.MoveAndSlide();
    if (player.IsOnFloor()) {
      player.CallDeferred("ChangeState", "Grounded");
    }
  }
}
