using Godot;
using System;

public partial class TimePowerUp : Area2D
{
	public void _on_area_entered(Area2D area)
	{
		if (area is Plane)
		{
			GetNode<Game>("/root/Game").RemainingTime += 30;
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/ExtraTime").Play();
			var b = GetNode<Banner>("/root/Banner");
			b.showUpperBanner("Ganhou 30 segundos!", AudioName: "ganhou 30 segundos");
			QueueFree();
		}
	}
}



