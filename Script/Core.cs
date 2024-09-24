using Godot;
using System;

public partial class Core : Sprite2D{
	#region Nodes
	private Area2D _coreArea;
	#endregion
	
	#region Variables
	
	#endregion
	public override void _Ready(){
		InitializeNode();
		InitializeSignal();
	}

	public override void _Process(double delta){

	}
	private void InitializeNode(){
		_coreArea=GetNode<Area2D>("Area2D");
	}
	
	private void InitializeSignal(){
		_coreArea.BodyEntered+=OnBallentered;
	}

    private void OnBallentered(Node2D body){
		if(body is Ball){
			GD.Print("Ball entered");
		}

    }

    private void InitializeVariables(){
	
	}
}
