using Godot;
using System;

public partial class Grounded : Node2D, IPlayerState
{
  public override void _Ready()
  {

  }

  public override void _Process(double delta)
  {
    Player player = GetParent<Player>();
    if (Input.IsActionJustPressed("ui_up")) {
      player.CallDeferred("ChangeState", "Jump");
    }
    float moveX = Input.GetAxis("ui_left", "ui_right");
    player.Velocity = new Vector2(moveX * player.Speed, 0);
    player.MoveAndSlide();
    if (!player.IsOnFloor()) {
      player.CallDeferred("ChangeState", "Falling");
    }
  }


}
