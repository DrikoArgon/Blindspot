using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject player4Prefab;


	private Transform playerSpawner1;
	private Transform playerSpawner2;
	private Transform playerSpawner3;
	private Transform playerSpawner4;

	private List<Transform> playerSpawners;
	private GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	// Use this for initialization
	void Start () {

		playerSpawners = new List<Transform> ();

		if (gameManager.playerAmount == 2) {

			playerSpawner1 = GameObject.Find ("PlayerSpawner1").GetComponent<Transform>();
			playerSpawner2 = GameObject.Find ("PlayerSpawner2").GetComponent<Transform>();

			playerSpawners.Add (playerSpawner1);
			playerSpawners.Add (playerSpawner2);

		} else if (gameManager.playerAmount == 3) {

			playerSpawner1 = GameObject.Find ("PlayerSpawner1").GetComponent<Transform>();
			playerSpawner2 = GameObject.Find ("PlayerSpawner2").GetComponent<Transform>();
			playerSpawner3 = GameObject.Find ("PlayerSpawner3").GetComponent<Transform>();

			playerSpawners.Add (playerSpawner1);
			playerSpawners.Add (playerSpawner2);
			playerSpawners.Add (playerSpawner3);

		} else if (gameManager.playerAmount == 4) {

			playerSpawner1 = GameObject.Find ("PlayerSpawner1").GetComponent<Transform>();
			playerSpawner2 = GameObject.Find ("PlayerSpawner2").GetComponent<Transform>();
			playerSpawner3 = GameObject.Find ("PlayerSpawner3").GetComponent<Transform>();
			playerSpawner4 = GameObject.Find ("PlayerSpawner4").GetComponent<Transform>();

			playerSpawners.Add (playerSpawner1);
			playerSpawners.Add (playerSpawner2);
			playerSpawners.Add (playerSpawner3);
			playerSpawners.Add (playerSpawner4);
		}

		for (int i = 0; i < playerSpawners.Count; i++) {

			GameObject prefab = null;

			if (i == 0) {
				prefab = player1Prefab;
			} else if (i == 1) {
				prefab = player2Prefab;
			} else if (i == 2) {
				prefab = player3Prefab;
			} else if (i == 3) {
				prefab = player4Prefab;
			}

			Instantiate (prefab, playerSpawners[i].position, Quaternion.Euler(new Vector3(0,40,0)));
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
