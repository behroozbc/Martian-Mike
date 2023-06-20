using Godot;

using System;

public partial class Start : StaticBody2D
{
    private Marker2D _spawnPosition;

    public override void _Ready()
    {
        this._spawnPosition = GetNode<Marker2D>("SpawnPosition");
        base._Ready();
    }
    public Vector2 SpawnPosition
    {
        get
        {
            return _spawnPosition?.GlobalPosition ?? Vector2.Zero;
        }
    }
}
