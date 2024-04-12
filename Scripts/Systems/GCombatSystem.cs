using Fate;
using Godot;
using System;

public partial class GCombatSystem : Node
{
	public CombatSystem FateCombatSystem;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GDependencyProvider gDp = GetTree().Root.FindChildByType<GDependencyProvider>();
		if (gDp is null)
		{
			throw new ArgumentNullException("GDependencyProvider is missing from scene.");
		}
		FateCombatSystem = gDp.FateDependencyProvider.GetCombatSystem();

		CombatGui combatGui = GetTree().Root.FindChildByType<CombatGui>();
		combatGui.AttackChosen += _OnAttackChosen;
	}

	private void _OnAttackChosen(string attackName)
	{
		// Do turn manager check and handle attack.
		GD.Print("Attack chosen " + attackName);
	}
}
