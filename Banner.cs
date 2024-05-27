using System;
using System.Collections.Generic;
using Godot;
public partial class Banner : Node
{
	public void showUpperBanner(string Text) {
		GetNode<Label>("/root/RootScene/GameScene/HUD/UpperBanner/UBannerLabel").Text = Text;
		GetNode<AnimationPlayer>("/root/RootScene/GameScene/HUD/UpperBanner/UpperBannerAnimation").Play("UBannerAnimation");
	}
    
}