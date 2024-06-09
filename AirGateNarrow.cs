using Godot;
using System;

public partial class AirGateNarrow : Node2D
{
	enum F {Front, Back, None};
	F lastEnteredFrom = F.None;
	/*
	 * If the plane touches the pylon any apparent valid pass
	 * should not be counted.
	 */
	private bool hasExplodedJustBefore = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

private void _on_front_area_entered(Area2D area)
	{
		if (hasExplodedJustBefore) {
			hasExplodedJustBefore = false;
			return;
		}
		if (area is Plane)
		{
			if (lastEnteredFrom == F.Back) 
			{
				lastEnteredFrom = F.None;
				GetNode<Game>("/root/Game").Health -= Levels.getLevelInfo(Levels.Info.LoseHealthSpeed);
				var b = GetNode<Banner>("/root/Banner");
				b.showUpperBanner("Sentido errado!", bad: true);
			}
			else 
			{
				lastEnteredFrom = F.Front;
			}
		}
	}


	private void _on_back_area_entered(Area2D area)
	{
		if (area is Plane)
		{
			if (hasExplodedJustBefore) {
				hasExplodedJustBefore = false;
				return;
			}
			Plane plane = (Plane)area;
			if (lastEnteredFrom == F.Front) 
			{
				lastEnteredFrom = F.None;
				if (Math.Abs(plane.HeadingSpeed) > 1.0f)
				{
					GetNode<Game>("/root/Game").Score += 100;
					GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/AirGateNarrowPassSFX").Play();
					var b = GetNode<Banner>("/root/Banner");
					b.showUpperBanner("Perfeito!");
				}
				else
				{
					GetNode<Game>("/root/Game").Health -= 1;
					GetNode<Game>("/root/Game").Score -= 100;
					var b = GetNode<Banner>("/root/Banner");
					b.showUpperBanner("Passe com a asa inclinada!", bad: true);
				}
			}
			else 
			{
				lastEnteredFrom = F.Back;
			}
		}
	}


	private void _on_gate_area_entered(Area2D area)
	{
		if (area is Plane)
		{
			GetNode<Game>("/root/Game").Score -= 100;
		}
	}

	private void Explode(Area2D area, String leftOrRight) {
		if (area is Plane)
		{
			hasExplodedJustBefore = true;
			GetNode<Game>("/root/Game").Score -= 100;
			GetNode<AnimatedSprite2D>("Gate" + leftOrRight + "/Explosion").Visible = true;
			GetNode<AnimatedSprite2D>("Gate" + leftOrRight + "/Explosion").Play();
			GetNode<Godot.Timer>("Gate" + leftOrRight + "/ExplosionTimer").Start();
			GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/FireSFX").Play();
			var b = GetNode<Banner>("/root/Banner");
			b.showUpperBanner("Atingiu o pylon!", bad: true);
		}
	}
	private void _on_right_area_entered(Area2D area)
	{
		if (hasExplodedJustBefore) {
			return;
		}
		Explode(area, "Right");
	}
	private void _on_left_gate_area_entered(Area2D area)
	{
		if (hasExplodedJustBefore) {
			return;
		}
		Explode(area, "Left");
	}
	private void _on_explosion_left_timer_timeout()
	{
		GetNode<AnimatedSprite2D>("GateLeft/Explosion").Visible = false;
	}

	private void _on_explosion_right_timer_timeout()
	{
		GetNode<AnimatedSprite2D>("GateRight/Explosion").Visible = false;
	}
}



