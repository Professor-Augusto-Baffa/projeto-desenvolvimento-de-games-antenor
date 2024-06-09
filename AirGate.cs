using Godot;
using System;

public partial class AirGate : Node2D
{
	[Export]
	public bool isMovable = false;

	private enum F {Front, Back, None};
	private F lastEnteredFrom = F.None;
	private enum D{Left, Right};
	private D currentDirection = D.Left;
	private float startX, startY;
	private int moveRange;
	private int moveSpeed;
	/*
	 * If the plane touches the pylon any apparent valid pass
	 * should not be counted.
	 */
	private bool hasExplodedJustBefore = false;


	public override void _Ready()
	{
		if (isMovable) {
			startX = Position.X;
			startY = Position.Y;
			moveRange = Levels.getLevelInfo(Levels.Info.ArrowInterval);
			moveSpeed = 50;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isMovable) {
			if (Position.X <= (startX - moveRange) && currentDirection == D.Right)
			{
				currentDirection = D.Left;
			}
			else if (Position.X >= (startX + moveRange) && currentDirection == D.Left)
			{
				currentDirection = D.Right;
			}

			if (currentDirection == D.Left)
			{
				Vector2 v = new Vector2((float)(moveSpeed * delta), 0);
				v = v.Rotated(this.Rotation);
				Position += v;
			}
			else if (currentDirection == D.Right)
			{
				Vector2 v = new Vector2((float)(-moveSpeed * delta), 0);
				v = v.Rotated(this.Rotation);
				Position += v;
			}
		}
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
		if (hasExplodedJustBefore) {
			hasExplodedJustBefore = false;
			return;
		}
		if (area is Plane)
		{
			if (lastEnteredFrom == F.Front) 
			{
				lastEnteredFrom = F.None;
				float distanceF = GetNode<Path2D>("/root/RootScene/GameScene/AirPath").Curve.GetClosestPoint(Position).DistanceSquaredTo(Position);
				int distance = (int)Math.Round(distanceF) / 100;
				int speed = GetNode<Game>("/root/Game").Speed;
				int points = ((int)distance + speed);
				GetNode<Game>("/root/Game").Score += points;
				// GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/AirGatePassSFX").Play();
				GetNode<Label>("/root/RootScene/GameScene/HUD/AquiredPoints").Text = $"Dist:{distance}+Veloc:{speed} = {points} pontos";
				GetNode<Label>("/root/RootScene/GameScene/HUD/AquiredPoints").Visible = true;
				GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/AquiredPoints/AnimationPlayer").Play("AppearAndDisappear");
			}
			else 
			{
				lastEnteredFrom = F.Back;
			}
		}
	}

	private void Explode(Area2D area, String leftOrRight) {
		if (area is Plane)
		{
			hasExplodedJustBefore = true;
			GetNode<Game>("/root/Game").Score -= 100;
			GetNode<Game>("/root/Game").Health -= 1;
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


