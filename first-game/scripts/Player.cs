using Godot;

public partial class Player : CharacterBody2D
{

    [Export]
    public int Speed { get; set; } = 400;
    [Export]
    public float Gravity { get; set; } = 100.0f;
    [Export]
    public int JumpForce { get; set; } = 400;
    public override void _Ready()
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        float xAxis = Input.GetAxis("move_left", "move_right");

        Vector2 velocity = new Vector2(xAxis * Speed, Velocity.Y);
        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            velocity.Y = -JumpForce;
        }
        this.Velocity = velocity;
        this.Velocity += Vector2.Down * Gravity * (float)delta;
        this.MoveAndSlide();
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            GD.Print("Collided with: " + GetSlideCollision(i).GetCollider());
            if (GetSlideCollision(i).GetCollider() is RigidBody2D body)
            {
                body.ApplyForce(GetSlideCollision(i).GetNormal() * -100);
            }
        }
    }
    // public override void _Input(InputEvent @event)
    // {
    //     GD.Print(@event);
    // }
}
