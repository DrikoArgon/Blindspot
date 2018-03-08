using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDisplayCheck : MonoBehaviour {

	public SpriteRenderer player3LifeDisplay;
	public SpriteRenderer player4LifeDisplay;

	public Sprite noHeartsSpritePlayer3;
	public Sprite noHeartsSpritePlayer4;

	private GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	// Use this for initialization
	void Start () {
		if (gameManager.playerAmount < 3) {
			player3LifeDisplay.sprite = noHeartsSpritePlayer3;
			player4LifeDisplay.sprite = noHeartsSpritePlayer4;
		} else if (gameManager.playerAmount == 3) {
			player4LifeDisplay.sprite = noHeartsSpritePlayer4;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
