using Godot;
using System;

public partial class StartCountdown : Node2D
{
	[Export]
	int CountDownStart = 3;

	int currentTime;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentTime = CountDownStart;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
		
	private void _on_timer_timeout()
	{
		// var StartTimerLabel = GetNode<Label>("/root/RootScene/GameScene/StartCountdown/StartTimerLabel");
		// if (currentTime > 1) 
		// {
		// 	currentTime--;
		// }
		// else 
		// {
		// 	GetNode<Node2D>("/root/RootScene/GameScene/StartCountdown").QueueFree();
		// 	GetNode<Node>("/root/").GetTree().Paused = false;
		// }
		// StartTimerLabel.Text = currentTime.ToString();
	}	
}

