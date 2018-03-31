using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] lanes;
	public GameObject currentLane;
	public GameObject lastLane;
	public GameObject enemy;
	public GameObject enemyPrefab;

	public List<GameObject> activeEnemies;
	[Range(0, 5f)]
	public float spawnTimer;

	[Range(1, 25)]
	public int enemyLimit;
	public int numberOfEnemies;

	public void Start () {
		Spawn ();
	}
	/*public void LateUpdate() {
		StartCoroutine ("CheckForEnemies");

	}*/
	public void Spawn ( ) {
		if (activeEnemies.Count <= enemyLimit) {
			currentLane = lanes [Random.Range (0, lanes.Length)];
			Debug.Log ("The Current Lane is: " + currentLane);
			if (currentLane != lastLane) {
				enemy = Instantiate (enemyPrefab, currentLane.transform.position, Quaternion.identity) as GameObject;
				activeEnemies.Add (enemy);
				lastLane = currentLane;
			}
		}
		Invoke ("Spawn", spawnTimer);
	}
	/*public IEnumerator CheckForEnemies () {
		numberOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (numberOfEnemies < (enemyLimit + 1)) {
			Invoke ("Spawn", spawnTimer);
		}
		yield return null;
	}*/
}
