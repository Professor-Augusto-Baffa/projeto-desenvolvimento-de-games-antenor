using System;
using System.Collections.Generic;
using Godot;
public partial class Banner : Node
{
	public void showUpperBanner(string Text, bool bad=false, string AudioName="", double audioDelay=0, int chance=1) {
		var label = GetNode<Label>("/root/RootScene/GameScene/HUD/UpperBanner/UBannerLabel");
		if (bad == true) {
            label.LabelSettings.FontColor = new Color(255, 0, 0);
			GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/UpperBanner/badAnimation").Play("bad");
        } 
		else {
			label.LabelSettings.FontColor = new Color(255, 255, 255);
		}
		label.Text = Text;
		GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/UpperBanner/UpperBannerAnimation").Play("UBannerAnimation");

		if (AudioName != "" && GD.Randi() % chance == 0) {
			var timer = new Timer
			{
				Autostart = true,
				WaitTime = audioDelay,
				ProcessMode = ProcessModeEnum.Pausable,
				OneShot = true
			};
			AddChild(timer);
			timer.Timeout += () => {
				GetNode<AudioStreamPlayer>("/root/RootScene/GameScene/Audio/" + AudioName).Play();
			};
		}
	}
    
}