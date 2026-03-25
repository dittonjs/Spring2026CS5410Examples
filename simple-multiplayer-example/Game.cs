using Godot;
using System;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        GameManager.Instance.Start();
        Multiplayer.PeerConnected += PeerConnected;
        Multiplayer.PeerDisconnected += PeerDisconnected;
    }
    public void PeerConnected(long id)
    {
        if (!Multiplayer.IsServer()) return;

        GD.Print("New peer connected: " + id);
    }

    public void PeerDisconnected(long id)
    {
        if (!Multiplayer.IsServer()) return;

        GD.Print("Peer disconnected!");
    }
}
