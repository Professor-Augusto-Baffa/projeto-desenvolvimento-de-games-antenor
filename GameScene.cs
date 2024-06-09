using Godot;
using System;

public partial class GameScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Node2D>("/root/RootScene/GameScene/HUD/CenterSquare").Visible = true;
		GetNode<Node2D>("/root/RootScene/GameScene/HUD/CenterSquare").Visible = true;
		GetNode<Sprite2D>("/root/RootScene/GameScene/HUD/UpperBanner/badAnimation/bad").Visible = true;
		GetNode<TextureRect>("/root/RootScene/GameScene/MainBG").Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	private void _on_btn_end_pressed()
	{
		GetNode<Game>("/root/Game/").EndGame();
	}
}