using Godot;
using System;
using System.Diagnostics;

public partial class BG : ParallaxBackground
{
    [Export]
    public int ScrollSpeed = 0;
    [Export]
    public CompressedTexture2D BGTexture = GD.Load<CompressedTexture2D>("res://assets/textures/bg/Blue.png");
    private Sprite2D _sprite;

    public override void _Ready()
    {
        this._sprite = GetNode<Sprite2D>("ParallaxLayer/Sprite2D");
        _sprite.Texture = BGTexture;
        base._Ready();
    }
    public override void _Process(double delta)
    {
        _sprite.RegionRect = new Rect2(_sprite.RegionRect.Position+new Vector2((float)(ScrollSpeed*delta),(float)(ScrollSpeed*delta)), _sprite.RegionRect.Size);
        if(this._sprite.RegionRect.Position>=new Vector2(64, 64))
        {
            _sprite.RegionRect=new(Vector2.Zero, _sprite.RegionRect.Size);
        }
        base._Process(delta);
    }
}
