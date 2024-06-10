using Godot;
using System;

public partial class Menu : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("/root/RootScene/Menu/LOGO/AnimationPlayer").Play("LOGO Animation");
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
	}
	
	private void _on_btn_mouse_pressed()
	{
		int sel = GetNode<OptionButton>("/root/RootScene/Menu/OptionButton").Selected;
		Levels.SelectedLevel = (Levels.Lvl)sel;
		StartGame(IsControledByMouse: true);
	}

	private void _on_btn_keyboard_pressed()
	{
		int sel = GetNode<OptionButton>("/root/RootScene/Menu/OptionButton").Selected;
		Levels.SelectedLevel = (Levels.Lvl)sel;
		StartGame(IsControledByMouse: false);
	}
	
	private void _on_option_sound_item_selected(long index)
	{
		GD.Print("a" + index);
		if (index == 0) {
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), 0);
		}
		else {
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), -80);
		}
	}
}
