using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	public float Speed = 150.0f;
	public Vector2 dir;
	public Vector2 velocity;
    private ScoreLabel _scoreLabel;
	Player player;
	//TODO add rebound limit and goback to center
    public override void _Ready(){
		// player.ReceiveEffectEvent+=BallEffectReceived;
    }


    public override void _PhysicsProcess(double delta)
	{
		var velocity=dir*Speed*(float)delta;
		KinematicCollision2D collision = MoveAndCollide(velocity);
		if(collision!=null){
			dir = dir.Bounce(collision.GetNormal());
			if(collision.GetCollider() is Brick brick){
				brick.OnHit();
			}
		}
		MoveAndCollide(velocity);
		if(Input.IsActionJustPressed("ui_accept")){
			DuplicateBalls(4);
		}
	}


	public Vector2 SetRandomDirection(){
		var newDir=new Vector2();
		int[] arr={1,-1};
		Random random=new Random();
		newDir.X=arr[random.Next(2)];
		newDir.Y=-1;
		return newDir.Normalized();
	}
	public void ShootBall(Vector2 initDir){
		dir=initDir;
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
				break;

			default:
				break;
		}
    }

    private void DuplicateBalls(int amount){
		var half = Mathf.FloorToInt(amount / 2);
		GD.Print(half);
		for(int i=1;i<1+half;i++){
			Ball tmp = (Ball)this.Duplicate();
			float newAngleInRadians = this.dir.Angle() + Mathf.DegToRad(5*i);
			tmp.dir = new Vector2(Mathf.Cos(newAngleInRadians), Mathf.Sin(newAngleInRadians)).Normalized();
			this.GetParent().AddChild(tmp);
			tmp = (Ball)this.Duplicate();
			newAngleInRadians = this.dir.Angle() + Mathf.DegToRad(-5*i);
			tmp.dir = new Vector2(Mathf.Cos(newAngleInRadians), Mathf.Sin(newAngleInRadians)).Normalized();
			this.GetParent().AddChild(tmp);

		}

    }

}
