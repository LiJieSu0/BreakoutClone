using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	Button _startBtn;
    private Button _quitBtn;

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
	}

	private void InitializeSignal(){
		_startBtn.Pressed+=ChangeScene;
		_quitBtn.Pressed+=QuitGame;
	}

    private void QuitGame(){
		GetTree().Quit();
    }


    private void ChangeScene(){
		PackedScene scene=GD.Load<PackedScene>("res://Scene/MainScene.tscn");
		GetTree().ChangeSceneToPacked(scene);
    }
}
