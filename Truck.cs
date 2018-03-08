using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {

	public float truckSpeed;

	public Vector3 forward, right;

	public DirectionToDrive directionToDrive;

	private SpriteRenderer mySpriteRenderer;

	private AudioSource source;

	public enum DirectionToDrive
	{
		upRight,
		UpLeft

	}

	void Awake(){
		mySpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {

		forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
		forward.y = 0; // make sure y is 0
		forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
		source.PlayOneShot (source.clip);

		Destroy (gameObject, 20);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 rightMovement = right * truckSpeed * Time.deltaTime; // Our right movement is based on the right vector, movement speed, and our GetAxis command. We multiply by Time.deltaTime to make the movement smooth.
		Vector3 upMovement = forward * truckSpeed * Time.deltaTime; 

		if (directionToDrive == DirectionToDrive.upRight) {
			transform.position += upMovement;
			transform.position += rightMovement;
		} else {
			transform.position += upMovement;
			transform.position -= rightMovement;
		}

	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") {
			PlayerController player = other.GetComponent<PlayerController> ();

			player.Death ();
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
