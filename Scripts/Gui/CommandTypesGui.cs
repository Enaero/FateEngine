using Fate;
using Godot;
using System;
using System.Collections;
using System.Linq;

public partial class CommandTypesGui : TabBar
{	
	[Export]
	public Node AvailableCommandsNode;
	public Node SceneLoaderNode;
	public const string ATTACK_TITLE = "Attack";
	public const string CREATE_ADV_TITLE = "Create an Advantage";
	public const string DEFEND_TITLE = "Defend";
	public const string OVERCOME_TITLE = "Overcome an Obstacle";

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		if (AvailableCommandsNode is null)
		{
			throw new NullReferenceException("AvailableCommandsNode is null");
		}

		SceneLoaderNode = (GetParent() as CombatGui).SceneLoaderNode;

		if (!GetSceneLoader().isFinishedLoading) {
			await ToSignal(SceneLoaderNode, SceneLoader.SignalName.FateSceneLoaded);
		}
		GD.Print("Loaded CommandTypesGui");

		CurrentTab = 0;
		_PopulateActionList(0);

		TabChanged += _PopulateActionList;
	}

	private void _PopulateActionList(long tabIndex)
	{
		string tabTitle = GetTabTitle((int) tabIndex);
		GetActionsItemList().Clear();

		if (tabTitle == ATTACK_TITLE)
		{
			IEnumerable actions = GetActionIndex().Attacks
				.Values
				.OrderBy((item) => item.Name);

			foreach(AttackInfo attackInfo in actions)
			{
				GetActionsItemList().AddItem(attackInfo.Name);
			}
		}
		else if (tabTitle == DEFEND_TITLE)
		{
			GD.Print(GetActionIndex().Defenses);
		}
	}

	private SceneLoader GetSceneLoader()
	{
		return SceneLoaderNode as SceneLoader;
	}

	private ActionIndex GetActionIndex()
	{
		return GetSceneLoader().LoadedActionIndex;
	}

	private ItemList GetActionsItemList()
	{
		return AvailableCommandsNode as ItemList;
	}
}
