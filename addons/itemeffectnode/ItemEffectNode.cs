using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class ItemEffectNode : Node2D
{
	// Called when the node enters the scene tree for the first time.
	protected float _waitTime=5f;
	protected Timer _timer;
	public override void _Ready(){
		CheckCurrentParent();//determine is Item or applied effect
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){

	}

	private void CreateTimer(){
		_timer=new Timer();
		this.AddChild(_timer);
		_timer.WaitTime=_waitTime;
		_timer.Timeout+=()=>{
			this.QueueFree();
		};
	}

	private void CheckCurrentParent(){
		var parent=this.GetParent();
		switch(parent){
			case EffectItem:
				//todo wait until item is picked up by ball
				//TODO if picked up, init signal create node to player
				
				GD.Print("EffectItem");
				break;
			case Ball:
				GD.Print("Ball");
				break;
			case Player:
				GD.Print("Player");
				EffectExec();
				break;
			default:
				GD.Print("nothing");
				break;

		}
	}

	protected virtual void EffectExec(){}

	public void MoveNodeTo(Node target){
		var copy=this.Duplicate();
		target.AddChild(copy);
		this.QueueFree();
	}	

}
