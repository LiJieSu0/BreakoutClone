using Godot;
using System;

public partial class Brick : StaticBody2D
{
	Node _specialItemManager;
	ScoreLabel _scoreLabel;
	public override void _Ready(){
		_specialItemManager=GetTree().CurrentScene.GetNode("SpecialItemManager");
	}

    public void OnHit(){
		//TODO create item
		PackedScene packedScene=GD.Load<PackedScene>("res://Scene/SpecialItem.tscn");
		SpecialItem item=(SpecialItem)packedScene.Instantiate();
		_specialItemManager.AddChild(item);
		item.GlobalPosition=this.GlobalPosition;
		this.QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta){
	}

}
