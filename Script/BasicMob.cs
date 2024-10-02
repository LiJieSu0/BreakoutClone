using Godot;
using System;

public partial class BasicMob : Node2D
{
	#region Nodes
	Area2D _generatePreventArea;
	#endregion
	
	#region Variables
	private bool isInMonitor=false;	
	#endregion
	public override void _Ready()
	{
		InitializeNode();
		InitializeSignal();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!isInMonitor){
			this.QueueFree();
			GD.Print("Mob destory");
		}
	}
	private void InitializeNode(){
		_generatePreventArea=GetNode<Area2D>("GeneratePreventArea");
	}
	
	private void InitializeSignal(){
		_generatePreventArea.AreaEntered+=OnMonitorDetected;
	}

    private void OnMonitorDetected(Area2D area){
		isInMonitor=true;
		GD.Print("is in monitor "+ isInMonitor);
    }

    private void InitializeVariables(){
	
	}

}
