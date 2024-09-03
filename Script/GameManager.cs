using Godot;
using System;

public enum GameState{
	GameReady,
	GameStart,
	GameStop,
	GameOver,
}
public partial class GameManager : Node2D{
	public static GameManager Instance { get; private set;}	
	private Node BallManager;
	private Node BrickManager;

	private Area2D OutArea;
	public GameState gameState;
	public delegate void GameOverPublisher();
	public event GameOverPublisher gameOverEvent;
	public bool isGameStarted=false;

    public override void _EnterTree(){
    	Instance=this;
		Engine.TimeScale=1;
    }
    public override void _Ready()
	{
		InitializeNode();
		OutArea.BodyEntered+=RemoveBall;
		gameState=GameState.GameReady;
	}

    private void RemoveBall(Node2D body){
		body.QueueFree();
    }

    public override void _Process(double delta)
	{
		GD.Print("running");
		if((isGameStarted&&BallManager.GetChildCount()==0) ||BrickManager.GetChildCount()==0){ //TODO add if brick equal to zero
			gameState=GameState.GameOver;
		}
		if(gameState==GameState.GameOver){
			Engine.TimeScale=0;
			GameOverTrigger();
		}

	}

	private void InitializeNode(){
		BallManager=GetNode("BallManager");
		BrickManager=GetNode("BrickManager");
		OutArea=GetNode<Area2D>("OutArea");
	}
	private void GameOverTrigger(){
		gameOverEvent.Invoke();
	}

}
