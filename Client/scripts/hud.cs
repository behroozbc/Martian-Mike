using Godot;
using System;

public partial class hud : Control
{
    private Label _timeLable;

    public override void _Ready()
    {
        this._timeLable = GetNode<Label>("Label");
        base._Ready();
    }

    public string Time
    {

        set { _timeLable.Text = "TIME "+value; }
    }

}
