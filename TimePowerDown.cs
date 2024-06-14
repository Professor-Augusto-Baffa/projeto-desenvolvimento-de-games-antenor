using Godot;
using System;

public partial class TimePowerDown : TimePowerUp
{
	new private void _on_area_entered(Area2D area)
	{
		if (area is Plane)
		{
			GetNode<Game>("/root/Game").RemainingTime -= 30;
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/LessTime").Play();
			var b = GetNode<Banner>("/root/Banner");
			b.showUpperBanner("Perdeu 30 segundos!", bad: true, AudioName: "perdeu 30 segundos");
			QueueFree();
		}
	}
}
