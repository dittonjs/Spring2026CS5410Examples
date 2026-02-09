using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node2D
{

    private Dictionary<string, PackedScene> levels = new Dictionary<string, PackedScene>()
    {
        {"home", GD.Load<PackedScene>("res://scenes/levels/Home.tscn")},
        {"level1", GD.Load<PackedScene>("res://scenes/levels/Level1.tscn")},
    };

    public void ChangeLevel(string levelName)
    {
        if (levels.ContainsKey(levelName))
        {
            GetTree().ChangeSceneToPacked(levels[levelName]);
        }
        else
        {
            GD.PrintErr("Level not found: " + levelName);
        }
    }

}
