using Godot;
using System;

public partial class RootScene : Node2D
{
	private bool firstTime = true;
	private bool isPauseDisabled = true;
	public override void _Ready()
	{
		// GetNode<Node2D>("/root/RootScene/GameScene/HUD/Pause").Visible = true;
		// GetNode<Node2D>("/root/RootScene/GameScene/HUD/Pause").Modulate = new Color(0,0,0,0);
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("esc") && !isPauseDisabled) {
			GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/Pause/PauseAnimation").Play("PauseAnimation");
			if (!GetNode<Node>("/root/RootScene/GameScene").GetTree().Paused) {
				GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/Pause/PauseAnimation").Play("PauseFadeIn");
			}
			else {
				GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/Pause/PauseAnimation").Play("PauseFadeOut");
			}
			
			GetNode<Node2D>("/root/RootScene/GameScene/HUD/Pause").Visible = !GetNode<Node2D>("/root/RootScene/GameScene/HUD/Pause").Visible;
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGM").StreamPaused = !GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGM").StreamPaused;
			GetNode<Node>("/root/RootScene/GameScene").GetTree().Paused = !GetNode<Node>("/root/RootScene/GameScene").GetTree().Paused;
		}

		float mousePosX = GetViewport().GetMousePosition().X;
		float mousePosY = GetViewport().GetMousePosition().Y;

		float centerX = 579;
		float centerY = 307;

		float threshold = 20.0f;

		bool isWithinVerticalThreshold = Math.Abs(mousePosX - centerX) <= threshold;
		bool isWithinHorizontalThreshold = Math.Abs(mousePosY - centerY) <= threshold;

		if (GetNode<Node>("/root/").GetTree().Paused) {
			bool IsControledByMouse = GetNode<Plane>("/root/RootScene/GameScene/Plane").IsControledByMouse;
			if ((isWithinVerticalThreshold && isWithinHorizontalThreshold) || !IsControledByMouse)
			{
				if (firstTime) {
					firstTime = false;
					GetNode<Sprite2D>("/root/RootScene/GameScene/HUD/CenterSquare/Sprite2D").Modulate = new Color(0, 125, 0);
					GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/CenterSquare/AnimationPlayer").Play("InstrFadeOut");
					GetNode<Node>("/root/").GetTree().Paused = false;
					var playPosition = GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGMBass").GetPlaybackPosition();
					GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGMBass").Stop();
					GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGM").Play(playPosition);
					var timer = new Timer
					{
						Autostart = true,
						WaitTime = 1,
						ProcessMode = ProcessModeEnum.Pausable,
						OneShot = true
					};
					AddChild(timer);
					timer.Timeout += () => {
						GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/CenterSquare/AnimationPlayer").Stop();
						GetNode<Area2D>("/root/RootScene/GameScene/HUD/CenterSquare").QueueFree();
						isPauseDisabled = false;
					};
				}
			}
		}
	}
}
