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
			CreateCore();
			GetParent<TileMapTest>().ConfirmBtnPressed();
			GetParent().GetNode("DrawDot").GetNode<DrawLine>("DrawLine").ConfirmBtnPressed();
			//CLEAR line
		};
	}

    private void CreateCore(){
		var dots=GetParent().GetNode<DrawDotTest>("DrawDot")._dots;
		Vector2 tmp=new Vector2(0,0);
		foreach(Vector2 v in dots){
			tmp+=v;
		}
		Vector2 corePos=tmp/dots.Count;
		Core core=GetTree().CurrentScene.GetNode<Core>("Core");
		core.GlobalPosition=corePos;
		// PackedScene corePackedScene=GD.Load<PackedScene>("res://Scene/Core.tscn");
		// Core core=corePackedScene.Instantiate<Core>();
		// GetTree().CurrentScene.// core.GlobalPosition=corePos;

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
