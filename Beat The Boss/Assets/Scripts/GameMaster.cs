using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	
	public static GameMaster gm;
	private GameObject player;
	private Player playerScript;

	private void Awake()
	{
		player = GameObject.Find ("Player");
	}
		
	void Start ()
	{
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();
		}
		playerScript = player.GetComponent<Player>();
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2f;

	void Update() {
		if (Input.GetButton ("Restart") && playerScript.isGameOver) {
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
	}
		
	public static void KillPlayer (Player player)
	{
		Destroy (player.gameObject);
	}

}
