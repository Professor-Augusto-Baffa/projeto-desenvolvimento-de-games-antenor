using Godot;
using System;
using System.ComponentModel;
using System.Globalization;

public partial class Plane : Area2D
{
	public float Speed = Levels.getLevelInfo(Levels.Info.Speed);

	public float MaxSpeed = Levels.getLevelInfo(Levels.Info.MaxSpeed);
	public float MinSpeed = Levels.getLevelInfo(Levels.Info.MinSpeed);

	public float HeadingSpeed = 0; // What rate the currentHeading can change
	
	private float Heading = 0; // Where the nose is pointing now

	[Export]
	private bool Debug = false; // When the debug is true, the aircraft can move front and back

	[Export]
	public bool IsControledByMouse = true; // When the debug is true, the aircraft can move front and back
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float magnitude = HandleInput();

		ChangeSpriteImage(HeadingSpeed: HeadingSpeed);

		HandleHeading(delta: delta, magnitude: magnitude);

		reduceBoundingBox();

		var distance = GetNode<Path2D>("/root/RootScene/GameScene/AirPath").Curve.GetClosestPoint(Position).DistanceSquaredTo(Position);

		
		if (distance < 10000) {
			
		}
		else if (distance < 50000 / Levels.getLevelInfo(Levels.Info.OutOfPathFactor)) {
			if (Time.GetTicksMsec() % 400 < 20) {
				GetNode<Game>("/root/Game").Health -= 1;
			}
			GetNode<Game>("/root/Game").Score -= 1;
		}
		else if (distance < 100000 / Levels.getLevelInfo(Levels.Info.OutOfPathFactor)) {
			if (Time.GetTicksMsec() % 400 < 20) {
				GetNode<Game>("/root/Game").Health -= 2;
			}
			GetNode<Game>("/root/Game").Score -= 1;
		}
		else {
			if (Time.GetTicksMsec() % 400 < 20) {
				GetNode<Game>("/root/Game").Health -= 1;
			}
			GetNode<Game>("/root/Game").Score -= 8;
		}
	}

    private float HandleInput()
    {
		float magnitude;
        if (Debug)
		{
			if (Input.IsActionPressed("ui_up"))
				magnitude = Speed;
			
			else if (Input.IsActionPressed("ui_down"))
				magnitude = -Speed;
			
			else
				magnitude = 0;
		}
		else
		{
			if (Input.IsActionPressed("ui_up")) {
				if (Speed + 5 < MaxSpeed) {
					Speed += 5;
				}
			}
			
			else if (Input.IsActionPressed("ui_down")) {
				if (Speed - 5 > MinSpeed) {
					Speed -= 5;
				}
			}

			GetNode<Game>("/root/Game").Speed = (int)Speed / 2;
						
			magnitude = Speed;
		}

		if (IsControledByMouse)
		{
			float halfWidth = GetViewportRect().Size.X / 2;
			float mousePosX = GetViewport().GetMousePosition().X;
			float mousePosRelative = mousePosX - halfWidth;

			float sensitivityCurve = 2.0f - Mathf.Pow(Mathf.Abs(mousePosRelative) / halfWidth, 1.8f);
			float mouseNormalized = (mousePosRelative / halfWidth) * sensitivityCurve * 3f;

			HeadingSpeed = mouseNormalized;
		}
		else
		{
			if (Input.IsActionPressed("ui_left")) 
				HeadingSpeed = -1;
			else if (Input.IsActionPressed("ui_right")) 
				HeadingSpeed = 1;
			else 
				HeadingSpeed = 0;
		}

		return magnitude;
    }


    private void ChangeSpriteImage(float HeadingSpeed)
    {
		/*
		 * Change aircraft image to show the user how its turning
		 */

		HeadingSpeed = Mathf.Clamp(HeadingSpeed, -5.0f, 5.0f);

		float normalizedHeadingSpeed = (HeadingSpeed + 5.0f) / 10.0f; 
		normalizedHeadingSpeed = 1 - normalizedHeadingSpeed;
		int number = Mathf.RoundToInt(normalizedHeadingSpeed * 115) + 1; 

		string imagePath = "res://Assets/AircraftConv/" + number + ".png";
		var img = (Texture2D)GD.Load(imagePath);
		GetChild<Sprite2D>(0).Texture = img;
		
    }


    private void HandleHeading(double delta, float magnitude)
    {
		/*
		 * Calculate the current heading based on the heading speed
		 */

        Heading += HeadingSpeed * (float)delta;
		if (Heading < 0) 
			Heading += 2 * Mathf.Pi;
		else if (Heading > 2 * Mathf.Pi) 
			Heading %= 2 * Mathf.Pi;
		
		Vector2 currentDirection = new Vector2(0, -1).Rotated(Heading) * magnitude;
		this.Position += currentDirection * (float)delta;
		this.Rotation = Heading;

		int HeadingDegrees = (int)Mathf.RadToDeg(Heading);
		GetNode<Game>("/root/Game").Heading = HeadingDegrees;	
    }


    private void reduceBoundingBox()
    {
        float ColisionBoxSize = 1 - Mathf.Abs(HeadingSpeed) / 4.2f;
		GetNode<CollisionShape2D>("/root/RootScene/GameScene/Plane/PlaneWings").Scale = new Vector2(ColisionBoxSize, 1.0f);
		GetNode<CollisionShape2D>("/root/RootScene/GameScene/Plane/PlaneProfundor").Scale = new Vector2(ColisionBoxSize, 1.0f);
    }
}
