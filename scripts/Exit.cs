using Godot;
using System;

public partial class Exit : Area2D
{
    private AnimatedSprite2D _animatedSprite;

    public override void _Ready()
    {
        this._animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        base._Ready();
    }
    public void Animate()
    {
        _animatedSprite.Play("default");
    }
}
