using System;
using System.Collections.Generic;
using Godot;
public partial class Banner : Node
{
	public void showUpperBanner(string Text, bool bad=false) {
		var label = GetNode<Label>("/root/RootScene/GameScene/HUD/UpperBanner/UBannerLabel");
		if (bad == true) {
            label.LabelSettings.FontColor = new Color(255, 50, 50);
        } 
		else {
			label.LabelSettings.FontColor = new Color(255, 255, 255);
		}
		label.Text = Text;
		GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/UpperBanner/UpperBannerAnimation").Play("UBannerAnimation");
	}
    
}