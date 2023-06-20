using Godot;
using System;

public partial class ui_layer : CanvasLayer
{
    private win_screen _winScreen;

    public override void _Ready()
    {
        this._winScreen = GetNode<win_screen>("WinScreen");
        base._Ready();

    }
    public void ShowWinScreen()
    {
        _winScreen.Visible = true;
    }
}
