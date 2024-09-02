using Godot;
using System;

public partial class RecordContainer : HBoxContainer
{
	Label _nameLabel;
	Label _scoreLabel;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void InitializeNode(){
		_nameLabel=GetNode<Label>("NameLabel");
		_scoreLabel=GetNode<Label>("ScoreLabel");

	}
	public void ReceiveRecord(string name,string score){
		InitializeNode();
		_nameLabel.Text=name;
		_scoreLabel.Text=score;
	}
}
