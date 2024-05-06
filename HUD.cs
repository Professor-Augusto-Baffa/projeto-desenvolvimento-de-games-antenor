using Godot;
using System;

public partial class HUD : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var game = GetNode<Game>("/root/Game");
		var HeadingLabel = GetNode<Label>("HeadingLabel");
		var compass = GetNode<Sprite2D>("/root/RootScene/GameScene/HUD/Compass");
		compass.RotationDegrees = -game.Heading;
		var ScoreLabel = GetNode<Label>("ScoreLabel");
		HeadingLabel.Text = Util.LeftPad(game.Heading, 3) + "°";
		ScoreLabel.Text = Util.LeftPad(game.Score, 8);
		var RemainingTime = GetNode<Game>("/root/Game").RemainingTime;
		var TimeLabel = GetNode<Label>("TimeLabel"); 
		TimeLabel.Text = Util.FormatTime(RemainingTime);
		var SpeedLabel = GetNode<Label>("SpeedLabel"); 
		SpeedLabel.Text = game.Speed + "kt";
	}
}
