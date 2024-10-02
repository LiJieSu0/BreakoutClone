using Godot;
using System;

public partial class Mob : CharacterBody2D
{
	#region Nodes
	private TextureProgressBar _hpBar;
	private Node2D _bulletSpawnPoint;
	private Node2D _core;
    #endregion
	[Export] float _attackTime=3f;
    #region Variables
	[Export] private int _maxHp=100;
	private int _currHp;
    #endregion
	//TODO make mob facint core
	//TODO make hp bar don't rotate with mob

    public override void _Ready(){
		InitializeNode();
		InitializeVariables();
		InitializeSignal();

		Vector2 dir=_bulletSpawnPoint.GlobalPosition-this.GlobalPosition; //set the mob facing core
		Vector2 originToCore=_core.GlobalPosition-this.GlobalPosition;
		float angle= Mathf.RadToDeg(dir.AngleTo(originToCore)); 
		this.Rotation=dir.AngleTo(originToCore);
    }
    public override void _PhysicsProcess(double delta){

	}
	private void InitializeNode(){
		_hpBar=GetParent().GetNode<TextureProgressBar>("HpBar");
		_bulletSpawnPoint=GetNode<Node2D>("BulletSpawnPoint");
		_core=GetTree().CurrentScene.GetNode<Node2D>("Core");
	}
	
	private void InitializeSignal(){
        AttackTimerSignal();
    }

    private void AttackTimerSignal(){
        var timer = new Timer();
        timer.WaitTime = _attackTime;
        timer.OneShot = false;
        timer.Timeout += () =>
        {
            Attack();
        };
        this.AddChild(timer);
        timer.Start();
    }

    private void InitializeVariables(){
		_currHp=_maxHp;
		_hpBar.MaxValue=_maxHp;
		_hpBar.Value=_currHp;
	}

	public void ReceiveDmg(int dmg){
		this._currHp-=dmg;
		_hpBar.Value=_currHp;
		if(_currHp<=0){
			this.QueueFree();
			//TODO drop scrap after destory
		}
	}

	private void Attack(){
		PackedScene packedScene=GD.Load<PackedScene>("res://Scene/Bullet.tscn");
		Bullet bullet=(Bullet)packedScene.Instantiate();
		this.AddChild(bullet);
		bullet.GlobalPosition=_bulletSpawnPoint.GlobalPosition;
		bullet.SetDir((_core.GlobalPosition-bullet.GlobalPosition).Normalized());
	}

}
