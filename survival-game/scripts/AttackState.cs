using Godot;

public partial class AttackState : IEnemyState
{
    private Enemy enemy;
    private Character character;

    float angularVelocity = 100f;

    public void Enter()
    {
    }

    public void Exit()
    {
        GD.Print("AttackState: Exit");
    }

    public void Process(double delta) {
        if (character.Position.DistanceTo(enemy.Position) > 60) {
            enemy.ChangeState(new ChaseState());
        } else {
            enemy.Rotate(angularVelocity * (float)delta);
            enemy.Velocity = enemy.Position.DirectionTo(character.Position) * 50;
            enemy.MoveAndSlide();
        }
    }

    public void Init(Enemy enemy, Character character) {
        this.enemy = enemy;
        this.character = character;
        GD.Print("AttackState: Init");
    }
}
