using Godot;
using System;

public partial class TimerLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	float time=0;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if(GameManager.Instance.gameState!=GameState.GameOver){
			time+=(float)delta;
			this.Text=time.ToString("F2");
		}
	}
}
