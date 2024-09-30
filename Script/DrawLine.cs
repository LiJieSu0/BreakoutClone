using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class DrawLine : Node2D
{
	// Called when the node enters the scene tree for the first time.
	#region Nodes
	private DrawDotTest _drawDot;
	#endregion
	
	#region Variables
	private bool isConfirmed=false;
	#endregion
	public override void _Ready(){
		InitializeNode();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public override void _Draw(){
		if(!isConfirmed){
			var arr=_drawDot._dots;
			for(int i = 0; i < arr.Count-1;i++){
				DrawLine(arr[i],arr[i+1],Colors.Blue,1f);
			}
		}
		else{
			var arr=_drawDot._dots;
			for(int i = 0; i < arr.Count-1;i++){
				DrawLine(arr[i],arr[i+1],Colors.Red,1f);
			}
			DrawLine(arr[arr.Count-1],arr[0],Colors.Red,1f);
		}
    }

	private void InitializeNode(){
		_drawDot=GetParent<DrawDotTest>();
	}
	
	private void InitializeSignal(){
	
	}
	
	private void InitializeVariables(){
	
	}
	public void UpdateLine(){
		QueueRedraw();
	}
	public void ConfirmBtnPressed(){
		GD.Print("Drawline confirmed");
		isConfirmed=true;
		QueueRedraw();
	}

}
