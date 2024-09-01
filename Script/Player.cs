using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Ball _mainBall;

	public override void _Ready(){
		InitializeNode();

	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		if (Input.IsActionJustPressed("ui_accept") ) //Space key pressed
		{
			GameManager.Instance.gameState=GameState.GameStart;
			GameManager.Instance.isGameStarted=true;

			var originalPos=_mainBall.GlobalPosition; //Save original pos, and remove mainball from child, add to ballManager node, then set the pos and shoot.
			RemoveChild(_mainBall);
			GetParent().GetNode("BallManager").AddChild(_mainBall);
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

	private void InitializeNode(){
		_mainBall=GetNode<Ball>("Ball");
	}
}
