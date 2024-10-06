using Godot;
using System;

public partial class MobManager : Node
{
	#region Nodes
	private CollisionPolygon2D _generateArea;
	private Camera _camera;
	#endregion
	
	#region Variables
	
	#endregion

	public override void _Ready(){
		InitializeNode();
		InitializeSignal();
		InitializeVariables();
		//TODO create mob after core created
		//TODO change the time create mob when the mining start, the mob should genereate time by time 
		// for(int i=0;i<20;i++)
		// 	GenerateMob();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void InitializeNode(){
		_camera=GetTree().CurrentScene.GetNode<Camera>("Camera");
		_generateArea=GetTree().CurrentScene.GetNode("Camera").GetNode("MonitorArea").GetNode<CollisionPolygon2D>("Polygon2D");
	}
	
	private void InitializeSignal(){
	
	}
	
	private void InitializeVariables(){
	
	}

	private void GenerateMob(){
		Vector2[] cameraRange=_camera.GetCameraConers();
		float xMin=cameraRange[0].X;
		float xMax=cameraRange[1].X;
		float yMin=cameraRange[0].Y;
		float yMax=cameraRange[2].Y;
		float randomX = GD.Randf() * (xMax - xMin) + xMin;
		float randomY = GD.Randf() * (yMax - yMin) + yMin;
        Vector2 generatePos=new Vector2(randomX, randomY);
		PackedScene packedMob=GD.Load<PackedScene>("res://Scene/BasicMob.tscn");
		BasicMob mob=(BasicMob)packedMob.Instantiate();
		this.AddChild(mob);
		mob.GlobalPosition=generatePos;
		mob.GetNode<Mob>("CharacterBody2D").SetFacingDir();
	}
}
