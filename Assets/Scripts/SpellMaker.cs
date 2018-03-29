using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellMaker : MonoBehaviour {

	public SpellAttributes combinedOrbAttributes;

	public List<Spell> selectedSpells; 
	public Spell spellCombination;
	public GameObject combinedSpellOrb;
	public Color combinedColor;
	public float combinedDamage;
	public Spell currentSpell;

	public Text currentSpellText;
	[Header("List of all Spells")]
	public Spell Fire;
	public Spell Ice;
	public Spell Wind;
	public Spell Blizzard;
	public Spell FireTornado;
	public Spell FireWall;
	public Spell GlacialTsunami;
	public Spell Hurricane;
	public Spell Magma;
	public Spell EmptySpell;
	// Update is called once per frame
	void LateUpdate () {
		if (selectedSpells.Count == 1) {
			CombineSpells ();
		} else if ( selectedSpells.Count == 2) {
			CombineSpells ();
		}
		currentSpellText.text = "Current Spell: " + currentSpell;
	}
	public void CombineSpells () {
		if (selectedSpells.Count == 0) {
			currentSpell = EmptySpell;
		} else if (selectedSpells.Count == 1) {
			currentSpell = selectedSpells [0];
		} else if (selectedSpells.Count == 2) {
			GenerateAttributes ();
			selectedSpells.Clear ();
		}
		combinedOrbAttributes.spell = currentSpell;
		combinedOrbAttributes.Attribute ();
		combinedSpellOrb.transform.tag = "XSpell";
	}
	private void GenerateAttributes() {
		switch (selectedSpells [0].name) {
		case "Fire":
			switch (selectedSpells [1].name) {
			case "Fire":
				spellCombination = FireWall;
				currentSpell = FireWall;
				break;
			case "Ice":
				spellCombination = Magma;
				currentSpell = Magma;
				break;
			case "Wind":
				spellCombination = FireTornado;
				currentSpell = FireTornado;
				break;
			}
			break;
		case "Ice":
			switch (selectedSpells [1].name) {
			case "Fire":
				spellCombination = Magma;
				currentSpell = Magma;;
				break;
			case "Ice":
				spellCombination = GlacialTsunami;
				currentSpell = GlacialTsunami;
				break;
			case "Wind":
				spellCombination = Blizzard;
				currentSpell = Blizzard;
				break;
			}
			break;
		case "Wind":
			switch (selectedSpells [1].name) {
			case "Fire":
				spellCombination = FireTornado;
				currentSpell = FireTornado;
				break;
			case "Ice":
				spellCombination = Blizzard;
				currentSpell = Blizzard;
				break;
			case "Wind":
				spellCombination = Hurricane;
				currentSpell = Hurricane;
				break;
			}
			break;
		}
	}
}
