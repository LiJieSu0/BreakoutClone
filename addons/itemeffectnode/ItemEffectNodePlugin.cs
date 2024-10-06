#if TOOLS
using Godot;
using System;

[Tool]
public partial class ItemEffectNodePlugin : EditorPlugin
{
	public override void _EnterTree()
	{
		var script = GD.Load<Script>("res://addons/itemeffectnode/ItemEffectNode.cs");
        var texture = GD.Load<Texture2D>("res://icon.svg");
		AddCustomType("ItemEffectNodeType","Node2D", script, texture);
		// Initialization of the plugin goes here.
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("ItemEffectNodeType");
	}
}
#endif
