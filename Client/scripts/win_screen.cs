using Godot;
using System;

public partial class win_screen : Control
{
    public void OnButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/level.tscn");
    }
}
