#if TOOLS
using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class ItemEffectNodePlugin : EditorPlugin
{
	List<string> typeNameList=new List<string>();
	public override void _EnterTree(){
		CreateNode("AttackEffectItem","res://addons/itemeffectnode/AttackEffectItem.cs","res://icon.svg");
	}

	public override void _ExitTree(){
		foreach(var s in typeNameList){
			RemoveCustomType(s);
		}
	}

	private void CreateNode(string nodeName,string scriptPath,string iconPath,string baseType="Node2D"){
		var script = GD.Load<Script>(scriptPath);
        var texture = GD.Load<Texture2D>(iconPath);
		AddCustomType(nodeName,baseType, script, texture);
		typeNameList.Add(nodeName);
	}


}
#endif
