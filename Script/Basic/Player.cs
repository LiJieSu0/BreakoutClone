using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private Node2D Core;
	private Vector2 _centerPosition;
	private float _raidus=100f;
	private float angle = 0f; 
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Ball _mainBall;
    private Node _ballManager;
	public delegate void ReceiveEffectPublisher(ItemEffect effect);
	public event ReceiveEffectPublisher ReceiveEffectEvent;
    public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitializeSignal();

	}
	public override void _PhysicsProcess(double delta)
	{


	}

	private void InitializeNode(){
		// _mainBall=GetNode<Ball>("Ball");
		// _ballManager=GetParent().GetNode("BallManager");
	}
	private void InitializeSignal(){
		ReceiveEffectEvent+=PlayerReceiveEffect;
	}
	private void InitializeVariables(){
		_centerPosition=Core.GlobalPosition;
	}
    public void ReceiveEffectTrigger(ItemEffect effect){
		ReceiveEffectEvent.Invoke(effect);
	}
    
	private void PlayerReceiveEffect(ItemEffect effect){
		switch(effect){
			case ItemEffect.PlayerLengthIncrease:
				break;
			case ItemEffect.PlayerLengthDecrease:
				break;
			default:
				break;
		}
    }
	private void OrignalBreakoutCloneMove(){
		Vector2 velocity = Velocity;
		if (Input.IsActionJustPressed("ui_accept") &&GameManager.Instance.gameState==GameState.GameReady){//Space key pressed
			GameManager.Instance.gameState=GameState.GameStart;
			GameManager.Instance.isGameStarted=true;

			var originalPos=_mainBall.GlobalPosition; //Save original pos, and remove mainball from child, add to ballManager node, then set the pos and shoot.
			RemoveChild(_mainBall);
			_ballManager.AddChild(_mainBall);
			_mainBall.GlobalPosition=originalPos;
			_mainBall.ShootBall();
		}

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero){
			velocity.X = direction.X * Speed;
		}else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		this.Position=new Vector2(this.Position.X,600);
		Velocity = velocity;
		MoveAndSlide();
	}

}
