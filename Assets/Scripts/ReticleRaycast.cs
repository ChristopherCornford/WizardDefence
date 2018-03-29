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
	[Header("Spell Combining")]
	[Range(0.1f, 5.0f)]
	public float timeToCombine = 5f;
	public float combinationTimer;
	private bool activateTimerToReset;
	[SerializeField]
	private bool canCombine = false;
	[Header("Cool Downs")]
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
		combinationTimer = timeToCombine;
	}
	void FixedUpdate() 
	{	
		canClick = true;
		//spellsSelected = spellMaker.selectedSpells.Count;

		triggerInputValue = Input.GetAxisRaw ("Fire1");
		if (triggerInputValue > -0.2) {
			canCast = true;
		} 
		// Raycast to see if Player is looking at important object
		RaycastHit hit = new RaycastHit();

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
		NewSpellCombo ();
		ResetSpellsCombo (activateTimerToReset);
}
	private void ResetSpellsCombo(bool resetTimer) {
		if (resetTimer) {
			combinationTimer -= Time.deltaTime;
			if (combinationTimer <= 0) {
				canCombine = false;
				activateTimerToReset = false;
				combinationTimer = timeToCombine;
				spellsSelected = 0;
			}
		}
	}
	private void NewSpellCombo () {
		if (Input.GetButtonDown ("Fire") || Input.GetButtonDown ("Ice") || Input.GetButtonDown ("Wind")) {
			activateTimerToReset = true;
			switch (canCombine) {
			case true:
				if (Input.GetButtonDown("Fire")) {
						spellMaker.selectedSpells.Add (spellMaker.Fire);
						combinationTimer = 0;
				} else if (Input.GetButtonDown("Ice")) {
						spellMaker.selectedSpells.Add (spellMaker.Ice);
						combinationTimer = 0;
				} else if (Input.GetButtonDown("Wind")) {
						spellMaker.selectedSpells.Add (spellMaker.Wind);
						combinationTimer = 0;
				}
				break;
			case false:
				if (Input.GetButtonDown("Fire")) {
					spellMaker.currentSpell = spellMaker.Fire;
					spellMaker.selectedSpells.Add (spellMaker.currentSpell);

				} else if (Input.GetButtonDown("Ice")) {
					spellMaker.currentSpell = spellMaker.Ice;
					spellMaker.selectedSpells.Add (spellMaker.currentSpell);

				} else if (Input.GetButtonDown("Wind")) {
					spellMaker.currentSpell = spellMaker.Wind;
					spellMaker.selectedSpells.Add (spellMaker.currentSpell);
				}
				canCombine = true;
				break;
			}
			spellsSelected++;
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
		if (canCombine == true) {
			combinationTimer = 0;
		} else if (canCombine == false) {
			combinationTimer = timeToCombine;
		}
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
			spellMaker.selectedSpells.Clear ();
			spellMaker.CombineSpells ();
		}
	}
}