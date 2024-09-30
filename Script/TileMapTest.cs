using Godot;
using Godot.Collections;
using System;

public partial class TileMapTest : TileMapLayer{
	[Export] int GRID_WIDTH = 16;
    [Export] int GRID_HEIGHT = 16;
    bool finished=false;
	bool isClear=false;
	public override void _Ready(){
        QueueRedraw();
        finished=true;
	}

	public override void _Process(double delta){


	}

    public override void _Draw()
    {
        GD.Print("draw line");
		if(!isClear){
			for(int i=1;i<GetViewportRect().Size.X/16;i++){
				DrawLine(new Vector2(i*GRID_WIDTH,0),new Vector2(i*GRID_WIDTH,720),Colors.Pink,1f);
			}
			for(int i=1;i<GetViewportRect().Size.Y/16;i++){
				DrawLine(new Vector2(0,i*GRID_HEIGHT),new Vector2(1280,i*GRID_HEIGHT),Colors.Pink,1f);
			}
		}
    }

	public void ConfirmBtnPressed(){
		isClear=!isClear;
		QueueRedraw();
	}

}
