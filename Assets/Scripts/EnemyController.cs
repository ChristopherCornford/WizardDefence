using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public EnemySpawner enemySpawner;
	public ScoreManager scoreManager;

	public float enemySpeed;


	// Use this for initialization
	void Awake () {
		enemySpawner = GameObject.Find ("/Lanes/SpawnPoints").GetComponent<EnemySpawner> ();
		scoreManager = GameObject.Find ("/ScoreManager").GetComponent<ScoreManager> ();
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
			scoreManager.Score++;
	
		}
	}
}
