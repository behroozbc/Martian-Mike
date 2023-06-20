using Godot;

using System;
using System.Diagnostics;

public partial class bg_music : AudioStreamPlayer
{
    private AudioStream _hurt;
    private AudioStream _jump;

    public override void _Ready()
    {
        this._hurt = GD.Load<AudioStream>("res://assets/audio/hurt.wav");
        this._jump = GD.Load<AudioStream>("res://assets/audio/jump.wav");
        base._Ready();
    }
    public async void PlaySfx(SfxMode mode)
    {
        var player = new AudioStreamPlayer();
        switch (mode)
        {
            case SfxMode.Hurt:
                player.Stream = this._hurt;
                break;
            case SfxMode.Jump:
                player.Stream = this._jump;
                break;
            default:
                throw new Exception();
                break;
        }
        AddChild(player);
        player.Play();
        player.Finished += () =>
        {
            RemoveChild(player);
        };
    }
}
public enum SfxMode
{
    Jump,
    Hurt
}