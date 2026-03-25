using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public bool server = false;
    public string host;

    public override void _Ready()
    {
        Instance = this;
    }

    public void GoToGame(bool server, string host = "")
    {
        this.server = server;
        this.host = host;
        GetTree().ChangeSceneToFile("res://Game.tscn");
    }

    public void StartServer()
    {
        ENetMultiplayerPeer peer = new ENetMultiplayerPeer();
        peer.CreateServer(8000, 64);
        Multiplayer.MultiplayerPeer = peer;
    }

    public void Join()
    {
        ENetMultiplayerPeer peer = new ENetMultiplayerPeer();
        peer.CreateClient(host, 8000);
        Multiplayer.MultiplayerPeer = peer;
    }

    public void Start()
    {
        if (server)
        {
            StartServer();
        }
        else
        {
            Join();
        }
    }


}
