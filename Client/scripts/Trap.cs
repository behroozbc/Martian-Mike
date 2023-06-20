using Godot;
using System;

public partial class Trap : Node2D
{
    [Signal]
    public delegate void TouchedPlayerEventHandler();
    public void OnBodyEntered(CharacterBody2D body)
    {
        EmitSignal("TouchedPlayer");
    }
}
