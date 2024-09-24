using Godot;
using System;

public partial class SceneManager : Node2D
{
	#region Nodes
	[Export] PackedScene _playerScene;
	[Export] private Node2D Core;
	private Path2D path;
    private PathFollow2D pathFollow;
	private Line2D line2D;
    #endregion

    #region Variables
    [Export] private int segments=10;
    [Export] private float _raidus=100f;
	private Vector2 _centerPoint;
    #endregion

    // Called when the node enters the scene tree for the first time.

    public override void _Ready()
	{
		InitializeNode();
		InitializeVariables();


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 facingDirection = new Vector2( Mathf.Sin(pathFollow.Rotation),Mathf.Cos(pathFollow.Rotation));
	}

	private void CreateCircularPath(){
        var curve = new Curve2D();
		Vector2 origin=Vector2.Zero;
        for (int i = 0; i < segments; i++)
        {
            float angle = 2*Mathf.Pi * i / segments; 
            Vector2 point = _centerPoint+new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _raidus;
            curve.AddPoint(point);
			if(i==0)
				origin=point;
        }
		curve.AddPoint(origin);
        path.Curve = curve;
		for (int i = 0; i < curve.GetPointCount(); i++)//draw line
        {
            line2D.AddPoint(curve.GetPointPosition(i));
        }
        line2D.Width = 2;
        line2D.DefaultColor = Colors.Red;
		AddChild(line2D);
    }

	private void InitializeNode(){
	
	}
	
	private void InitializeSignal(){
	
	}
	
	private void InitializeVariables(){
		_centerPoint=Core.GlobalPosition;
		line2D = new Line2D();
		path = new Path2D();
		AddChild(path);
		CreateCircularPath();
        pathFollow = new PathFollow2D();
        path.AddChild(pathFollow);
		Player _player=_playerScene.Instantiate<Player>();
		pathFollow.AddChild(_player);
		pathFollow.ProgressRatio=0;
	}

}


