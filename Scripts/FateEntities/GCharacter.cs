using Fate;
using Godot;
using System;
using System.Text.Json;

public partial class GCharacter : PostSceneLoadNode
{
	/// <summary>
	/// Name (id) of the character.
	/// </summary>
	[Export]
	public string CharacterName;

	public Character FateCharacter;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		if (String.IsNullOrEmpty(CharacterName))
		{
			throw new ArgumentException($"CharacterName is required for node[{Name}]");
		}

		base._Ready();
		await _SceneLoadTask;

		SceneLoader sceneLoader = (SceneLoader) SceneLoaderNode;

		if (sceneLoader.LoadedFateIndex is null)
		{
			throw new ArgumentException(
				$"LoadedFateIndex is null in SceneLoader node. Cannot laod character [{CharacterName}]");
		}
		else if (!sceneLoader.LoadedFateIndex.Characters.TryGetValue(CharacterName, out FateCharacter))
		{
			throw new ArgumentException($"Could not get {CharacterName} from {SceneLoaderNode.Name}.");
		}
		else
		{
			Logger.INFO($"Successfully loaded {CharacterName}");
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
	}
	
}
