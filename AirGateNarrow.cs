using Godot;
using System;

public partial class AirGateNarrow : Node2D
{
	enum F {Front, Back, None};
	F lastEnteredFrom = F.None;
	
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
		if (area is Plane)
		{
			Plane plane = (Plane)area;
			if (lastEnteredFrom == F.Back) 
			{
				lastEnteredFrom = F.None;
				if (plane.HeadingSpeed > 0.8)
				{
					GetNode<Game>("/root/Game").Score -= 100;
				}
				else
				{
				GetNode<Game>("/root/Game").Score -= 150;
				}
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
			Plane plane = (Plane)area;
			if (lastEnteredFrom == F.Front) 
			{
				lastEnteredFrom = F.None;
				if (plane.HeadingSpeed > 0.8)
				{
					GetNode<Game>("/root/Game").Score += 100;
				}
				else
				{
					GetNode<Game>("/root/Game").Score -= 100;
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
}



