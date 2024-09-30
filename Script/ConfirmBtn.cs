using Godot;
using System;

public partial class ConfirmBtn : Button
{
	// Called when the node enters the scene tree for the first time.
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
		this.Pressed+=()=>{
			CreatePath();
			GetParent<TileMapTest>().ConfirmBtnPressed();
			GetParent().GetNode("DrawDot").GetNode<DrawLine>("DrawLine").ConfirmBtnPressed();
			//CLEAR line
		};
	}
	
	private void InitializeVariables(){
	
	}
	private void CreatePath(){
		var curve = new Curve2D();
			var dots=GetParent().GetNode<DrawDotTest>("DrawDot")._dots;
			foreach(Vector2 v in dots){
				curve.AddPoint(v);
			}
			curve.AddPoint(dots[0]);
			Path2D path=new Path2D();
			path.Curve=curve;
			GetParent().AddChild(path);
			PathFollow2D pathFollow = new PathFollow2D();
			path.AddChild(pathFollow);
			PackedScene packedScene=GD.Load<PackedScene>("res://Scene/Player.tscn");
			Player _player=packedScene.Instantiate<Player>();
			pathFollow.AddChild(_player);
			pathFollow.ProgressRatio=0;
	}

}
