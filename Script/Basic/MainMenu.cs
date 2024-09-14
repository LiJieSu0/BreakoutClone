using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	private Button _startBtn;
    private Button _quitBtn;
    private Button _highScoresBtn;


    public override void _Ready()
	{
		InitializeNode();
		InitializeSignal();
	}

	public override void _Process(double delta)
	{
	}
	private void InitializeNode(){
		_startBtn=GetNode<Button>("Control/VBoxContainer/StartBtn");
		_quitBtn=GetNode<Button>("Control/VBoxContainer/QuitBtn");
		_highScoresBtn=GetNode<Button>("Control/VBoxContainer/HighScoresBtn");
	}

	private void InitializeSignal(){
		_startBtn.Pressed+=ChangeScene;
		_quitBtn.Pressed+=QuitGame;
		_highScoresBtn.Pressed+=ShowHighScorePanel;
	}

    private void ShowHighScorePanel(){
		//TODO show high score history
    }


    private void QuitGame(){
		GetTree().Quit();
    }


    private void ChangeScene(){
		PackedScene scene=GD.Load<PackedScene>("res://Scene/MainScene.tscn");
		GetTree().ChangeSceneToPacked(scene);
    }
}
