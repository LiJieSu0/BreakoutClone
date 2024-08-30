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

		if (Input.IsActionJustPressed("ui_accept") )
		{
			// _mainBall.ShootBall();
			// GetParent().GetNode("BallManager").AddChild(_mainBall);
			//TODO shoot the ball
			
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		this.Position=new Vector2(this.Position.X,600);
		Velocity = velocity;
		MoveAndSlide();
	}

	private void InitializeNode(){
		_mainBall=GetParent().GetNode<Ball>("BallManager/Ball");
	}
}
