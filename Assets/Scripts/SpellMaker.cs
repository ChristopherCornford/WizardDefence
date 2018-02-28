using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMaker : MonoBehaviour {

	public List<Spell> selectedSpells; 
	public Spell spellCombination;
	public GameObject combinedSpellOrb;
	public Color combinedColor;
	public float combinedDamage;

	// Update is called once per frame
	void Update () {
		if (selectedSpells.Count == 2) {
			CombineSpells ();
		}
	}
	private void CombineSpells () {
		spellCombination = ScriptableObject.CreateInstance("Spell") as Spell;
		combinedColor = selectedSpells [0].color + selectedSpells [1].color;
		spellCombination.color = combinedColor;
		spellCombination.damage = selectedSpells [0].damage + selectedSpells [1].damage;
		GenerateAttributes ();
		selectedSpells.Clear();
		combinedSpellOrb.GetComponent<SpellAttributes> ().spell = spellCombination;
		combinedSpellOrb.GetComponent<SpellAttributes> ().Attribute ();
	}
	private void GenerateAttributes() {
		switch (selectedSpells [0].name) {
		case "Fire":
			switch (selectedSpells [1].name) {
			case "Fire":
				spellCombination.name = "Fire Wall";
				break;
			case "Ice":
				spellCombination.name = "Magma";
				break;
			case "Wind":
				spellCombination.name = "Fire Tornado";
				break;
			}
			break;
		case "Ice":
			switch (selectedSpells [1].name) {
			case "Fire":
				spellCombination.name = "Magma";
				break;
			case "Ice":
				spellCombination.name = "Glacial Tsunami";
				break;
			case "Wind":
				spellCombination.name = "Blizzard";
				break;
			}
			break;
		case "Wind":
			switch (selectedSpells [1].name) {
			case "Fire":
				spellCombination.name = "Fire Tornado";
				break;
			case "Ice":
				spellCombination.name = "Blizzard";
				break;
			case "Wind":
				spellCombination.name = "Hurricane";
				break;
			}
			break;
		}
	}
}
