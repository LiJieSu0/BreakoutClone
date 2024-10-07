using Godot;
using System;
using System.Collections.Generic;

public partial class EffectItem : Node2D
{
	public Area2D _pickUpArea;
	public Node _effectListNode;
	public override void _Ready()
	{
		InitializeNode();
		InitializeSignal();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void InitializeNode()
    {
        _pickUpArea = GetNode<Area2D>("Area2D");
        _effectListNode = GetNode<Node>("EffectListNode");
        CreateErffectNode();

    }

    private void CreateErffectNode()
    {
        AttackEffectItem atk = new AttackEffectItem();
        _effectListNode.AddChild(atk);
		//TODO randonize the effect or determie by mob
    }

    private void InitializeSignal(){
		_pickUpArea.BodyEntered+=(Node2D body)=>{
			if(body is Ball ball){
				foreach(ItemEffectNode n in _effectListNode.GetChildren()){
					n.MoveNodeTo(ball.GetEffeListNode());
				}
			}
		};
	}
	
	private void InitializeVariables(){
	
	}

}
