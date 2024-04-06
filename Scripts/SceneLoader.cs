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

	[Signal]
	public delegate void FateSceneLoadedEventHandler(Node sceneLoader);

	public Fate.Scene LoadedFateScene;
	public Fate.SceneIndex LoadedFateIndex;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (SceneFile is null || SceneIndexFile is null)
		{
			Logger.ERROR("Both SceneFile and SceneIndexFile are required for SceneLoader node.");
			throw new ArgumentNullException("Both SceneFile and SceneIndexFile are required");
		}

		Logger.SetErrorFunc(s => { GD.PushError(s); GD.PrintErr(s); });
		Logger.SetPrintFunc(GD.Print);

		using FileAccess sceneFileAccess = FileAccess.Open(SceneFile, FileAccess.ModeFlags.Read);
		using FileAccess sceneIndexAccess = FileAccess.Open(SceneIndexFile, FileAccess.ModeFlags.Read);

		Logger.INFO($"Loading scene {SceneFile}");
		LoadedFateScene = JsonSerializer.Deserialize<Fate.Scene>(sceneFileAccess.GetAsText());

		Logger.INFO($"Loading scene index {SceneIndexFile}");
		LoadedFateIndex = JsonSerializer.Deserialize<Fate.SceneIndex>(sceneIndexAccess.GetAsText());

		Logger.INFO("Emitting scene loaded signal");
		EmitSignal(SignalName.FateSceneLoaded, this);
	}
}
