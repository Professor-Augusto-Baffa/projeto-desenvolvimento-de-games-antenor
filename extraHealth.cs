using Godot;
using System;

public partial class extraHealth : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_area_entered(Area2D area)
	{
		if (area is Plane)
		{
			GetNode<Game>("/root/Game").Health += 1;
			QueueFree();
			var b = GetNode<Banner>("/root/Banner");
			b.showUpperBanner("Vida extra!", AudioName: "ganhou vida extra");
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/ExtraHealth").Play();
		}
	}
}
