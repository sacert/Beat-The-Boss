using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	public class fireballScript : MonoBehaviour {

		public float speed;
		private Boss Boss; 
		public GameObject impactEffect;
		
		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			GetComponent<Rigidbody2D> ().velocity = transform.up.normalized * -speed; //new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);
			Destroy (gameObject,2);
			
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.tag != "Boss") {
				Destroy (gameObject);
			}

		}

	}
}
