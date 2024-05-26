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
	public int RemainingTime = 0;
	public int Speed = 0;
	public int Health = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Health = Levels.getLevelInfo(Levels.Info.InitialHealth);
		Score = 200;
		RemainingTime = 5 * 60;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Score <= 10 || Health < 0) {
			GetNode<Node>("/root/RootScene/GameScene").GetTree().Paused = true;
			GetNode<Label>("/root/RootScene/GameScene/HUD/BannerLabel").Text = $"FIM DE JOGO\nScore: {GetNode<Game>("/root/Game").Score }";
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
				this.Health = Levels.getLevelInfo(Levels.Info.InitialHealth);;
				this.RemainingTime = 5 * 60;
				this.Heading = 0;
				GetNode<Node>("/root/").GetTree().ReloadCurrentScene();
				GetNode<Node>("/root/").GetTree().Paused = false;
			};
		}
	}
}
