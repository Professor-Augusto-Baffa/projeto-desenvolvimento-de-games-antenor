using Godot;
using System;

public partial class RootScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("esc")) {
			GetNode<Label>("/root/RootScene/GameScene/HUD/PausedLabel").Visible = !GetNode<Label>("/root/RootScene/GameScene/HUD/PausedLabel").Visible;
			GetNode<Label>("/root/RootScene/GameScene/HUD/PausedLabel2").Visible = !GetNode<Label>("/root/RootScene/GameScene/HUD/PausedLabel2").Visible;
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

		if (isWithinVerticalThreshold && isWithinHorizontalThreshold)
		{
			GetNode<Node>("/root/").GetTree().Paused = false;
			GetNode<Area2D>("/root/RootScene/GameScene/HUD/CenterSquare").QueueFree();
		}
	}
}
