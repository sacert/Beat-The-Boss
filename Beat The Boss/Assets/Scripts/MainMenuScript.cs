using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GameObject PlayButton;
	public GameObject ExitButton;
	public GameObject Arrow;

	public Transform PlayPos;
	public Transform ExitPos;

	// 0 for Play, 1 for Exit
	private int selection;

	// Use this for initialization
	void Start () {
		selection = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			selection = 1;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			selection = 0;
		}

		if(selection == 0) {
			Arrow.transform.position = PlayPos.position;
		}
		else {
			Arrow.transform.position = ExitPos.position;
		}

		if(Input.GetKeyDown(KeyCode.Return)){
			if(selection == 0){
				Application.LoadLevel("Beat The Boss");
			}
			else {
				Application.Quit();
			}
		}
	}
}
