using Godot;
using System;

public partial class JumpPad : Area2D
{
    [Export]
    public uint JumpForce = 3000;
    private AnimatedSprite2D _animatedSprite;
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        base._Ready();
    }
    public void OnBodyEntered(CharacterBody2D player)
    {
        if (player is Player)
        {
            _animatedSprite.Play("jump");
            (player as Player).Jump(JumpForce);
        }
        
    }
}
