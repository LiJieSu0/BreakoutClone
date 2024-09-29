using Godot;
using System;

public partial class MonitorArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public static bool isMouseEntered=false;
	public override void _Ready()
	{
		InitializeSignal();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void InitializeNode(){
	
	}
	
	private void InitializeSignal(){
		this.MouseEntered+=()=>{
			GD.Print("MouseEntered");
			isMouseEntered=true;
		};
		this.MouseExited+=()=>{
			GD.Print("MouseExited");
			isMouseEntered=false;
		};
	}
	
	private void InitializeVariables(){
	
	}

}
