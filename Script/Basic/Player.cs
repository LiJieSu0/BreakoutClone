using Godot;
using System;

public partial class Player : CharacterBody2D
{
	#region Nodes

	private Ball _mainBall;
    private Node _ballManager;
	private PathFollow2D pathFollow;
	private Node2D _origin;
	private Node2D _target;
	#endregion
	
	#region Variables
	private float _raidus=100f;
	private float angle = 0f; 
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public Vector2 facingDir;
	[Export]private int segments=10;
	#endregion

	public delegate void ReceiveEffectPublisher(ItemEffect effect);
	public event ReceiveEffectPublisher ReceiveEffectEvent;
    public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitializeSignal();

	}
	public override void _PhysicsProcess(double delta)
    {
        Controller(delta);
		facingDir=(_target.GlobalPosition-_origin.GlobalPosition).Normalized();
    }

    private void Controller(double delta)
    {
		#region Moving
        if (Input.IsActionPressed("ui_left"))
        {
            pathFollow.ProgressRatio -= (float)delta;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            pathFollow.ProgressRatio += (float)delta;
        }
		#endregion

		if(Input.IsActionJustPressed("shoot_ball")){
			PackedScene packedScene=GD.Load<PackedScene>("res://Scene/Ball.tscn");
			Ball ball=(Ball)packedScene.Instantiate();
			_ballManager.AddChild(ball);
			ball.GlobalPosition=_target.GlobalPosition;
			ball.ShootBall(facingDir);
			GD.Print("Create Ball");
		}
    }

    private void InitializeNode(){
		// _mainBall=GetNode<Ball>("Ball");
		_ballManager=GetTree().CurrentScene.GetNode("BallManager");
		_origin=GetNode<Node2D>("Origin");
		_target=GetNode<Node2D>("Target");
		pathFollow=this.GetParent<PathFollow2D>();
	}
	private void InitializeSignal(){
		ReceiveEffectEvent+=PlayerReceiveEffect;
	}
	private void InitializeVariables(){
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
