using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleRaycast : MonoBehaviour {

	/*Public*/

	[TooltipAttribute("Use this variable to change how far away from them a player can reach!")]
	public float raycastReach = 3;

	public float spellRange = 15;

	/* Private */
	[SerializeField] private bool canClick;
	private Ray theRay;
	private GameObject currentObj;

	public Text descriptionText;

	public SpellMaker spellMaker;
	public GameObject fireball;
	public GameObject[] effects;

	public float triggerInputValue;
	public bool canCast = false;

	[Range(0.1f, 5.0f)]
	public float timeToCombine = 5f;
	public float combinationTimer;
	public bool canCombine = true;
	[Range(0f, 1f)]
	public float singleSpellCooldown;
	[Range(1f, 5f)]
	public float combinedSpellCooldown;

	void Start () {
		canCombine = true;
		combinationTimer = timeToCombine;
	}
	void FixedUpdate() 
	{	
		triggerInputValue = Input.GetAxisRaw ("Fire1");
		if (triggerInputValue > -0.2) {
			canCast = true;
		} 
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
			if (Input.GetButtonDown ("Fire") || Input.GetButtonDown ("Ice") || Input.GetButtonDown ("Wind")) {
				SpellSelection ();
				StartCombinationTimer ();
			}
		// Actual clicking on object
		if (canClick == true && Input.GetButtonDown("Fire1")){
			Click(hit.collider.gameObject.tag);
		}
			if (canClick == true && canCast == true && triggerInputValue == -1) {
				CastSpell (spellMaker.currentSpell);
			}
	}
}
	public void StartCombinationTimer () {
		Debug.Log ("Timer Starts Now");
		canCombine = true;
		if (Input.GetButtonDown ("Fire") || Input.GetButtonDown ("Ice") || Input.GetButtonDown ("Wind")) {
			EndCombinationTimer ();
		} else {
			Invoke ("EndCombinationTimer", timeToCombine);
		}
	}
	public void EndCombinationTimer () {
		canCombine = false;
	}
	private void SpellSelection () {
		if (Input.GetButtonDown("Fire")) {
			spellMaker.currentSpell = spellMaker.Fire;
			if (canCombine == true) {
				spellMaker.selectedSpells.Add (spellMaker.currentSpell);
			}
		} else if (Input.GetButtonDown("Ice")) {
			spellMaker.currentSpell = spellMaker.Ice;
			if (canCombine == true) {
				spellMaker.selectedSpells.Add (spellMaker.currentSpell);
			}
		} else if (Input.GetButtonDown("Wind")) {
			spellMaker.currentSpell = spellMaker.Wind;
			if (canCombine == true) {
				spellMaker.selectedSpells.Add (spellMaker.currentSpell);
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
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(transform.position, transform.forward, out hit, spellRange)){
		Debug.Log ("You've cast: " + spell.name);
			switch (spellMaker.currentSpell.name) {
			case "Fire":
				Instantiate (fireball, transform.position, Quaternion.identity);
				break;
			case "Ice":
				break;
			case "Wind":
				break;
			}
		spellMaker.CombineSpells ();
		}
	}
}