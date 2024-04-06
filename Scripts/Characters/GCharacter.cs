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

	/// <summary>
	/// A reference to the SceneLoader node to access loaded Fate objects.
	/// </summary>
	[Export]
	public SceneLoader SceneLoaderRef;


	public Character LoadedCharacter;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		if (String.IsNullOrEmpty(CharacterName))
		{
			throw new ArgumentException($"CharacterName is required for node[{Name}]");
		}
		if (SceneLoaderRef is null)
		{
			throw new ArgumentException($"SceneLoader reference is required for node[{Name}]");
		}

		base._Ready();
		await _SceneLoadTask;

		if (!SceneLoaderRef.LoadedFateIndex.Characters.TryGetValue(CharacterName, out LoadedCharacter))
		{
			throw new ArgumentException($"Could not get {CharacterName} from {SceneLoaderRef.Name}");
		}

		Logger.INFO($"Successfully loaded {CharacterName}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
