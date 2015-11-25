using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	public class Boss : MonoBehaviour {
		
		[System.Serializable]
		public class BossStats {
			public float Health = 100f;
		}

		BossStats Demon = new BossStats();
		public RectTransform healthBar;

		// for fire ball attack
		public Transform firePoint; 
		public GameObject fireball;

		// for lightning strike attack
		public GameObject lightningStrike;
		public Transform lightningStrikePos;
		public GameObject lightningBall;
		public Transform lightningBallPos;

		// for lightning shot attack
		// *NOTE: Changed the sprite so it is no longer a lightning sword
		// Should rename to 'blast shot' or something
		public GameObject lightningSword;
		public Transform shootPoint;

		public GameObject orb;
		public Transform orbPos;

		private Vector3 startPos;
		private Vector3 endPos;
		private float healthFloat;

		// can delete, didnt use this
		private float timer = 0;
		private Vector3 newRot;

		// *NOTE: Change name
		// This is where the boss's set locations are on the ground
		public Transform lightningShotTele1;
		public Transform lightningShotTele2;

		// boss's set locations in the sky
		public Transform skyPosLeft;
		public Transform skyPosRight;

		// for fire fall attack
		// *NOTE: rename to something else, too similar to 'fire ball attack'
		public GameObject fireBallFall;
		public Transform fireFallPos;

		private GameObject playerPos;

		// probably dont need this, delete
		private bool rotateRev;

		private int counter = 0;
		private bool inverse = false;

		// changed so I dont need these
		private bool resetLeft = false;
		private bool returnRight = false;

		private bool moveBottomLeft = false;
		private bool moveBottomRight = false;
		private bool moveTopLeft = false;
		private bool moveTopRight = false;

		private bool bossHit;
		public Animator m_Anim;  

		public bool facingLeft;

		private float resetCounter;

		public bool isDead;

		public GameObject gameWinScreen;
		public Transform gameWinPos;

		void Start() {
			isDead = false;
			facingLeft = true;

			resetCounter = 0;
			endPos = new Vector3 (healthBar.position.x - healthBar.rect.width, healthBar.position.y, healthBar.position.z);
			startPos = healthBar.position;

			playerPos = GameObject.Find ("Player");
			rotateRev = false;

			bossHit = false;

			healthBar.position = startPos;
			newRot = transform.rotation.eulerAngles;
			StartCoroutine (allActions ());
		}
		
		void Update() {
			if (Demon.Health <= 0) {
				isDead = true;
				Instantiate(gameWinScreen,gameWinPos.position,gameWinPos.rotation);
				Destroy (gameObject);
				Time.timeScale = 0.0000000001f; // this will allow me to make every object in the game appear as it is not moving
			}

			playerPos = GameObject.Find ("Player");


			m_Anim.SetBool("bossHit", bossHit);
			bossHit = false;

			// figure out where the boss is supposed to be at the time and move him when necesary 
			if(moveBottomLeft) {
				transform.position = Vector3.MoveTowards (transform.position, lightningShotTele1.position, 40 * Time.deltaTime);
			}
			if(moveBottomRight) {
				transform.position = Vector3.MoveTowards (transform.position, lightningShotTele2.position, 40 * Time.deltaTime);
			}
			if(moveTopLeft) {
				transform.position = Vector3.MoveTowards (transform.position, skyPosLeft.position, 40 * Time.deltaTime);
			}
			if(moveTopRight) {
				transform.position = Vector3.MoveTowards (transform.position, skyPosRight.position, 40 * Time.deltaTime);
			}

		}

		// 2nd rotation of attacks performed by the boss
		// *NOTE: May way to change name to 'fire rain'
		void flamingAttack() {

				// swap the direction of the rain for each interval 
				if(inverse)
					inverse = false;
				else 
					inverse = true;

			 	// between the visible range, create a copy of the fireball and give each a random direction
				for(float i = -30f; i < 40f; i += 5f)
				{
					newRot.z = Random.Range(-30,-15);
					if(inverse) 
						newRot.z = -1 * newRot.z;
					firePoint.rotation = Quaternion.Euler (newRot);
					firePoint.position = new Vector3(i,firePoint.position.y,firePoint.position.z);
					Instantiate (fireball, firePoint.position, firePoint.rotation);
				}
		}

		void lightningAttack() {
			lightingStrike lightningStrike = new lightingStrike ();
			lightningStrike.strike (true);

		}

		void flipBoss() {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		// Shoot off two 'blasts' and then swap positions and shot another two 'blasts'
		IEnumerator lightningShotAttack()
		{
			facingLeft = true;
			transform.position = lightningShotTele2.position;
			yield return new WaitForSeconds (0.5f);
			Instantiate (lightningSword, shootPoint.position, shootPoint.rotation);
			yield return new WaitForSeconds (1f);
			Instantiate (lightningSword, shootPoint.position, shootPoint.rotation);

			yield return new WaitForSeconds (1.2f);

			moveBottomLeft = true;

			flipBoss ();

			facingLeft = false;

			yield return new WaitForSeconds (1f);
			Instantiate (lightningSword, shootPoint.position, shootPoint.rotation);
			yield return new WaitForSeconds (1f);
			Instantiate (lightningSword, shootPoint.position, shootPoint.rotation);

			yield return new WaitForSeconds (1.2f);

			moveBottomLeft = false;
			moveTopLeft = true;

		}

		IEnumerator flamingSkyAttack ()
		{
			moveTopLeft = false;
			yield return new WaitForSeconds (7.3f);
			flamingAttack ();
			yield return new WaitForSeconds (.8f);
			flamingAttack ();
			yield return new WaitForSeconds (.8f);
			flamingAttack ();
			yield return new WaitForSeconds (.8f);
			flamingAttack ();

			moveTopLeft = false;
			
		}

		// 3rd attack in rotation
		// Place a circle ball between a set range and after a period of time, fire it in the direction of the user
		IEnumerator orbAttack()
		{
			yield return new WaitForSeconds (11f);
			transform.position = skyPosRight.position;
			flipBoss ();
			moveBottomRight = true;

			yield return new WaitForSeconds (1f);

			counter = 0;
			while (counter != 10) {
				orbPos.position = new Vector3 (Random.Range (22, -15), Random.Range (12, 6), orbPos.position.z);
				GameObject shoot = (GameObject)Instantiate (orb, orbPos.position, orbPos.rotation);
				yield return new WaitForSeconds (0.5f);
				Vector2 direction = playerPos.transform.position - shoot.transform.position;
				
				shoot.GetComponent<orbScript>().setDirection(direction);
				counter++;
			}
		}

		// 4th attack within rotation
		// Place a ball on top of the screen to let the player know where the lightning will strike
		IEnumerator lightningStrikeAttack()
		{
			moveBottomRight = false;
		
			yield return new WaitForSeconds (19f);
			int counterx = 0;
			while (counterx != 5) {
				lightningBallPos.position = new Vector3 (Random.Range(-6f,14.5f), lightningBallPos.position.y, lightningBallPos.position.z);
				lightningStrikePos.position = new Vector3 (lightningBallPos.transform.position.x, lightningStrikePos.position.y, lightningStrikePos.position.z);

				yield return new WaitForSeconds (0.5f);
				Instantiate (lightningBall, lightningBallPos.position, lightningBallPos.rotation);

				yield return new WaitForSeconds (.5f);
				Instantiate (lightningStrike, lightningStrikePos.position, lightningStrikePos.rotation);

				counterx++;

			}
			
		}

		// 5th attack in rotaiton
		// Hurl large fire ball's from the sky down in a random location
		IEnumerator fireFall()
		{
			yield return new WaitForSeconds (25f);
			int counterx = 0;
			while (counterx != 10) {
				fireFallPos.position = new Vector3 (Random.Range (-4.75f, 14.75f), fireFallPos.position.y, fireFallPos.position.z);
				yield return new WaitForSeconds (.3f);
				Instantiate (fireBallFall, fireFallPos.position, fireFallPos.rotation);
				
				counterx++;
			}
		}

		// while the boss has not been defeated, keep rotating through his attacks
		IEnumerator allActions() {
			resetCounter = 29f;
			while (gameObject != null) {
				StartCoroutine (lightningShotAttack ());
				StartCoroutine (flamingSkyAttack ());
				StartCoroutine (orbAttack ());
				StartCoroutine (lightningStrikeAttack ());
				StartCoroutine (fireFall ());
				yield return new WaitForSeconds (resetCounter);
			}
		}

		// When the player shoots the boss, he will lose 1 health per bullet
		// *NOTE: Losing one health per bullet might drag out the fight for too long and reduce the fun
		// May increase bullet damage to 2 or 3
		void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.tag == "Bullet") {
				Demon.Health = Demon.Health - BulletController.damage;
				healthFloat = 1 - Demon.Health/100;
				healthBar.position = Vector3.Lerp(startPos, endPos,healthFloat);
				bossHit = true;
			}
		}

	}
}