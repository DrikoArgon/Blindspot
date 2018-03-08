using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreenSpawner : MonoBehaviour {

	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawn(){

		Instantiate (playerPrefab, transform.position, Quaternion.Euler (new Vector3 (0, 41, 0)));
	}
}
