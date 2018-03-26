using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleRaycast : MonoBehaviour {

	/*Public*/

	[TooltipAttribute("Use this variable to change how far away from them a player can reach!")]
	public float raycastReach = 100f;

	public float spellRange = 100f;

	/* Private */
	[SerializeField] private bool canClick;
	private Ray theRay;
	private GameObject currentObj;
	private GameObject laneObj;

	public Text descriptionText;

	public SpellMaker spellMaker;
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

	public int spellsSelected;

	[Header("Particle Effects")]
	public GameObject fire;
	public GameObject ice;
	public GameObject wind;

	void Start () {
		canCombine = true;
		combinationTimer = timeToCombine;
	}
	void FixedUpdate() 
	{	
		canClick = true;
		spellsSelected = spellMaker.selectedSpells.Count;

		triggerInputValue = Input.GetAxisRaw ("Fire1");
		if (triggerInputValue > -0.2) {
			canCast = true;
		} 
		// Raycast to see if Player is looking at important object
		RaycastHit hit = new RaycastHit();

		if (spellsSelected == 0 && (Input.GetButtonDown ("Fire") || Input.GetButtonDown ("Ice") || Input.GetButtonDown ("Wind"))) {
			SpellSelection ();
			//StartCombinationTimer ();
		}
		if (Physics.Raycast(transform.position, transform.forward, out hit, raycastReach)){
			currentObj = hit.collider.gameObject;
			if (hit.collider.gameObject.tag == "Spell" || spellMaker.selectedSpells.Count != 0 || spellMaker.spellCombination != null) {
				canClick = true;
				if (hit.collider.gameObject.tag == "Spell") {
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
			if (canClick == true && canCast == true && triggerInputValue == -1) {
				CastSpell (spellMaker.currentSpell);
				canCast = false;
			}
	}
		RaycastHit lane = new RaycastHit ();
		if (Physics.Raycast (transform.position, transform.forward, out lane, raycastReach)) {
			laneObj = lane.collider.gameObject;
			if (laneObj.transform.tag == "Lane") {
				canClick = true;
				laneObj.GetComponent<Collider> ().GetComponent<Renderer> ().material.color = Color.cyan;
			} else {
				Debug.Log ("Not Looking At A Lane");
			}
		}
}
	/*public void StartCombinationTimer () {
		Debug.Log ("Timer Starts Now");
		canCombine = true;
		if (spellsSelected == 1 && (Input.GetButtonDown ("Fire") || Input.GetButtonDown ("Ice") || Input.GetButtonDown ("Wind"))) {
			SpellSelection ();
			EndCombinationTimer ();
		} else {
			Invoke ("EndCombinationTimer", timeToCombine);
		}
	}
	public void EndCombinationTimer () {
		spellMaker.selectedSpells.Clear ();
		canCombine = false;
	}*/
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
				Instantiate (fire, hit.point, Quaternion.identity);
				break;
			case "Ice":
				Instantiate (ice, hit.point, Quaternion.identity);
				break;
			case "Wind":
				Instantiate (wind, hit.point, Quaternion.identity);
				break;
			}
			/*if (spellMaker.selectedSpells.Count != 1) {
				spellMaker.selectedSpells.Clear ();

			}*/
			spellMaker.selectedSpells.Clear ();
			spellMaker.CombineSpells ();
		}
	}
}