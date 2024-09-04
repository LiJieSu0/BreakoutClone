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
		_scoreLabel=GetTree().CurrentScene.GetNode<ScoreLabel>("ScoreLabel");
		player=GetNode<Player>("%Player");
		player.ReceiveEffectEvent+=BallEffectReceived;
		// ShootBall();

    }


    public override void _PhysicsProcess(double delta)
	{
		var _velocity=_dir*Speed*(float)delta;
		KinematicCollision2D collision = MoveAndCollide(_velocity);
		if(collision!=null){
			_dir = _dir.Bounce(collision.GetNormal());
			if(collision.GetCollider() is Brick brick){
				brick.OnHit();
				_scoreLabel.AddScore();
			}
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
				//TODO duplicate 2 balls
				break;

			default:
				break;
		}
    }


}
