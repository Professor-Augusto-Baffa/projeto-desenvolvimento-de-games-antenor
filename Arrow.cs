using Godot;
using System;

public partial class Arrow : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Modulate = new Color(255, 255, 255);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_entered(Area2D area)
	{
		if (area is Plane) {
			this.Modulate = new Color(255, 0, 0);
		}
	}
	private void _on_area_exited(Area2D area)
	{
		if (area is Plane) {
			var timer = new Timer
            {
                Autostart = true,
                WaitTime = 3,
				ProcessMode = ProcessModeEnum.Pausable,
				OneShot = true
            };
            AddChild(timer);
			timer.Timeout += () => {
				this.Modulate = new Color(255, 255, 255);
			};
		}
	}

}


