using UnityEngine;
using System.Collections;


namespace UnityStandardAssets._2D {
	public class textPulse : MonoBehaviour {


		private float resetCounter;
		private int check;
		private int check1;

		private GameObject player;
		private Player playerScript;

		private GameObject boss;
		private Boss bossScript;

		// these are unused 
		private float timer;
		private float newVal;

		private void Awake()
		{
			player = GameObject.Find ("Player");
			boss = GameObject.Find ("Boss");
		}

		// Use this for initialization
		void Start () {
			timer = 0f;
			newVal = 0f;
			check = 0;
			check1 = 0;
			resetCounter = 0.15f;
			StartCoroutine (textLarge(1));
			playerScript = player.GetComponent<Player>();
			bossScript = boss.GetComponent<Boss>();
		}
		
		// Update is called once per frame
		void Update () {
			if (check == 0 && playerScript.isGameOver) {
				StartCoroutine (textLarge(Time.timeScale));
				check = 1;
			}

			if (check1 == 0 && bossScript.isDead == true) {
				StartCoroutine (textLarge(Time.timeScale));
				check1 = 1;
			}
		}
		

		IEnumerator textLarge(float times) {

			while (true) {
				int counter = 0;
				while (counter !=35) {
					yield return new WaitForSeconds (.01f*times);
					Vector3 scale = new Vector3 (transform.localScale.x + .005f, transform.localScale.y + .005f, transform.localScale.z);
					transform.localScale = scale;
					counter++;
				}
				counter = 0;

				while (counter != 35) {
					yield return new WaitForSeconds (.01f*times);
					Vector3 scale = new Vector3 (transform.localScale.x - .005f, transform.localScale.y - .005f, transform.localScale.z);
					transform.localScale = scale;
					counter++;
				}
				yield return new WaitForSeconds (resetCounter * Time.timeScale);
			}
		}
		
	}
}
