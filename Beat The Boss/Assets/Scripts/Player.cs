using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject gameOverScreen;
	public Transform gameOverPos;

	public int fallBoundary = -20;

	public bool isGameOver;

	[System.Serializable]
	public class PlayerStats {
		// Player should be a one hit death, add variables for stats I may implement in the future
		// public float Health = 100f;
	}

	void Start() {
		isGameOver = false;
	}

	void Update() {
		if (transform.position.y <= fallBoundary) {
			hitPlayer (true);
		}
	}

	// once the player has been hit, display game over screen
	public void hitPlayer (bool hit) {
		if (hit) {
			GameMaster.KillPlayer(this);
			Instantiate(gameOverScreen,gameOverPos.position,gameOverPos.rotation);
			isGameOver =  true;
			Time.timeScale = 0.0000000001f; // this will allow me to stop the execution of all other game objects
		}
	}

	// Collions with only attacks, attackColOff are attacks that do not detect collisions 
	void OnCollisionEnter2D(Collision2D other) {
		string layerName = LayerMask.LayerToName (other.gameObject.layer);
		if (layerName == "Attack" || layerName == "AttackColOff") {
			hitPlayer(true);
		}
		
	}


}
