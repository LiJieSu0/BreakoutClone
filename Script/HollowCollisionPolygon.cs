using Godot;
using System;
using System.Collections.Generic;

public partial class HollowCollisionPolygon : StaticBody2D{

	private bool isMouseIn=false;
	private Polygon2D polygon;
	private Camera camera;
	private MonitorCollision monitorAreaCollision;

	public override void _Ready(){
			camera=GetParent().GetNode<Camera>("Camera");
			// 創建 CollisionPolygon2D
			CollisionPolygon2D collisionPolygon = new CollisionPolygon2D();
			AddChild(collisionPolygon);
			monitorAreaCollision=camera.GetNode("MonitorArea").GetNode<MonitorCollision>("Polygon2D");
			Vector2[] originalArr=monitorAreaCollision.polygonPoints;

			List<Vector2> innerList = new List<Vector2>(originalArr){
				originalArr[0]
			};
			Vector2[] innerArr = innerList.ToArray();
			List<Vector2>outerList = new List<Vector2>(camera.GetCameraConers()){
				camera.GetCameraConers()[0]
			};
			Vector2[] outerArr=outerList.ToArray();
			// 合併內外部多邊形形成中空結構
			Vector2[] hollowPolygon =CreateHollowPolygon(outerArr,innerArr);

			// 設置 CollisionPolygon2D 的多邊形
			collisionPolygon.Polygon = hollowPolygon;
		}

    public override void _Process(double delta){
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
