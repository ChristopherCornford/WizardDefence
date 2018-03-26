using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour {

	public void OnParticleCollison(GameObject other){
		if (other.transform.tag == "Enemy") {
			SendMessage ("Score");
		}
	}
}
