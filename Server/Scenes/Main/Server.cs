using Godot;
using System;

public partial class Server : Node
{
    private readonly ENetMultiplayerPeer _network;
    private readonly int _port;
    private readonly int _maxUser;
    public Server()
    {
        _port = 8800;
        _maxUser = 100;
        _network = new();
    }
    public override void _Ready()
    {
        _StartServer();
        base._Ready();
    }

    private void _StartServer()
    {
        _network.CreateServer(_port, _maxUser);
        Multiplayer.MultiplayerPeer = _network;
        GD.Print("Server started");
        _network.PeerConnected += _network_PeerConnected;
        _network.PeerDisconnected += _network_PeerDisconnected;
    }

    private void _network_PeerDisconnected(long id)
    {
        GD.Print($"User {id} disconnected");
    }

    private void _network_PeerConnected(long id)
    {
        GD.Print($"$User {id} connected");
    }
}
