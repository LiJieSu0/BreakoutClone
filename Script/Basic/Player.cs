using Godot;
using System;

public partial class Player : CharacterBody2D
{
	#region Nodes
	[Export] private Node2D Core;
	private Path2D path;
    private PathFollow2D pathFollow;
	private Ball _mainBall;
    private Node _ballManager;
	#endregion
	
	#region Variables
	private Vector2 _centerPosition;
	private float _raidus=100f;
	private float angle = 0f; 
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	[Export]private int segments=10;
	#endregion

	public delegate void ReceiveEffectPublisher(ItemEffect effect);
	public event ReceiveEffectPublisher ReceiveEffectEvent;
    public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitializeSignal();
        path = new Path2D();
		Core.AddChild(path);

        // 創建 PathFollow2D 節點
        pathFollow = new PathFollow2D();
        path.AddChild(pathFollow);

        // 創建一個圓形路徑
        CreateCircularPath();
		pathFollow.AddChild(this);

	}
	public override void _PhysicsProcess(double delta)
	{
		if(Input.IsActionJustPressed("ui_left")){
			pathFollow.ProgressRatio-=(float)delta;
		}
		if(Input.IsActionJustPressed("ui_right")){
			pathFollow.ProgressRatio+=(float)delta;
		}
	}
    private void CreateCircularPath()
    {
        var curve = new Curve2D();
        for (int i = 0; i < segments; i++)
        {
            float angle = 2*Mathf.Pi * i / segments; 
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _raidus;
            curve.AddPoint(point);
        }
        path.Curve = curve;
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

	private void ReparentNode(Node2D node, Node2D newParent)
    {
        // 確認節點當前的父節點
        Node currentParent = node.GetParent();

        if (currentParent != null)
        {
            // 從當前父節點中移除
            currentParent.RemoveChild(node);
        }

        // 添加到新的父節點
        newParent.AddChild(node);

        // 如果需要，可以設置新的變換（如位置）
        node.GlobalPosition = newParent.GlobalPosition;
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
