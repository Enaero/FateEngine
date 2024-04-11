using Fate;
using Godot;
using System;
using System.Text.Json;

public partial class CreateCombatDemo : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DependencyProvider provider = new DependencyProvider();

		ActionIndex combatIndex = provider.GetEmptyCombatIndex();
		combatIndex.Attacks.Add(CommonActions.BASIC_ATTACK.Name, CommonActions.BASIC_ATTACK);
		combatIndex.Defenses.Add(CommonActions.BLOCK.Name, CommonActions.BLOCK);
		combatIndex.Defenses.Add(CommonActions.BRACE.Name, CommonActions.BRACE);
		combatIndex.Defenses.Add(CommonActions.DODGE.Name, CommonActions.DODGE);

		Character larry = _CreateLarry(provider);
		Character aiden = _CreateAiden(provider);

		SceneIndex sceneIndex = provider.GetEmptySceneIndex();
		sceneIndex.Characters.Add(larry.Name, larry);
		sceneIndex.Characters.Add(aiden.Name, aiden);
		
		Zone mainZone = provider.GetDefaultZone("MainZone");
		mainZone.Entities.Add(larry.Name);
		mainZone.Entities.Add(aiden.Name);

		Scene scene = provider.GetDefaultScene("CombatScene");
		scene.Zones.Add(mainZone);

		using FileAccess sceneFile = FileAccess.Open("res://Dev/CombatScene.json", FileAccess.ModeFlags.Write);
		using FileAccess sceneIndexFile = FileAccess.Open("res://Dev/CombatScene_Index.json", FileAccess.ModeFlags.Write);
		using FileAccess combatIndexFile = FileAccess.Open("res://Dev/CombatScene_ActionIndex.json", FileAccess.ModeFlags.Write);

		sceneFile.StoreString(
			JsonSerializer.Serialize(
				scene, 
				scene.GetType(),
				new JsonSerializerOptions()
				{
					WriteIndented = true
				}));
		sceneIndexFile.StoreString(
			JsonSerializer.Serialize(
				sceneIndex,
				sceneIndex.GetType(),
				new JsonSerializerOptions()
				{
					WriteIndented = true
				}));
		combatIndexFile.StoreString(
			JsonSerializer.Serialize(
				combatIndex, 
				combatIndex.GetType(),
				new JsonSerializerOptions()
				{
					WriteIndented = true
				}));
		
		GD.Print("Finished creating scene.");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private Character _CreateLarry(DependencyProvider provider)
	{
		Character larry = provider.GetDefaultCharacter("Larry");

		larry.Skills[CharacterSkills.ATHLETICS].Value = 2; 
		larry.Skills[CharacterSkills.ACADEMICS].Value = 3;
		larry.Skills[CharacterSkills.BURGLARY].Value = 0;
		larry.Skills[CharacterSkills.CONTACTS].Value = 1;
		larry.Skills[CharacterSkills.CRAFTS].Value = 1;
		larry.Skills[CharacterSkills.DECEIVE].Value = 0;
		larry.Skills[CharacterSkills.DRIVE].Value = 1;
		larry.Skills[CharacterSkills.EMPATHY].Value = 0;
		larry.Skills[CharacterSkills.FIGHT].Value = 1;
		larry.Skills[CharacterSkills.INVESTIGATE].Value = 1;
		larry.Skills[CharacterSkills.LORE].Value = 2;
		larry.Skills[CharacterSkills.NOTICE].Value = 0;
		larry.Skills[CharacterSkills.PHYSIQUE].Value = 2;
		larry.Skills[CharacterSkills.PROVOKE].Value = 4;
		larry.Skills[CharacterSkills.RAPPORT].Value = 0;
		larry.Skills[CharacterSkills.RESOURCES].Value = 1;
		larry.Skills[CharacterSkills.SHOOT].Value = 0;
		larry.Skills[CharacterSkills.STEALTH].Value = 0;
		larry.Skills[CharacterSkills.WILL].Value = 2;

		larry.AddAspects(
			new Aspect("Underachieving Office Drone with a Delinquent Past"),
			new Aspect("Eager to Self-Sacrifice"),
			new Aspect("My Wife is my EVERYTHING")
		);

		larry.PhysicalStress = provider.GetDefaultStress(Character.PHYSICAL_STRESS_NAME);
		larry.PhysicalStress.Capacity = 4;

		larry.MentalStress = provider.GetDefaultStress(Character.MENTAL_STRESS_NAME);
		larry.MentalStress.Capacity = 3;

		larry.FatePoints = 3;
		larry.FateRefresh = 3;

		return larry;
	}

	private Character _CreateAiden(DependencyProvider provider)
	{
		Character aiden = provider.GetDefaultCharacter("Aiden");

		aiden.Skills[CharacterSkills.ATHLETICS].Value = 4; 
		aiden.Skills[CharacterSkills.ACADEMICS].Value = 2;
		aiden.Skills[CharacterSkills.BURGLARY].Value = 0;
		aiden.Skills[CharacterSkills.CONTACTS].Value = 2;
		aiden.Skills[CharacterSkills.CRAFTS].Value = 0;
		aiden.Skills[CharacterSkills.DECEIVE].Value = 0;
		aiden.Skills[CharacterSkills.DRIVE].Value = 2;
		aiden.Skills[CharacterSkills.EMPATHY].Value = 3;
		aiden.Skills[CharacterSkills.FIGHT].Value = 3;
		aiden.Skills[CharacterSkills.INVESTIGATE].Value = 1;
		aiden.Skills[CharacterSkills.LORE].Value = 1;
		aiden.Skills[CharacterSkills.NOTICE].Value = 2;
		aiden.Skills[CharacterSkills.PHYSIQUE].Value = 2;
		aiden.Skills[CharacterSkills.PROVOKE].Value = 0;
		aiden.Skills[CharacterSkills.RAPPORT].Value = 2;
		aiden.Skills[CharacterSkills.RESOURCES].Value = 0;
		aiden.Skills[CharacterSkills.SHOOT].Value = 0;
		aiden.Skills[CharacterSkills.STEALTH].Value = 2;
		aiden.Skills[CharacterSkills.WILL].Value = 2;

		aiden.AddAspects(
			new Aspect("Baseball Superstar with a Heart of Gold"),
			new Aspect("Blackout Blitz"),
			new Aspect("Mother always said \"Do the noble thing...\"")
		);

		aiden.PhysicalStress = provider.GetDefaultStress(Character.PHYSICAL_STRESS_NAME);
		aiden.PhysicalStress.Capacity = 3;

		aiden.MentalStress = provider.GetDefaultStress(Character.MENTAL_STRESS_NAME);
		aiden.MentalStress.Capacity = 4;

		aiden.FatePoints = 3;
		aiden.FateRefresh = 3;

		// Default character has basic attacks and defenses already.

		return aiden;
	}
}
