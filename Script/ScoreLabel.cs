using Godot;
using System;

public partial class ScoreLabel : Label
{
	int score=0;
	public override void _Ready()
	{
		this.Text="Score: "+score.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		this.Text="Score: "+score.ToString();
	}
	public void AddScore(int value=1){
		this.score+=value;
	}
}
