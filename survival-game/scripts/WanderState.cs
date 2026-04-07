using Godot;

public partial class WanderState : IEnemyState
{
    private Enemy enemy;
    private Character character;
    private Vector2 direction;
    private Timer timer;
    public void Enter()
    {
        direction = new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
        timer = new();
        timer.Timeout += () => {
            direction = new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
            enemy.Velocity = direction * 50;
        };
        timer.Autostart = true;
        timer.WaitTime = 1;
        enemy.AddChild(timer);
    }

    public void Exit()
    {
        GD.Print("WanderState: Exit");
        timer.QueueFree();
    }

    public void Process(double delta) {
        if (character.Position.DistanceTo(enemy.Position) < 100) {
            enemy.ChangeState(new ChaseState());
        } else {
            enemy.MoveAndSlide();
        }
    }

    public void Init(Enemy enemy, Character character) {
        this.enemy = enemy;
        this.character = character;
        GD.Print("WanderState: Init");
    }
}
