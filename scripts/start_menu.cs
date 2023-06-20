using Godot;

using System;

public partial class start_menu : Control
{
    public void OnStartBtnPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/level.tscn");
    }
    public void OnQuitBtnPressed()
    {
        GetTree().Quit();
    }
}
