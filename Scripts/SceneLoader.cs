using Godot;
using System;
using System.Text.Json;
using Logger = Fate.Logger;

public partial class SceneLoader : Node
{
	[Export]
	public string SceneFile;

	[Export]
	public string SceneIndexFile;
	
	[Export]
	public string ActionIndexFile;

	[Signal]
	public delegate void FateSceneLoadedEventHandler(Node sceneLoader);

	public bool isFinishedLoading = false;
	public Fate.Scene LoadedFateScene;
	public Fate.SceneIndex LoadedFateIndex;
	public Fate.ActionIndex LoadedActionIndex;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (SceneFile is null || SceneIndexFile is null || ActionIndexFile is null)
		{
			Logger.ERROR("All scene files are required for SceneLoader node.");
			throw new ArgumentNullException("All scene files are required");
		}

		Logger.SetErrorFunc(s => { GD.PushError(s); GD.PrintErr(s); });
		Logger.SetPrintFunc(GD.Print);

		using FileAccess sceneFileAccess = FileAccess.Open(SceneFile, FileAccess.ModeFlags.Read);
		using FileAccess sceneIndexAccess = FileAccess.Open(SceneIndexFile, FileAccess.ModeFlags.Read);
		using FileAccess actionIndexAccess = FileAccess.Open(ActionIndexFile, FileAccess.ModeFlags.Read);
		
		Logger.INFO($"Loading scene {SceneFile}");
		LoadedFateScene = JsonSerializer.Deserialize<Fate.Scene>(sceneFileAccess.GetAsText());

		Logger.INFO($"Loading scene index {SceneIndexFile}");
		LoadedFateIndex = JsonSerializer.Deserialize<Fate.SceneIndex>(sceneIndexAccess.GetAsText());
		
		Logger.INFO($"Loading action index {ActionIndexFile}");
		LoadedActionIndex = JsonSerializer.Deserialize<Fate.ActionIndex>(actionIndexAccess.GetAsText());

		Logger.INFO("Finished loading scene files.");

		Logger.INFO("Emitting scene loaded signal");
		EmitSignal(SignalName.FateSceneLoaded, this);
		isFinishedLoading = true;
	}
}
