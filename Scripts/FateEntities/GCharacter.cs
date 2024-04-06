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
		if (!sceneLoader.LoadedFateIndex.Characters.TryGetValue(CharacterName, out FateCharacter))
		{
			Logger.ERROR($"Could not get {CharacterName} from {SceneLoaderNode.Name}. Using default character.");
			FateCharacter = new DependencyProvider().GetDefaultCharacter(CharacterName);
		}
		else
		{
			Logger.INFO($"Successfully loaded {CharacterName}");
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
