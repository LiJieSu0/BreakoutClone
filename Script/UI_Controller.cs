using Godot;
using System;

public partial class UI_Controller : Node
{
	#region Nodes
	Panel _historyPanel;
	#endregion
	
	#region Variables

	#endregion
	public override void _Ready(){
		InitializeNode();
		InitializeSignal();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if(Input.IsActionJustPressed("show_menu")){
			_historyPanel.Visible=!_historyPanel.Visible;
			Engine.TimeScale=_historyPanel.Visible?0:1;
		}
	}
	
	private void InitializeNode(){
		_historyPanel=GetParent().GetNode<Panel>("HUD/HistoryScore");
	}
	
	private void InitializeSignal(){
	
	}
	
	private void InitializeVariables(){
	
	}
}
