using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellMaker : MonoBehaviour {

	public SpellAttributes combinedOrbAttributes;

	public List<Spell> selectedSpells; 
	public Spell spellCombination;
	public GameObject combinedSpellOrb;
	public Color combinedColor;
	public float combinedDamage;
	public Spell currentSpell;

	public TMP_Text currentSpellText;
	public TMP_Text currentSpellTextSpell;
	[Header("List of all Spells")]
	public Spell Fire;
	public Spell Ice;
	public Spell Wind;
	public Spell Lightning;
	public Spell FireTornado;
	public Spell FireWall;
	public Spell GlacialTsunami;
	public Spell Hurricane;
	public Spell Magma;
	public Spell EmptySpell;

	void Start() {
		currentSpellText.text = "Current Spell: ";
		currentSpellTextSpell.text = "";
	}

	void LateUpdate () {
		if (selectedSpells.Count == 1) {
			CombineSpells ();
		} else if ( selectedSpells.Count == 2) {
			CombineSpells ();
		}
		currentSpellTextSpell.text = currentSpell.name;
		currentSpellTextSpell.color = currentSpell.color + new Color(0, 0, 0, 195);
		currentSpellText.text = "Current Spell: ";

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
				spellCombination = Lightning;
				currentSpell = Lightning;
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
				spellCombination = Lightning;
				currentSpell = Lightning;
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
