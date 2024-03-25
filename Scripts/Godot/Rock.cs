using Godot;
using System;
using FateEngine;
using System.Collections.Generic;
using System.Linq;

public partial class Rock : Node
{
	[Export] public string[] AspectNames;

	private FateComponent _fateComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_fateComponent = new FateComponent
		{
			Aspects = AspectsCatalogue.INSTANCE.GetAspects(AspectNames)
		};

		_fateComponent.Aspects.ForEach(
			aspect => GD.Print($"Loaded aspect: {aspect}")
		);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
