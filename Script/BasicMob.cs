using Godot;
using System;

public partial class BasicMob : Node2D
{
	#region Nodes
	Area2D _generatePreventArea;
	#endregion
	
	#region Variables
	private bool isValidInCamera=false;	
	private bool isInWall=false;
	#endregion
	public override void _Ready()
	{
		InitializeNode();
		InitializeSignal();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!isValidInCamera||isInWall){
			this.QueueFree();
			GD.Print("Mob destory");
		}
	}
	private void InitializeNode(){
		_generatePreventArea=GetNode<Area2D>("GeneratePreventArea");
	}
	
	private void InitializeSignal(){
		_generatePreventArea.AreaEntered+=OnMonitorDetected;
		_generatePreventArea.BodyEntered+=OnBodyEntered;
	}

    private void OnMonitorDetected(Area2D area){
		if(area is MonitorArea)
			isValidInCamera=true;
		if(area.Name=="GeneratePreventArea")
			isValidInCamera=false;
			
    }
	private void OnBodyEntered(Node2D body){
		if(body is StaticBody2D)
			isInWall=true;
	}

    private void InitializeVariables(){
	
	}

}
