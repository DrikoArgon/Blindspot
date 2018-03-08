using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int playerAmount;
	public int playerReady;

	public bool player1Dead = false;
	public bool player2Dead = false;
	public bool player3Dead = false;
	public bool player4Dead = false;

	public GameObject pressStart1;
	public GameObject pressStart2;
	public GameObject pressStart3;
	public GameObject pressStart4;

	public GameObject player1Spawner;
	public GameObject player2Spawner;
	public GameObject player3Spawner;
	public GameObject player4Spawner;

	public GameObject winnerSpawner;
	public GameObject loserSpawner1;
	public GameObject loserSpawner2;
	public GameObject loserSpawner3;

	public AudioClip pressStartSound1;
	public AudioClip pressStartSound2;

	public string startButtonPlayer1;
	public string startButtonPlayer2;
	public string startButtonPlayer3;
	public string startButtonPlayer4;

	public bool startingScreen;
	public bool endingScreen;
	public PlayerWhoWon winnerPlayer;

	private bool player1PressedStart;
	private bool player2PressedStart;
	private bool player3PressedStart;
	private bool player4PressedStart;
	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		playerReady = 0;

		player1PressedStart = false;
		player2PressedStart = false;
		player3PressedStart = false;
		player4PressedStart = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (startingScreen) {
			
			if (Input.GetButtonDown (startButtonPlayer1) && !player1PressedStart) {
				playerAmount++;
				player1Spawner.GetComponent<StartingScreenSpawner> ().Spawn ();
				player1PressedStart = true;
				int random = Random.Range (1, 3);

				if (random >= 1 && random < 2) {
					source.PlayOneShot (pressStartSound1);
				} else {
					source.PlayOneShot (pressStartSound2);
				}

				Destroy (pressStart1);
			}

			if (Input.GetButtonDown (startButtonPlayer2) && !player2PressedStart) {
				playerAmount++;
				player2Spawner.GetComponent<StartingScreenSpawner> ().Spawn ();
				player2PressedStart = true;

				int random = Random.Range (1, 3);

				if (random >= 1 && random < 2)  {
					source.PlayOneShot (pressStartSound1);
				} else {
					source.PlayOneShot (pressStartSound2);
				}
				Destroy (pressStart2);
			}

			if (Input.GetButtonDown (startButtonPlayer3) && !player3PressedStart) {
				playerAmount++;
				player3Spawner.GetComponent<StartingScreenSpawner> ().Spawn ();
				player3PressedStart = true;

				int random = Random.Range (1, 3);

				if (random >= 1 && random < 2)  {
					source.PlayOneShot (pressStartSound1);
				} else {
					source.PlayOneShot (pressStartSound2);
				}
				Destroy (pressStart3);
			}
			if (Input.GetButtonDown (startButtonPlayer4) && !player4PressedStart) {
				playerAmount++;
				player4Spawner.GetComponent<StartingScreenSpawner> ().Spawn ();
				player4PressedStart = true;

				int random = Random.Range (1, 4);

				if (random >= 1 && random < 2)  {
					source.PlayOneShot (pressStartSound1);
				} else {
					source.PlayOneShot (pressStartSound2);
				}
				Destroy (pressStart4);
			}

			if (playerAmount == playerReady && playerAmount > 1) {
				StartGame ();
			}
		}

		if (endingScreen) {

			if (playerAmount == playerReady && playerAmount > 1) {
				StartGame ();
			}

		}

		if (!startingScreen && !endingScreen) {

			if (!player1Dead && player2Dead && player3Dead && player4Dead) { //Player 1 Wins
				winnerPlayer = PlayerWhoWon.Player1;
				DeclareWinner ();
			} else if (player1Dead && !player2Dead && player3Dead && player4Dead) { // Player 2 Wins
				winnerPlayer = PlayerWhoWon.Player2;
				DeclareWinner ();
			} else if (player1Dead && player2Dead && !player3Dead && player4Dead) { //Player 3 Wins
				winnerPlayer = PlayerWhoWon.Player3;
				DeclareWinner ();
			} else if (player1Dead && player2Dead && player3Dead && !player4Dead) { // Player 4 Wins
				winnerPlayer = PlayerWhoWon.Player4;
				DeclareWinner ();
			} 
		}

	}

	public void StartGame(){

		player1Dead = false;
		player2Dead = false;
		player3Dead = false;
		player4Dead = false;

		if (playerAmount == 2) {
			player3Dead = true;
			player4Dead = true;
		} else if (playerAmount == 3) {
			player4Dead = true;
		}

		endingScreen = false;
		startingScreen = false;
		SceneManager.LoadScene (1);


	}

	public void DeclareWinner(){

		playerReady = 0;
		endingScreen = true;
		SceneManager.LoadScene (2);
	}
}

public enum PlayerWhoWon
{
	Player1,
	Player2,
	Player3,
	Player4
}
