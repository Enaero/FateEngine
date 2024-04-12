using Fate;
using Godot;
using System;

public partial class GDependencyProvider : Node
{
	public DependencyProvider FateDependencyProvider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FateDependencyProvider = new DependencyProvider();
	}
}
