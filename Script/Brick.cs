using Godot;
using System;

public partial class Brick : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// this.BodyEntered+=OnHit;
	}

    private void OnHit(Node2D body){
		this.QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

}
