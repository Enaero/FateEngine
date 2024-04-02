using Fate;
using Godot;
using System;

public partial class SceneLoader : Node
{
	[Export]
	public string SaveFile;

	[Signal]
	public delegate void FateSceneLoadedEventHandler(Node sceneLoader);

	public Fate.Scene LoadedScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Logger.SetErrorFunc(s => { GD.PushError(s); GD.PrintErr(s); });
		Logger.SetPrintFunc(GD.Print);

		SaveFile = "CombatScene.json";
		Logger.INFO($"Loading scene {SaveFile}");
		LoadedScene = new("CombatScene", Array.Empty<Aspect>());

		Logger.INFO("Emitting scene loaded signal");
		EmitSignal(SignalName.FateSceneLoaded, this);
	}
}
