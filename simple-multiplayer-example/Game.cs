using Godot;
using System;
// 144.39.105.125
public partial class Game : Node2D
{
    [Export]
    PackedScene PlayerScene;

    public override void _Ready()
    {
        GameManager.Instance.Start();
        Multiplayer.PeerConnected += PeerConnected;
        Multiplayer.PeerDisconnected += PeerDisconnected;

    }
    public void PeerConnected(long id)
    {
        if (!Multiplayer.IsServer()) return;

        Node player = PlayerScene.Instantiate();

        player.Name = id.ToString();

        CallDeferred("add_child", player);
    }

    public void PeerDisconnected(long id)
    {
        if (!Multiplayer.IsServer()) return;

        GetNode(id.ToString()).QueueFree();
    }
}
