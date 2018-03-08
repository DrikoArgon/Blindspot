using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoserSpawner : MonoBehaviour {

	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject player4Prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPlayer1(){
		Instantiate (player1Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
	}

	public void SpawnPlayer2(){
		Instantiate (player2Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
	}

	public void SpawnPlayer3(){
		Instantiate (player3Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
	}

	public void SpawnPlayer4(){
		Instantiate (player4Prefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
	}
}
