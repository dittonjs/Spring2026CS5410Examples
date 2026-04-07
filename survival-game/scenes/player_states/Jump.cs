using Godot;
using System;

public partial class Jump : Node2D
{
  private float gravity = 200;
  private float jumpForce = 150.0f;
  public override void _Ready()
  {
    GD.Print("Jump: Ready");
    Player player = GetParent<Player>();
    player.Velocity = new Vector2(player.Velocity.X, -jumpForce);
  }

  public override void _Process(double delta) {
    Player player = GetParent<Player>();
    if (Input.IsActionJustPressed("ui_up")) {
      player.CallDeferred("ChangeState", "DoubleJump");
      return;
    }
    float moveX = Input.GetAxis("ui_left", "ui_right");
    player.Velocity = new Vector2(moveX * player.Speed, player.Velocity.Y + gravity * (float)delta);
    player.MoveAndSlide();
    if (player.Velocity.Y >= 0) {
      player.ChangeState("Falling");
    }
  }
}
