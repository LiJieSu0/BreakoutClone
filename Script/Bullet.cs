using Godot;
using System;

public partial class Bullet : CharacterBody2D
{

	[Export]public float _speed = 5.0f;
	Vector2 _dir=new Vector2(0, 0);
	public override void _PhysicsProcess(double delta)
	{

		Vector2 velocity = Velocity;
		velocity=_dir*_speed;
		Velocity = velocity;
		KinematicCollision2D collision = MoveAndCollide(velocity);
		if(collision!=null){
			if(collision.GetCollider() is Player){
				GD.Print("Player hitted");
			}
			this.Free();
		}
	}
	public void SetDir(Vector2 dir){
		_dir=dir;
	}

}
