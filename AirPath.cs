using Godot;
using System;

public partial class AirPath : Path2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Line2D l = new (){
		// 	DefaultColor = new Color(0,0,0,0.2f),
		// 	Width = 2,
		// };
	
		// foreach (var point in Curve.GetBakedPoints())
		// {
		// 	l.AddPoint(point + Position);
		// }
		// GetNode<Node2D>("/root/RootScene/GameScene/").CallDeferred("add_child", l);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
