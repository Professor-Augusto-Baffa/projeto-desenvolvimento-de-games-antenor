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

	public override void _Ready()
	{
		if (isMovable) {
			startX = Position.X;
			startY = Position.Y;
			moveRange = GD.RandRange(20, 50);
			moveSpeed = 80 - moveRange;
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
				Position = new Vector2((float)(Position.X + (moveSpeed * delta)), Position.Y);
			}
			else if (currentDirection == D.Right)
			{
				Position = new Vector2((float)(Position.X + (-moveSpeed * delta)), Position.Y);
			}
		}
	}
	
	private void _on_front_area_entered(Area2D area)
	{
		if (area is Plane)
		{
			if (lastEnteredFrom == F.Back) 
			{
				lastEnteredFrom = F.None;
				GetNode<Game>("/root/Game").Score -= 100;
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
			if (lastEnteredFrom == F.Front) 
			{
				lastEnteredFrom = F.None;
				GetNode<Game>("/root/Game").Score += 100;
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
}



