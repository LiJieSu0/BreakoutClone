using Godot;
using Godot.Collections;
using System;

public partial class DrawDotTest : Node2D
{
	// Called when the node enters the scene tree for the first time.
	TileMapLayer tileMapLayer;
    public Godot.Collections.Array<Vector2> _dots=new Array<Vector2>();

	public override void _Ready(){
		InitializeNode();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	


	public override void _Input(InputEvent @event){
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex==MouseButton.Left)
        {
            Vector2 globalMousePosition = GetGlobalMousePosition();
            GD.Print("Mouse clicked at: " + globalMousePosition);
			Vector2 coordinate=tileMapLayer.LocalToMap(globalMousePosition);
			Vector2 tmpDotPos=new Vector2(coordinate.X*16+8, coordinate.Y*16+8);
			if(_dots.Contains(tmpDotPos)){
				GD.Print("already contain");
				return;
			}
            AddPoint(tmpDotPos);
        }
    }
	private void InitializeNode(){
		tileMapLayer=GetParent<TileMapLayer>();
	}
	
	private void InitializeSignal(){
	
	}
	
	private void InitializeVariables(){
	
	}
	private void AddPoint(Vector2 position){
        _dots.Add(position);
        QueueRedraw();
		GetNode<DrawLine>("DrawLine").UpdateLine();
		//CALL draw line
    }
    public override void _Draw()
    {
		foreach (Vector2 dot in _dots){
            DrawCircle(dot, 5f, Colors.Green); 
		}
    }
}
