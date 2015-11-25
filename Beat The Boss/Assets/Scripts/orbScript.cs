using UnityEngine;
using System.Collections;

public class orbScript : MonoBehaviour {

	public float speed;
	public Vector2 _direction;
	public bool isReady;

	void Awake() {
		isReady = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isReady) {
			Vector2 position = transform.position;

			position += _direction * speed * Time.deltaTime;

			transform.position = position;

		}
		Destroy (gameObject,3);
		
	}

	void FixedUpdate() {
		speed += .5f;
	}

	public void setDirection(Vector2 direction)
	{
		_direction = direction.normalized;
		isReady = true;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (other.gameObject);
		}
		
	}
	
}

