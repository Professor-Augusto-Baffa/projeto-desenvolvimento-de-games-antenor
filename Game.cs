using Godot;
using System;
using System.Threading;

public partial class Game : Node
{
	/*
	 * Global Variable goes here
	 */
	public int Score = 0;
	public int Heading = 0;
	public int RemainingTime = 5 * 60;
	public int Speed = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Score < 0) {
			GetNode<Node>("/root/RootScene/GameScene").GetTree().Paused = true;
			GetNode<Label>("/root/RootScene/GameScene/HUD/BannerLabel").Text = "PERDEU, MANÉ";
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGM").Stop();	
			var randomBreakSound = GD.Randi() % 4 + 1;
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/LOSE").Play();
            var timer = new Timer
            {
                Autostart = true,
                WaitTime = 3,
				ProcessMode = ProcessModeEnum.WhenPaused,
				OneShot = true
            };
            AddChild(timer);
			timer.Timeout += () => {
				this.Score = 200;
				this.RemainingTime = 5 * 60;
				this.Heading = 0;
				GetNode<Node>("/root/").GetTree().ReloadCurrentScene();
				GetNode<Node>("/root/").GetTree().Paused = false;
			};
		}
	}
}
