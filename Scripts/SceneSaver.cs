using Godot;
using System;
using System.Reflection;
using System.Text.Json;

public partial class SceneSaver : Node
{
	[Export]
	public string SceneName;

	private Fate.DependencyProvider _DependencyProvider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (String.IsNullOrEmpty(SceneName))
		{
			throw new ArgumentException("SceneName needs to be defined for SceneSaver");
		}
		_DependencyProvider = new Fate.DependencyProvider();

		Button saveSceneButton = new()
		{
			Text = "Save",
			Position = GetViewport().GetWindow().Size / 2
		};
		saveSceneButton.Connect(Button.SignalName.Pressed, Callable.From(_SaveSceneToJson));

		AddChild(saveSceneButton);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _SaveSceneToJson()
	{
		Fate.Logger.INFO("Saving scene...");
		Godot.Collections.Array<Node> nodesToProcess = GetTree().Root.GetChildren();
		Fate.SceneIndex sceneIndex = _DependencyProvider.GetEmptySceneIndex();
		Fate.Scene fateScene = _DependencyProvider.GetDefaultScene(SceneName);
	
		while(nodesToProcess.Count > 0)
		{
			Node node = nodesToProcess[^1];
			if (node is GCharacter gCharacterNode)
			{
				// Character node in scene. Store to scene index.
				sceneIndex.Characters.Add(gCharacterNode.FateCharacter.Name, gCharacterNode.FateCharacter);
			}

			nodesToProcess.RemoveAt(nodesToProcess.Count - 1);
			nodesToProcess.AddRange(node.GetChildren());
		}

		string serializedScene = _SerializeObj(fateScene);
		using FileAccess sceneFile = FileAccess.Open($"res://{SceneName}.json", FileAccess.ModeFlags.Write);
		sceneFile.StoreString(serializedScene);
		Fate.Logger.INFO($"{sceneFile.GetPathAbsolute()} written to disk");

		string serializedIndex = _SerializeObj(sceneIndex);
		using FileAccess sceneIndexFile = FileAccess.Open($"res://{SceneName}_Index.json", FileAccess.ModeFlags.Write);
		sceneIndexFile.StoreString(serializedIndex);

		Fate.Logger.INFO($"{sceneIndexFile.GetPathAbsolute()} written to disk");
	}

	private string _SerializeObj(object obj)
	{
 		return JsonSerializer.Serialize(
			obj,
			obj.GetType(),
			new JsonSerializerOptions() 
			{
				WriteIndented = true
			}
		);
	}
}
