using Godot;
using System;

public partial class Mob : CharacterBody2D
{
	#region Nodes
	private TextureProgressBar _hpBar;
	private Node2D _core;
    #endregion

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
    }
    public override void _PhysicsProcess(double delta){

	}
	private void InitializeNode(){
		_hpBar=GetNode<TextureProgressBar>("HpBar");
		_core=GetTree().CurrentScene.GetNode<Node2D>("Core");
	}
	
	private void InitializeSignal(){
		var timer=new Timer();
		timer.WaitTime=3f;
		timer.OneShot=false;
		timer.Timeout+=()=>{
			GD.Print("Mob attack");
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
			//TODO free this object	
			//TODO drop scrap after destory
		}
	}

	private void Attack(){
		PackedScene packedScene=GD.Load<PackedScene>("res://Scene/Bullet.tscn");
		Bullet bullet=(Bullet)packedScene.Instantiate();
		this.AddChild(bullet);
		bullet.GlobalPosition=GetNode<Node2D>("BulletSpawnPoint").GlobalPosition;
		bullet.SetDir((_core.GlobalPosition-bullet.GlobalPosition).Normalized());
		//TODO create bullet shoot to core
	}

}
