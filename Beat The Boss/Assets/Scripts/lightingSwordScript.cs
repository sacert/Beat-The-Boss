using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
	{
	public class lightingSwordScript : MonoBehaviour {

		public float speed;
		private Boss boss;
		
		// Use this for initialization
		void Start () {
			boss = FindObjectOfType<Boss> ();

			if (boss.facingLeft == true) {
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
				speed = -speed;
			}

		}
		
		// Update is called once per frame
		void Update () {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);
			Destroy (gameObject,2);
			
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.tag == "Player") {
				Destroy (other.gameObject);
			}
		}
	}
}