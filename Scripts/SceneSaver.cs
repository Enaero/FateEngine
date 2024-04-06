using Fate;
using Godot;
using System;
using System.Reflection;
using System.Text.Json;

public partial class SceneSaver : Node
{
	[Export]
	public string SceneName = "scene";

	public Character character = new DependencyProvider().GetDefaultCharacter("cuung");
	public Character character2 = new DependencyProvider().GetDefaultCharacter("cuung2");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		Godot.Collections.Array<Node> nodesToProcess = GetTree().Root.GetChildren();
		
		while(nodesToProcess.Count > 0)
		{
			Node node = nodesToProcess[^1];
			FieldInfo[] nodeFieldInfos = node.GetType().GetFields();
			foreach (FieldInfo fieldInfo in nodeFieldInfos)
			{
				if (fieldInfo.GetValue(node) is Character characterObj)
				{
					string serialized = JsonSerializer.Serialize(characterObj, characterObj.GetType());
					using FileAccess file = FileAccess.Open($"res://{SceneName}_{characterObj.Name}.json", FileAccess.ModeFlags.Write);
					file.StoreString(serialized);
					Logger.INFO($"{file.GetPathAbsolute()} to to disk");
				}
			}

			nodesToProcess.RemoveAt(nodesToProcess.Count - 1);
			nodesToProcess.AddRange(node.GetChildren());
		}
	}
}
