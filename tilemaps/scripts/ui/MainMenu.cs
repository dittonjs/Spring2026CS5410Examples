using Godot;



public partial class MainMenu : CanvasLayer
{
    public override void _Ready()
    {
        var startButton = GetNode<Button>("Button");
        startButton.Pressed += () =>
        {
            var gameManager = GetTree().Root.GetNode<GameManager>("GameManager");
            gameManager.ChangeLevel("level1");
        };
    }
}
