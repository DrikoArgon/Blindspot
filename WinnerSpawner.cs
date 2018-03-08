using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerSpawner : MonoBehaviour {

	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject player4Prefab;

	public Transform loserSpawner;
	public Transform loserSpawner2;
	public Transform loserSpawner3;

	private GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	// Use this for initialization
	void Start () {
		if (gameManager.winnerPlayer == PlayerWhoWon.Player1) {

			GameObject winnerPlayer = (GameObject)Instantiate (player1Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
			winnerPlayer.GetComponent<PlayerController> ().winner = true;
			SpawnLosers (1);

		} else if (gameManager.winnerPlayer == PlayerWhoWon.Player2) {

			GameObject winnerPlayer = (GameObject)Instantiate (player2Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
			winnerPlayer.GetComponent<PlayerController> ().winner = true;
			SpawnLosers (2);

		} else if (gameManager.winnerPlayer == PlayerWhoWon.Player3) {

			GameObject winnerPlayer = (GameObject)Instantiate (player3Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
			winnerPlayer.GetComponent<PlayerController> ().winner = true;
			SpawnLosers (3);

		} else {
			
			GameObject winnerPlayer = (GameObject)Instantiate (player4Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
			winnerPlayer.GetComponent<PlayerController> ().winner = true;
			SpawnLosers (4);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpawnLosers(int playerWhoWon){
		if (playerWhoWon == 1) {
			if (gameManager.playerAmount == 2) {
				
				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer2 ();

			} else if (gameManager.playerAmount == 3) {
				
				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer2 ();
				loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer3 ();

			} else if (gameManager.playerAmount == 4) {
				
				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer2 ();
				loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer3 ();
				loserSpawner3.GetComponent<LoserSpawner> ().SpawnPlayer4 ();

			}
		} else if (playerWhoWon == 2) {
			if (gameManager.playerAmount == 2) {

				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer1 ();

			} else if (gameManager.playerAmount == 3) {

				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer1 ();
				loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer3 ();

			} else if (gameManager.playerAmount == 4) {

				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer1 ();
				loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer3 ();
				loserSpawner3.GetComponent<LoserSpawner> ().SpawnPlayer4 ();

			}
		} else if (playerWhoWon == 3) {
			if (gameManager.playerAmount == 3) {

				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer1 ();
				loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer2 ();

			} else if (gameManager.playerAmount == 4) {
				
				loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer1 ();
				loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer2 ();
				loserSpawner3.GetComponent<LoserSpawner> ().SpawnPlayer4 ();

			} 
		} else {
			loserSpawner.GetComponent<LoserSpawner> ().SpawnPlayer1 ();
			loserSpawner2.GetComponent<LoserSpawner> ().SpawnPlayer2 ();
			loserSpawner3.GetComponent<LoserSpawner> ().SpawnPlayer3 ();
		}
	}
}
