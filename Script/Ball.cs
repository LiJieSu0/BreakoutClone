using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -400.0f;
	private Vector2 _dir;

    public override void _Ready(){
		// Velocity=new Vector2(0,0);
		ShootBall();

    }
    public override void _PhysicsProcess(double delta)
	{
		var _velocity=_dir*Speed*(float)delta;
		KinematicCollision2D collision = MoveAndCollide(_velocity);
		if(collision!=null){
			_dir = _dir.Bounce(collision.GetNormal());
			if(collision.GetCollider() is Brick brick)
			brick.QueueFree();
		}
		MoveAndCollide(_velocity);
	}

	public void SetVector(Vector2 v){
		Velocity=v;
	}

	public Vector2 SetRandomDirection(){
		var newDir=new Vector2();
		int[] arr={1,-1};
		Random random=new Random();
		newDir.X=arr[random.Next(2)];
		// float minValue = -1f;
		// float maxValue = 1f;
		// float randomFloatInRange = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
		newDir.Y=-1;
		return newDir.Normalized();
	}
	public void ShootBall(){
		_dir=SetRandomDirection();
		
	}

}
