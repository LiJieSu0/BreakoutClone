using Godot;
using Godot.Collections;
using System;

public partial class TileMapTest : TileMapLayer{

    bool finished=false;
	public override void _Ready(){
		int gridWidth = 16;
        int gridHeight = 16;
        GD.Print(GetViewportRect().Size);
        QueueRedraw();
        finished=true;
	}

	public override void _Process(double delta){


	}

    public override void _Draw()
    {
        GD.Print("draw line");
        for(int i=1;i<GetViewportRect().Size.X/16;i++){
            DrawLine(new Vector2(i*16,0),new Vector2(i*16,720),Colors.Pink,1f);
        }
        for(int i=1;i<GetViewportRect().Size.Y/16;i++){
            DrawLine(new Vector2(0,i*16),new Vector2(1280,i*16),Colors.Pink,1f);
        }
    }

}
