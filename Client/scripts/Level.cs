using Godot;

using System;
using System.Diagnostics;

public partial class Level : Node2D
{
    [Export]
    public PackedScene NextLevel = null;
    [Export]
    public short LevelTime = 30;
    [Export]
    public bool IsFinalLevel = false;
    private Start _start;
    private Player _player;
    private bg_music _soundPlayer;
    private Exit _exitArea;
    private hud _hud;
    private ui_layer _uiLayer;
    private short _timeLeft;
    private Timer _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _start = GetNode<Start>("Start");
        _player = (Player)GetTree().GetFirstNodeInGroup("player");
        _soundPlayer = GetNode<bg_music>("/root/BgMusic");
        GetNode<Area2D>("Deathzone").BodyEntered += OnDeathzoneBodyEntered;
        _exitArea = GetNode<Exit>("Exit");
        this._hud = GetNode<hud>("UILayer/HUD");
        this._uiLayer = GetNode<ui_layer>("UILayer");
        _exitArea.BodyEntered += ExitAreaBodyEntered;
        _timeLeft = LevelTime;
        this._timer=new Timer();
        _timer.WaitTime = 1;
        _timer.Timeout += _timer_Timeout;
        AddChild(this._timer);
        _hud.Time = _timeLeft.ToString();
        _timer.Start();
        foreach (var trap in GetTree().GetNodesInGroup("traps"))
        {
            (trap as Trap).TouchedPlayer += OnTrapTouchedPlayer;
        }
        _player.GlobalPosition = _start.SpawnPosition;
    }

    private void _timer_Timeout()
    {
        _timeLeft -= 1;
        _hud.Time = _timeLeft.ToString(); 
        if(_timeLeft < 0 )
        {
            _resetPlayer();
            _timeLeft = LevelTime;
        }
    }

    private async void ExitAreaBodyEntered(Node2D body)
    {
        if (body is not Player)
            return;
        Player player = (Player)body;
        _exitArea.Animate();
        _player.Active = false;
        _timer.Stop();
        await ToSignal(GetTree().CreateTimer(.5), "timeout");
        if(IsFinalLevel is true)
        {
            _uiLayer.ShowWinScreen();
            return;
        }
        if (NextLevel is not null)
            GetTree().ChangeSceneToPacked(NextLevel);
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("quit"))
            GetTree().Quit();
        if (Input.IsActionJustPressed("reset"))
            GetTree().ReloadCurrentScene();
    }
    public void OnDeathzoneBodyEntered(Node2D node)
    {
        if (node is Player)
            _resetPlayer();
    }
    private void _resetPlayer()
    {
        _soundPlayer.PlaySfx(SfxMode.Hurt);
        _player.Velocity = Vector2.Zero;
        _player.GlobalPosition = _start.SpawnPosition;
    }
    public void OnTrapTouchedPlayer()
    {
        _resetPlayer();
    }
}
