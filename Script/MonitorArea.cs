using Godot;
using System;
using System.Collections.Generic;
public partial class MonitorArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public static bool isMouseEntered=false;
	private CollisionPolygon2D monitorArea;
	public override void _Ready()
	{
		InitializeSignal();
		monitorArea=GetNode<CollisionPolygon2D>("Polygon2D");
		StaticBody2D staticBody=new StaticBody2D();
		this.AddChild(staticBody);
		CollisionPolygon2D collisionPolygon = new CollisionPolygon2D();
		staticBody.AddChild(collisionPolygon);
		Vector2[] originalArr=monitorArea.Polygon;
		GD.Print(originalArr.Length);
        List<Vector2> innerArr = new List<Vector2>(originalArr)
        {
            originalArr[0]
        };
		GD.Print(innerArr.Count);


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
    private Vector2[] CreateHollowPolygon(Vector2[] outerPolygon, Vector2[] innerPolygon){
			// 將外部多邊形和內部多邊形合併
			Vector2[] hollowPolygon = new Vector2[outerPolygon.Length + innerPolygon.Length];
			
			// 將外部多邊形的點複製到新陣列
			outerPolygon.CopyTo(hollowPolygon, 0);

			// 將內部多邊形的點按逆時針順序複製到新陣列
			for (int i = 0; i < innerPolygon.Length; i++)
			{
				hollowPolygon[outerPolygon.Length + i] = innerPolygon[innerPolygon.Length - 1 - i];
			}

			return hollowPolygon;
	}


}
