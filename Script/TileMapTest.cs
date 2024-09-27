using Godot;
using System;

public partial class TileMapTest : TileMapLayer{
	public override void _Ready(){
		int gridWidth = 16;
        int gridHeight = 16;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                SetCell(new Vector2I(x,y), 1);
            }
        }

	}

	public override void _Process(double delta){


	}
	public override void _Input(InputEvent @event){
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            Vector2 globalMousePosition = GetGlobalMousePosition();
            GD.Print("Mouse clicked at: " + globalMousePosition);
			GD.Print(this.LocalToMap(globalMousePosition)); 
        }
    }

}
