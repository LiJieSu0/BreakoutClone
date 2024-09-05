using Godot;
using System;

public partial class Brick : StaticBody2D
{
	Node _specialItemManager;
	ScoreLabel _scoreLabel;
	public override void _Ready(){
		_specialItemManager=GetTree().CurrentScene.GetNode("SpecialItemManager");
		_scoreLabel=GetTree().CurrentScene.GetNode<ScoreLabel>("ScoreLabel");
	}

    public void OnHit(){
		PackedScene packedScene=GD.Load<PackedScene>("res://Scene/SpecialItem.tscn");
		SpecialItem item=(SpecialItem)packedScene.Instantiate();
		_specialItemManager.AddChild(item);
		item.GlobalPosition=this.GlobalPosition;
		_scoreLabel.AddScore();
		this.QueueFree();
    }


}
