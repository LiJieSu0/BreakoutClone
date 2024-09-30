using Godot;
using System;

public partial class MonitorCollision : CollisionPolygon2D
{
	public Vector2[] polygonPoints;
	public override void _Ready(){
		Vector2[] localPoints = this.Polygon;
		Vector2[] globalPositions = new Vector2[localPoints.Length];
		GD.Print(this.GlobalPosition);
		for (int i = 0; i < localPoints.Length; i++)
        {
            globalPositions[i] =new Vector2(localPoints[i].X+this.GlobalPosition.X,localPoints[i].Y+this.GlobalPosition.Y);
        }

		polygonPoints=globalPositions;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
