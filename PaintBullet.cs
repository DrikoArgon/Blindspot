using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBullet : MonoBehaviour {

	[SerializeField]
	float speed;
	[SerializeField]
	float timeToDestroy;

	public Vector3 directionToFire;
	public SelectedColor selectedColor;
	public PlayerWhoOwnsTheBullet player;
	public bool startingScreen;
	public bool endingScreen;

	public AudioClip shotSound1;
	public AudioClip shotSound2;
	public AudioClip shotSound3;
	public AudioClip shotSound4;
	public AudioClip shotSound5;
	public AudioClip shotSound6;
	public GameObject shotHitPrefab;

	public int direction;

	private SpriteRenderer mySpriteRenderer;
	private AudioSource source;
	private PlayerController playerController;

	public enum PlayerWhoOwnsTheBullet 
	{
		Player1,
		Player2,
		Player3,
		Player4
	}

	void Awake(){

		source = GetComponent<AudioSource> ();
		mySpriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		if (!startingScreen && !endingScreen) {
			if (player == PlayerWhoOwnsTheBullet.Player1) {
				playerController = GameObject.Find ("Player(Clone)").GetComponent<PlayerController> ();
			} else if (player == PlayerWhoOwnsTheBullet.Player2) {
				playerController = GameObject.Find ("Player2(Clone)").GetComponent<PlayerController> ();
			} else if (player == PlayerWhoOwnsTheBullet.Player3) {
				playerController = GameObject.Find ("Player3(Clone)").GetComponent<PlayerController> ();
			} else {
				playerController = GameObject.Find ("Player4(Clone)").GetComponent<PlayerController> ();
			}
		} else if (startingScreen && !endingScreen) {
			if (player == PlayerWhoOwnsTheBullet.Player1) {
				playerController = GameObject.Find ("StartingScreenPlayer(Clone)").GetComponent<PlayerController> ();
			} else if (player == PlayerWhoOwnsTheBullet.Player2) {
				playerController = GameObject.Find ("StartingScreenPlayer2(Clone)").GetComponent<PlayerController> ();
			} else if (player == PlayerWhoOwnsTheBullet.Player3) {
				playerController = GameObject.Find ("StartingScreenPlayer3(Clone)").GetComponent<PlayerController> ();
			} else {
				playerController = GameObject.Find ("StartingScreenPlayer4(Clone)").GetComponent<PlayerController> ();
			}
		} else {
			if (player == PlayerWhoOwnsTheBullet.Player1) {
				playerController = GameObject.Find ("EndingScreenPlayer(Clone)").GetComponent<PlayerController> ();
			} else if (player == PlayerWhoOwnsTheBullet.Player2) {
				playerController = GameObject.Find ("EndingScreenPlayer2(Clone)").GetComponent<PlayerController> ();
			} else if (player == PlayerWhoOwnsTheBullet.Player3) {
				playerController = GameObject.Find ("EndingScreenPlayer3(Clone)").GetComponent<PlayerController> ();
			} else {
				playerController = GameObject.Find ("EndingScreenPlayer4(Clone)").GetComponent<PlayerController> ();
			}
		}

		selectedColor = playerController.myColor;

		if (selectedColor == SelectedColor.Blue) {
			PaintBlue ();
		} else if (selectedColor == SelectedColor.Green) {
			PaintGreen ();
		} else if (selectedColor == SelectedColor.Red) {
			PaintRed ();
		} else if (selectedColor == SelectedColor.Yellow) {
			PaintYellow ();
		} else {
			mySpriteRenderer.color = new Color (1, 1, 1, 1);
		}

		directionToFire = playerController.transform.forward;

		GetComponent<ConstantForce> ().force = new Vector3 (0, 6.0f, 0);
	}


	// Use this for initialization
	void Start () {

		int random = Random.Range (1, 6);

		if (random == 1) {
			source.PlayOneShot (shotSound1);
		} else if (random == 2) {
			source.PlayOneShot (shotSound2);
		} else if (random == 3) {
			source.PlayOneShot (shotSound3);
		} else if (random == 4) {
			source.PlayOneShot (shotSound4);
		} else if (random == 5) {
			source.PlayOneShot (shotSound5);
		} else {

		}
			
		Destroy (gameObject, timeToDestroy);
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody> ().transform.position += directionToFire * speed * Time.deltaTime;

	}

	void OnTriggerEnter(Collider other){

		PlayerController playerHit;
		
		if (player == PlayerWhoOwnsTheBullet.Player1) {

			if (other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") {

				playerHit = other.GetComponent<PlayerController> ();

				if (!playerHit.invulnerable) {
					playerHit.TakeDamage ();
				}

				if (direction == 1) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,90)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 2) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 3) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 4) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 5) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 6) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 7) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				}

				Destroy (gameObject);
			}

		} else if (player == PlayerWhoOwnsTheBullet.Player2) {
			if (other.tag == "Player1" || other.tag == "Player3" || other.tag == "Player4") {
				playerHit = other.GetComponent<PlayerController> ();

				if (!playerHit.invulnerable) {
					playerHit.TakeDamage ();
				}

				if (direction == 1) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,90)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 2) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 3) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 4) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 5) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 6) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 7) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				}

				Destroy (gameObject);
			} 


		} else if (player == PlayerWhoOwnsTheBullet.Player3) {
			if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player4") {
				playerHit = other.GetComponent<PlayerController> ();

				if (!playerHit.invulnerable) {
					playerHit.TakeDamage ();
				}

				if (direction == 1) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,90)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 2) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 3) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 4) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 5) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 6) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 7) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				}

				Destroy (gameObject);
			}
				

		} else {
			if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3"){
				playerHit = other.GetComponent<PlayerController> ();

				if (!playerHit.invulnerable) {
					playerHit.TakeDamage ();
				}

				if (direction == 1) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,90)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 2) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 3) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 4) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 5) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 6) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else if (direction == 7) {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				} else {
					GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
					effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
				}

				Destroy (gameObject);
			}
				
		}


		if (other.tag == "PaintableFloor") {

			PaintableFloor floor = other.GetComponent<PaintableFloor> ();

			if (selectedColor == SelectedColor.Blue) {
				floor.PaintBlue ();
			} else if (selectedColor == SelectedColor.Green) {
				floor.PaintGreen ();
			} else if (selectedColor == SelectedColor.Red) {
				floor.PaintRed ();
			} else {
				floor.PaintYellow ();
			}

			GameObject effect = (GameObject)Instantiate (shotHitPrefab, transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
			effect.GetComponent<ShotHit> ().selectedColor = selectedColor;

			Destroy (gameObject);
		}

		if (other.tag == "Truck") {

			Truck truck = other.GetComponent<Truck> ();

			if (selectedColor == SelectedColor.Blue) {
				truck.PaintBlue ();
			} else if (selectedColor == SelectedColor.Green) {
				truck.PaintGreen ();
			} else if (selectedColor == SelectedColor.Red) {
				truck.PaintRed ();
			} else {
				truck.PaintYellow ();
			}

			if (direction == 1) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,90)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else if (direction == 2) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else if (direction == 3) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else if (direction == 4) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else if (direction == 5) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else if (direction == 6) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else if (direction == 7) {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,180)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			} else {
				GameObject effect = (GameObject)Instantiate (shotHitPrefab, other.transform.position, Quaternion.Euler (new Vector3 (0,45,0)));
				effect.GetComponent<ShotHit> ().selectedColor = selectedColor;
			}

			Destroy (gameObject);
		}

		if (other.tag == "Ground") {

			GameObject effect = (GameObject)Instantiate (shotHitPrefab, transform.position, Quaternion.Euler (new Vector3 (0,45,270)));
			effect.GetComponent<ShotHit> ().selectedColor = selectedColor;

			Destroy (gameObject);
		}

	}

	public void PaintBlue(){
		mySpriteRenderer.color = new Color (0, 0, 1);
	}

	public void PaintRed(){
		mySpriteRenderer.color = new Color (1, 0, 0);
	}

	public void PaintGreen(){
		mySpriteRenderer.color = new Color (0, 1, 0);
	}

	public void PaintYellow(){
		mySpriteRenderer.color = new Color (1, 1, 0);
	}

}
