using Godot;

public partial class ChaseState : IEnemyState
{
    private Enemy enemy;
    private Character character;
    public void Enter()
    {
    }

    public void Exit()
    {
        GD.Print("ChaseState: Exit");
    }

    public void Process(double delta) {
        if (character.Position.DistanceTo(enemy.Position) > 200) {
            enemy.ChangeState(new WanderState());
        } else if (character.Position.DistanceTo(enemy.Position) < 50) {
            enemy.ChangeState(new AttackState());
        }
        else {
            enemy.Velocity = (character.Position - enemy.Position).Normalized() * 50;
            enemy.MoveAndSlide();
        }
    }

    public void Init(Enemy enemy, Character character) {
        this.enemy = enemy;
        this.character = character;
        GD.Print("ChaseState: Init");
    }
}
