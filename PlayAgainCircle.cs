using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainCircle : MonoBehaviour {

	private GameManager gameManager;
	private Animator animator;

	void Awake(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		animator = GetComponentInChildren<Animator> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") {
			gameManager.playerReady++;
			animator.SetBool ("playerInside", true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") {
			gameManager.playerReady--;
			animator.SetBool ("playerInside", false);
		}
	}
}

