using System.ComponentModel.DataAnnotations;

public interface IEnemyState
{
    void Enter();
    void Exit();
    void Process(double delta);

    void Init(Enemy enemy, Character character);
}
