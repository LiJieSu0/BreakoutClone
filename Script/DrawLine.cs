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
	#endregion
	public override void _Ready(){
		InitializeNode();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public override void _Draw(){
		var arr=_drawDot._dots;
		for(int i = 0; i < arr.Count-1;i++){
			DrawLine(arr[i],arr[i+1],Colors.Blue,1f);
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


}
