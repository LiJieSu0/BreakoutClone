using Godot;
using System;

public partial class AttackEffectItem : ItemEffectNode
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Attack item node");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
