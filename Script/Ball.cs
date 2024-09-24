using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	#region Nodes
    private ScoreLabel _scoreLabel;
	#endregion
	
	#region Variables
	private Vector2 _corePosition;
	public int _bounceLimit=3;
	public float Speed = 150.0f;
	public Vector2 dir;
	public Vector2 velocity;
	#endregion
	//TODO add rebound limit and goback to center
    public override void _Ready(){
		InitializeVariables();
		// player.ReceiveEffectEvent+=BallEffectReceived;
    }


    public override void _PhysicsProcess(double delta)
	{
		var velocity=dir*Speed*(float)delta;
		KinematicCollision2D collision = MoveAndCollide(velocity);
		if(collision!=null){
			_bounceLimit--;
			GD.Print("Bounce times "+_bounceLimit);
			dir = dir.Bounce(collision.GetNormal());
			if(collision.GetCollider() is Player){
				_bounceLimit=3;
			}
			if(collision.GetCollider() is Brick brick){
				brick.OnHit();
			}
			if(_bounceLimit==0){
				//TODO change state
				dir=(_corePosition-this.GlobalPosition).Normalized();
				GD.Print(" back to core "+dir);
			}
		}
		MoveAndCollide(velocity);
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

	private void InitializeNode(){
		
	}
	
	private void InitializeSignal(){
	
	}
	
	private void InitializeVariables(){
		_corePosition=GetTree().CurrentScene.GetNode<Node2D>("Core").GlobalPosition;
	}

}
