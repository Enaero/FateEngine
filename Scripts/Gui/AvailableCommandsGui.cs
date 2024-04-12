using Fate;
using Godot;
using System;
using System.Collections.Generic;

public partial class AvailableCommandsGui : ItemList
{
	public Node SceneLoaderNode;
	private CombatGui _CombatGui;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		_CombatGui = GetParent() as CombatGui;
		SceneLoaderNode = _CombatGui.SceneLoaderNode;
		ItemClicked += _WhenCommandChosen;
	}

	private void _WhenCommandChosen(long index, Vector2 atPosition, long mouseButtonIndex)
	{
		GD.Print("_WhenCommandChosen", index, atPosition, mouseButtonIndex);
		string chosenCommandName = GetItemText((int) index);

		if (GetActionIndex().Attacks.ContainsKey(chosenCommandName))
		{
			_CombatGui.EmitSignal(CombatGui.SignalName.AttackChosen, chosenCommandName);
		}
	}

	private ActionIndex GetActionIndex()
	{
		return (SceneLoaderNode as SceneLoader).LoadedActionIndex;
	}

	private Character GetCharacter(string name)
	{
		return (SceneLoaderNode as SceneLoader).LoadedFateIndex.Characters[name];
	}
}
