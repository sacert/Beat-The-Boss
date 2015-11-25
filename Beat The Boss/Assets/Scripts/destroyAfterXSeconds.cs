using UnityEngine;
using System.Collections;

public class destroyAfterXSeconds : MonoBehaviour {

	public float destroyInXSeconds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject,destroyInXSeconds);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (other.gameObject);
		}
		
	}
}
