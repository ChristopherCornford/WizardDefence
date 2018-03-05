using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public EnemySpawner enemySpawner;

	public float enemySpeed;


	// Use this for initialization
	void Awake () {
		enemySpawner = GameObject.Find ("/Lanes/SpawnPoints").GetComponent<EnemySpawner> ();
	}
	
	// Update is called once per frame
	void Update () {
		MoveEnemy ();
	}
	private void MoveEnemy () {
		Vector3 enemyTransform = new Vector3 (0f, 0f, -(Time.deltaTime * enemySpeed));
		transform.position += enemyTransform;
	}

	public void OnTriggerEnter (Collider collider) {
		if (collider.transform.tag == "Death") {
			enemySpawner.activeEnemies.Remove (gameObject);
			enemySpawner.Invoke ("Spawn", enemySpawner.spawnTimer);
			Destroy (gameObject);
		}
	}
}
