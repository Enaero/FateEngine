using Godot;

public partial class NoOpSceneLoader : SceneLoader
{
    [Export] public string SceneName = "NoOpScene";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Fate.Logger.SetErrorFunc(s => { GD.PushError(s); GD.PrintErr(s); });
		Fate.Logger.SetPrintFunc(GD.Print);
        Fate.DependencyProvider dp = new();

        LoadedFateIndex = dp.GetEmptySceneIndex();
        LoadedFateScene = dp.GetDefaultScene(SceneName);

		Fate.Logger.INFO("Emitting scene loaded signal after no-op");
		EmitSignal(SignalName.FateSceneLoaded, this);
	}
}
