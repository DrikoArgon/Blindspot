using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour {


	public GameObject truckToSpawn;
	public float CooldownBetweenSpawns;

	private float spawnTruckTimestamp;

	// Use this for initialization
	void Start () {

		spawnTruckTimestamp = Time.time + CooldownBetweenSpawns;
	}
	
	// Update is called once per frame
	void Update () {

		if (spawnTruckTimestamp < Time.time) {

			Instantiate (truckToSpawn, transform.position, Quaternion.Euler(new Vector3(0,45,0)));

			spawnTruckTimestamp = Time.time + CooldownBetweenSpawns;
		}


	}
}
