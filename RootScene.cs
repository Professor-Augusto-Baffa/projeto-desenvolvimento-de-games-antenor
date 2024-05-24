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
	}
}
