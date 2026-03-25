using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    LineEdit hostInput;

    [Export]
    Button join;

    [Export]
    Button server;


    public override void _Ready()
    {
        join.Pressed += Join;
        server.Pressed += Server;
    }

    public void Join()
    {
        GameManager.Instance.GoToGame(false, hostInput.Text);
    }

    public void Server()
    {
        GameManager.Instance.GoToGame(true);
    }
}
