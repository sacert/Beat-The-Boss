using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask notToHit;

	float timeToFire = 0;
	Transform throwingPoint;

	// Use this for initialization
	void Start () {
		throwingPoint = transform.FindChild ("ThrowingObjectPosition");

	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetKeyDown (KeyCode.X)) {
				Shoot ();
			}
		}
		else {
			if (Input.GetKey (KeyCode.X) && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Shoot ();
			}
		}
		
	}

	void Shoot() {
		Vector2 throwingOjbectPosition = new Vector2 (throwingPoint.position.x, throwingPoint.position.y);
		Debug.DrawLine (throwingOjbectPosition,throwingOjbectPosition * 100);
	}
}
