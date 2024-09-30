using Godot;
using System;

public partial class Camera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public Vector2[] GetCameraConers(){
		Vector2 CameraPos=this.GlobalPosition;
		Vector2 WindowSize=GetWindow().Size;
		Vector2[] res=new Vector2[]{
			new Vector2(CameraPos.X-WindowSize.X/2,CameraPos.Y-WindowSize.Y/2),
			new Vector2(CameraPos.X+WindowSize.X/2,CameraPos.Y-WindowSize.Y/2),
			new Vector2(CameraPos.X+WindowSize.X/2,CameraPos.Y+WindowSize.Y/2),
			new Vector2(CameraPos.X-WindowSize.X/2,CameraPos.Y+WindowSize.Y/2),
		};
		return res;
	}

}
