using Godot;

using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
    [Export]
    public uint Gravity = 400;
    [Export]
    public uint Speed = 12500;
    [Export]
    public uint JumpForce = 10000;
    [Export]
    public bool Active = true;
    private AnimatedSprite2D _animatedSprite;
    private bg_music _soundPlayer;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _soundPlayer = GetNode<bg_music>("/root/BgMusic");
        base._Ready();
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Active is false)
            return;
        if (IsOnFloor() is false)
            Velocity = new(y: Math.Clamp(Velocity.Y + (float)(Gravity * delta), -500, 500), x: Velocity.X);
        if (Input.IsActionJustPressed("jump") && IsOnFloor() is true)
            Jump((float)(JumpForce * delta));
        var dir = Input.GetAxis("move_left", "move_right");
        Velocity = new((float)(dir * delta) * Speed, Velocity.Y);
        _animatedSprite.FlipH = Math.Sign(Velocity.X) == -1;
        MoveAndSlide();
 
        base._PhysicsProcess(delta);
        _updateAnimation(dir);
    }
    public void Jump(float jumpForce)
    {
        Velocity = new(Velocity.X, -jumpForce);
        _soundPlayer.PlaySfx(SfxMode.Jump);
    }
    private void _updateAnimation(float dir)
    {
        if(IsOnFloor() is true)
        {
            if(dir == 0)
            {
                _animatedSprite.Play("idle");
            }
            else
            {
                _animatedSprite.Play("run");
            }
        }
        else
        {
            if (Velocity.Y < 0)
                _animatedSprite.Play("jump");
            else
                _animatedSprite.Play("fall");
        }
    }
}
