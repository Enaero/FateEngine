using Fate;
using Godot;
using System;
using System.Threading.Tasks;

public partial class PostSceneLoadNode : Node
{
	[Export]
	public Node SceneLoaderNode;

	protected Task _SceneLoadTask;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_SceneLoadTask = WaitForFateSceneToLoad();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private async Task WaitForFateSceneToLoad()
	{
		if (SceneLoaderNode is null || SceneLoaderNode is not SceneLoader)
		{
			GD.PushWarning($"SceneLoaderNode not set or valid in {Name}. Attempting to find default.");
			SceneLoaderNode = NodeOps.FindNode<SceneLoader>(GetTree().Root);
			if (SceneLoaderNode is null)
			{
				GD.PushError("Cannot find SceneLoader node. Please set it in editor");
				throw new ArgumentNullException($"SceneLoader is not set in {this}");
			}
		}
		GD.Print($"{Name} waiting for Fate Scene to load...");
		SceneLoader sceneLoader = (SceneLoader) SceneLoaderNode;
		if (!sceneLoader.isFinishedLoading)
		{
			await ToSignal(SceneLoaderNode, SceneLoader.SignalName.FateSceneLoaded);
		}

		GD.Print($"{Name} finished waiting for Fate Scene to load!");
	}
}
