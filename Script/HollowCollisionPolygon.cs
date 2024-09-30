using Godot;
using System;

public partial class HollowCollisionPolygon : StaticBody2D{

	private bool isMouseIn=false;
	private Polygon2D polygon;
	public override void _Ready(){
			
			// 創建 CollisionPolygon2D
			CollisionPolygon2D collisionPolygon = new CollisionPolygon2D();
			AddChild(collisionPolygon);

			// polygon=GetParent().GetNode("MonitorArea").GetNode<Polygon2D>("Polygon2D");
			// Vector2[] innerPolygon=polygon.vert

			// 定義外部多邊形（較大的多邊形）
			Vector2[] outerPolygon = new Vector2[]{
				new Vector2(-100, -100),
				new Vector2(100, -100),
				new Vector2(100, 100),
				new Vector2(-100, 100),
				new Vector2(-100, -100),
			};

			// 定義內部多邊形（較小的多邊形）
			Vector2[] innerPolygon = new Vector2[]{
				new Vector2(-50, -50),
				new Vector2(50, -50),
				new Vector2(50, 50),
				new Vector2(-50, 50),
				new Vector2(-50, -50),
			};
			// 合併內外部多邊形形成中空結構
			Vector2[] hollowPolygon = CreateHollowPolygon(outerPolygon, innerPolygon);

			// 設置 CollisionPolygon2D 的多邊形
			collisionPolygon.Polygon = hollowPolygon;
		}

    public override void _Process(double delta){
		GD.Print(isMouseIn);
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
