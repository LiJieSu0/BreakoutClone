using Godot;
using System;

public partial class Brick : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	ScoreLabel _scoreLabel;
	public override void _Ready(){
	}

    private void OnHit(Node2D body){
		this.QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

}
