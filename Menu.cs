using Godot;
using System;

public partial class Menu : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void StartGame(bool IsControledByMouse=true) 
	{
		var scene = GD.Load<PackedScene>("res://GameScene.tscn");
		GetNode<Node>("/root/RootScene/Menu").QueueFree();
		GetNode<Node>("/root/RootScene/").AddChild(scene.Instantiate());
		GetNode<Node>("/root/RootScene/GameScene").GetTree().Paused = true;
		GetNode<Plane>("/root/RootScene/GameScene/Plane").IsControledByMouse = IsControledByMouse;
		GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/MainBGM").Play();
	}
	
	private void _on_btn_mouse_pressed()
	{
		StartGame(IsControledByMouse: true);
	}

	private void _on_btn_keyboard_pressed()
	{
		StartGame(IsControledByMouse: false);
	}

}




