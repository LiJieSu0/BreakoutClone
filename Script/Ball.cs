using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	public float Speed = 100.0f;
	private Vector2 _dir;
    private ScoreLabel _scoreLabel;
	Player player;

    public override void _Ready(){
		// Velocity=new Vector2(0,0);


		player=GetTree().CurrentScene.GetNode<Player>("Player");
		GD.Print(player==null);
		player.ReceiveEffectEvent+=BallEffectReceived;

    }


    public override void _PhysicsProcess(double delta)
	{
		var _velocity=_dir*Speed*(float)delta;
		KinematicCollision2D collision = MoveAndCollide(_velocity);
		if(collision!=null){
			_dir = _dir.Bounce(collision.GetNormal());
			if(collision.GetCollider() is Brick brick){
				brick.OnHit();
			}
		}
		MoveAndCollide(_velocity);
		if(Input.IsActionJustPressed("ui_accept")){
			DuplicateBalls(2);
		}
	}

	// public void SetVector(Vector2 v){
	// 	Velocity=v;
	// }

	public Vector2 SetRandomDirection(){
		var newDir=new Vector2();
		int[] arr={1,-1};
		Random random=new Random();
		newDir.X=arr[random.Next(2)];
		newDir.Y=-1;
		return newDir.Normalized();
	}
	public void ShootBall(){
		_dir=SetRandomDirection();
		
	}
    private void BallEffectReceived(ItemEffect e){
		switch (e){
			case ItemEffect.IncreaseSpeed:
				this.Speed+=50f;
				GD.Print("Increase Speed");
				break;
			case ItemEffect.DecreaseSpeed:
				this.Speed-=50f;
				GD.Print("Decrease Speed");
				break;
			case ItemEffect.BallDuplicate2:
				GD.Print("ball duplicate");
				DuplicateBalls(2);
				//TODO duplicate 2 balls
				break;

			default:
				break;
		}
    }

    private void DuplicateBalls(int v){
		Ball tmp=(Ball)this.Duplicate(); //TODO set direction of the ball and set the amount of duplicates
		tmp.ShootBall();
		this.GetParent().AddChild(tmp);
    }

}
