using Godot;
using System;
using Fate;
using System.Collections.Generic;
using System.Linq;

public partial class Rock : Node
{
	[Export] public string[] AspectNames;

	private FateEntity _fateComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Logger.SetErrorFunc(s => { GD.PushError(s); GD.PrintErr(s); });
		Logger.SetPrintFunc(GD.Print);

		_fateComponent = new FateEntity("Name", AspectsCatalogue.INSTANCE.GetAspects(AspectNames).ToArray());

		foreach (Aspect aspect in _fateComponent.GetAllAspects())
		{
			Logger.INFO($"Loaded aspect: {aspect}");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
