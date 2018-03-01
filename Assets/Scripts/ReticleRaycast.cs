﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleRaycast : MonoBehaviour {

	/*Public*/

	[TooltipAttribute("Use this variable to change how far away from them a player can reach!")]
	public float raycastReach = 3;

	/* Private */
	[SerializeField] private bool canClick;
	private Ray theRay;
	private GameObject currentObj;

	public Text descriptionText;

	public SpellMaker spellMaker;

	void FixedUpdate() 
	{	
		// Raycast to see if Player is looking at important object
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(transform.position, transform.forward, out hit, raycastReach)){
			currentObj = hit.collider.gameObject;
			if (hit.collider.gameObject.tag == "Spell" || spellMaker.selectedSpells.Count != 0 ||spellMaker.spellCombination != null){
				canClick = true;
				if (hit.collider.gameObject.tag == "Spell"){
				descriptionText.text = currentObj.GetComponent<SpellAttributes> ().spell.description;
				}
			}
		else {
			canClick = false;
				descriptionText.text = " ";
		}

		// Actual clicking on object
		if (canClick == true && Input.GetButtonDown("Fire1")){
			Click(hit.collider.gameObject.tag);
		}
			if (canClick == true && Input.GetButtonDown ("Fire2")) {
				CastSpell (spellMaker.currentSpell);
			}
	}
}
	private void Click(string objTag) {
		if (objTag == "Spell") {
			Debug.Log (currentObj.name);
			spellMaker.selectedSpells.Add( currentObj.GetComponent<SpellAttributes> ().spell);
		} else {
		}
	}
	private void CastSpell(Spell spell){
		Debug.Log ("You've cast: " + spell.name);
		spellMaker.CombineSpells ();
	}
}