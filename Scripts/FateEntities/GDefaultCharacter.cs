using Fate;
using Godot;
using System;
using System.Text.Json;

public partial class GDefaultCharacter : GCharacter
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (String.IsNullOrEmpty(CharacterName))
		{
			throw new ArgumentException($"CharacterName is required for node[{Name}]");
		}

		FateCharacter = new DependencyProvider().GetDefaultCharacter(CharacterName);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
