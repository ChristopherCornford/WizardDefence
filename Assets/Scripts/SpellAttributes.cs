using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttributes : MonoBehaviour {

	public Spell spell;

	public Color orbColor;
	// Use this for initialization
	void Start () {
		Attribute ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Attribute () {
		name = spell.name;

		orbColor = spell.color;

		GetComponent<Renderer> ().material.color = orbColor;
		GetComponent<Renderer> ().material.SetColor ("_EmissionColor", orbColor);
		GetComponent<Light> ().color = orbColor;
	}
}
