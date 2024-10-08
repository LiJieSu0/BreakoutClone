using Godot;
using System;

public partial class Core : Sprite2D{
	#region Nodes
	private Area2D _coreArea;
	#endregion
	
	#region Variables
	
	#endregion

	//TODO create mine system
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
		if(body is Ball ball){
			if(ball._bounceLimit<=0){
				ball.Free();
			}
			GD.Print("Ball entered");
		}
		if(body is Bullet){
			GD.Print("Core hitted");
		}

    }

    private void InitializeVariables(){
	
	}
}
