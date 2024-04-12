using Fate;
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class CombatGui : Node
{
	private const string ALLIED_INFO_NAME = "AlliedInfo";
	private const string ENEMY_INFO_NAME = "EnemyInfo";

	[Signal]
	public delegate void AttackChosenEventHandler(string chosenAttack);

	[Export]
	public Node SceneLoaderNode;

	[Export]
	public Node Camera3DNode;
	
	[Export]
	public string[] Allies;
	
	[Export]
	public string[] Enemies;

	public string SelectedAlly;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Camera3DNode is null)
		{
			GD.PushError("Camera is needed for combat gui");
			throw new ArgumentException("CombatGui needs a Camera");
		}
		if (SceneLoaderNode is null)
		{
			GD.PushError("CombatGui needs a SceneLoaderNode connected");
			throw new ArgumentException("CombatGui needs a SceneLoaderNode connected");
		}
		
		if (Allies is null || Enemies is null)
		{
			GD.PushError("Allies and/or Enemies is null, please set them");
			throw new ArgumentException("Allies / Enemies is null");
		}

		SelectedAlly = "";
		_HideAlliedInfo();
		_HideEnemyInfo();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (!Input.IsActionJustPressed(InputOps.POINTER_SELECT))
		{
			return;
		}

		Node clickedNode = InputOps.GetClickedNode3D(Camera3DNode as Camera3D, 20f);
		
		if (clickedNode is null)
		{
			_HideAlliedInfo();
			_HideEnemyInfo();
			return;
		}

		GCharacter clickedCharacter = clickedNode.GetSelfOrParentByType<GCharacter>();
		if (clickedCharacter is not null)
		{
			string name = clickedCharacter.Name;

			if (Allies.Contains(name))
			{
				_ShowAllyInfo(name);
				_HideEnemyInfo();
			}
			else if (Enemies.Contains(name))
			{
				_ShowEnemyInfo(name);
				_HideAlliedInfo();
			}
		}
	}

	private void _HideAlliedInfo()
	{
		this.GetChildByName<CanvasItem>(ALLIED_INFO_NAME).Hide();
	}

	private void _ShowAllyInfo(string allyName)
	{
		_ShowCharacterInfo(allyName, ALLIED_INFO_NAME);
	}

	private void _ShowCharacterInfo(string characterName, string guiName)
	{
		CanvasItem infoPanel = this.GetChildByName<CanvasItem>(guiName);

		SceneIndex characterIndex = GetSceneLoader().LoadedFateIndex;
		if (!characterIndex.Characters.TryGetValue(characterName, out Character character))
		{
			GD.PushError($"Cannot find character {characterName} in character index");
			return;
		}
		infoPanel.GetChildByName<RichTextLabel>("NameLabel").Text = character.DisplayName;
		infoPanel.GetChildByName<RichTextLabel>("StressLabel").Text = $"Physical Stress: {character.PhysicalStress.Current}" +
			$" | Max: {character.PhysicalStress.Capacity}\n" +
			$"Mental Stress: {character.MentalStress.Current} | Max: {character.MentalStress.Capacity}";
		infoPanel.Show();
	}

	private void _HideEnemyInfo()
	{
		this.GetChildByName<CanvasItem>(ENEMY_INFO_NAME).Hide();
	}

	private void _ShowEnemyInfo(string enemyName)
	{
		_ShowCharacterInfo(enemyName, ENEMY_INFO_NAME);
	}

	private SceneLoader GetSceneLoader()
	{
		return (SceneLoader) SceneLoaderNode;
	}
}
