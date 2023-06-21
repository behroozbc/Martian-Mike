using Godot;
using System;

public partial class Server : Node
{
    private readonly ENetMultiplayerPeer _network;
    private readonly int _port;
    private readonly string _host;
    public Server()
    {
        _network = new();
        _port = 8800;
        _host = "localhost";
    }
    public override void _Ready()
    {
        _connectToServer();
        base._Ready();
    }

    private void _connectToServer()
    {
       _network.CreateClient(_host, _port);
       Multiplayer.MultiplayerPeer = _network;
        Multiplayer.ConnectionFailed += Multiplayer_ConnectionFailed;
        Multiplayer.ConnectedToServer += Multiplayer_ConnectedToServer; ;
    }

    private void Multiplayer_ConnectedToServer()
    {
        GD.Print("Succesfully connected");
    }

    private void Multiplayer_ConnectionFailed()
    {
        GD.Print("Faild to connect");
    }
}
