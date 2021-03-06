using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject {

	public new string name;
	public string description;
	[Space]
	[Space]
	public Color color;

	public float damage;
}
