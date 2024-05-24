using Godot;
using System;

public partial class AirPath : Path2D
{
	[Export]
    public Texture2D ArrowTexture;
	[Export]
    public int ArrowInterval = Levels.getLevelInfo(Levels.Info.ArrowInterval);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (ArrowTexture == null)
        {
            GD.PrintErr("ArrowTexture not assigned.");
            return;
        }

        var points = Curve.GetBakedPoints();

		var length = Curve.GetBakedLength();
        for (int i = 0; i < points.Length; i += ArrowInterval)
        {
            Sprite2D arrow = new Sprite2D
            {
                Texture = ArrowTexture,
                Position = points[i] + Position,
            };

            // Calculate the rotation angle for the arrow sprite
            Vector2 direction;
            if (i < points.Length - 1)
            {
                direction = (points[i + 1] - points[i]).Normalized();
            }
            else
            {
                direction = (points[i] - points[i - 1]).Normalized();
            }
            float angle = direction.Angle();
            arrow.Rotation = angle + (float)Math.PI / 2;

            GetNode<Node2D>("/root/RootScene/GameScene/").CallDeferred("add_child", arrow);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
