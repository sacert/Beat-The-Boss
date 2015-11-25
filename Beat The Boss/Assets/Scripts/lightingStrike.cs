using UnityEngine;
using System.Collections;

public class lightingStrike : MonoBehaviour {
	
	private Animator m_Anim;
	
	private void Start()
	{
		// Setting up references.
		m_Anim = GetComponent<Animator>();
	}

	private void Update() {
		m_Anim.SetBool ("strike", true);
	}

	public void strike(bool strike)
	{
		m_Anim.SetBool("strike", strike);
	}
}
