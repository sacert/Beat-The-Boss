using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	//RequireComponent(typeof (PlatformerCharacter2D))]
	public class BulletController : MonoBehaviour {

		public float speed;
		private PlatformerCharacter2D Player;
		public static int damage = 1; 
		public GameObject impactEffect;

		// Use this for initialization
		void Start () {
			Player = FindObjectOfType<PlatformerCharacter2D> ();

			if (Player.transform.localScale.x < 0) {
				speed = -speed;
			}
		}
		
		// Update is called once per frame
		void Update () {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);
			Destroy (gameObject,2);
		
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.tag != "Player") {
				Destroy(Instantiate(impactEffect,transform.position,transform.rotation),1f);
				Destroy (gameObject);
			}
		}
	}
}