using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreenPlayer : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 4f; //Change in inspector to adjust move speed
	[SerializeField]
	string horizontalKey;
	[SerializeField]
	string verticalKey;
	[SerializeField]
	GameObject bulletPrefab;
	[SerializeField]
	Transform firePointTransform;
	[SerializeField]
	string firingKey;
	[SerializeField]
	string blueColorKey;
	[SerializeField]
	string redColorKey;
	[SerializeField]
	string greenColorKey;
	[SerializeField]
	string yellowColorKey;

	public Vector3 forward, right; // Keeps track of our relative forward and right vectors
	public SpriteRenderer mySpriteRenderer;
	public int maxLife = 3;
	public int currentLife;

	public int flashDelay = 1;
	public float invulnerableSeconds;
	public bool invulnerable;
	private int flashingCounter;
	private bool toggleFlashing = false;
	private float invulnerableTimeStamp;
	private int walkingDirection; 

	private GameManager gameManager;
	private Animator animator;
	private bool alreadyShot;
	public SelectedColor myColor;
	public WhichPlayerAmI whichPlayerAmI;

	private bool dead;

	public enum WhichPlayerAmI{
		Player1,
		Player2,
		Player3,
		Player4
	}

	void Awake(){
		animator = GetComponentInChildren<Animator> ();
		mySpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	void Start()
	{
		forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
		forward.y = 0; // make sure y is 0
		forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector
		currentLife = maxLife;

		invulnerableSeconds = 2f;
		invulnerableTimeStamp = 0;
		invulnerable = true;

		walkingDirection = 5;
		alreadyShot = false;

	}

	void Update()
	{
		
			if (Input.GetAxis (verticalKey) != 0 || Input.GetAxis (horizontalKey) != 0) { // only execute if a key is being pressed
				animator.SetBool ("idle", false);
				Move ();
			} else {
				animator.SetBool ("idle", true);
			}

			if (Input.GetAxis (firingKey) == 1 && !alreadyShot) {
				alreadyShot = true;
				Shoot ();
			}

			if (Input.GetAxis (firingKey) < 0 && alreadyShot) {
				alreadyShot = false;
			}

			if (Input.GetButtonDown (blueColorKey)) {
				PaintBlue ();
			}

			if (Input.GetButtonDown (redColorKey)) {
				PaintRed ();
			}

			if (Input.GetButtonDown (greenColorKey)) {
				PaintGreen ();
			}

			if (Input.GetButtonDown (yellowColorKey)) {
				PaintYellow ();
			}

			if (invulnerableTimeStamp < Time.time) {
				invulnerable = false;
				mySpriteRenderer.enabled = true;
			}

			if (!dead) {
				if (currentLife <= 0) {
					dead = true;

				}
			}
	}

	void Move()
	{
		Vector3 direction = new Vector3(Input.GetAxis(horizontalKey), 0, Input.GetAxis(verticalKey)); // setup a direction Vector based on keyboard input. GetAxis returns a value between -1.0 and 1.0. If the A key is pressed, GetAxis(HorizontalKey) will return -1.0. If D is pressed, it will return 1.0
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis(horizontalKey); // Our right movement is based on the right vector, movement speed, and our GetAxis command. We multiply by Time.deltaTime to make the movement smooth.
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis(verticalKey); // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement); // This creates our new direction. By combining our right and forward movements and normalizing them, we create a new vector that points in the appropriate direction with a length no greater than 1.0
		transform.forward = heading; // Sets forward direction of our game object to whatever direction we're moving in
		transform.position += rightMovement; // move our transform's position right/left
		transform.position += upMovement; // Move our transform's position up/down

		if (Input.GetAxis (verticalKey) >= 0.5f && Input.GetAxis (horizontalKey) < 0.1f  && Input.GetAxis (horizontalKey) > -0.1f) { //Up
			animator.SetInteger("direction",1);
			walkingDirection = 1;
		} else if (Input.GetAxis (verticalKey) >= 0.5f && Input.GetAxis (horizontalKey) >= 0.5f ) { //UpRight
			animator.SetInteger("direction",5);
			walkingDirection = 5;
		} else if (Input.GetAxis (verticalKey) >= 0.5f  && Input.GetAxis (horizontalKey) <= -0.1f ) { // UpLeft
			animator.SetInteger("direction",6);
			walkingDirection = 6;
		} else if (Input.GetAxis (verticalKey) <= -0.5f && Input.GetAxis (horizontalKey) < 0.1f  && Input.GetAxis (horizontalKey) > -0.1f) { // Down
			animator.SetInteger("direction",3);
			walkingDirection = 3;
		} else if (Input.GetAxis (verticalKey) <= -0.5f && Input.GetAxis (horizontalKey) >= 0.5f) { // DownRight
			animator.SetInteger("direction",8);
			walkingDirection = 8;
		} else if (Input.GetAxis (verticalKey) <= -0.5f && Input.GetAxis (horizontalKey) <= -0.1f) { // DownLeft
			animator.SetInteger("direction",7);
			walkingDirection = 7;
		} else if (Input.GetAxis (verticalKey) < 0.1f && Input.GetAxis (horizontalKey) >= 0.5f) { // Right
			animator.SetInteger("direction",2);
			walkingDirection = 2;
		} else if (Input.GetAxis (verticalKey) < 0.1f && Input.GetAxis (horizontalKey) <= -0.5f) { // Left
			animator.SetInteger("direction",4);
			walkingDirection = 4;
		} 

	}

	void Shoot(){
		if(walkingDirection == 1){ // Up
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(90,315,0)));
		} else if(walkingDirection == 2){ // Right
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,45,0)));
		} else if(walkingDirection == 3){ // Down
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(90,135,0)));
		} else if(walkingDirection == 4){ // Left
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,225,0)));
		} else if(walkingDirection == 5){ // UpRight
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.identity);
		} else if(walkingDirection == 6){ // UpLeft
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,270,0)));
		} else if(walkingDirection == 7){ // DownLeft
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,180,0)));
		} else { // DownRight
			Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,90,0)));
		}
	}

	void PaintBlue(){
		mySpriteRenderer.color = new Color (0, 0, 1);
		myColor = SelectedColor.Blue;
	}

	void PaintRed(){
		mySpriteRenderer.color = new Color (1, 0, 0);
		myColor = SelectedColor.Red;
	}

	void PaintGreen(){
		mySpriteRenderer.color = new Color (0, 1, 0);
		myColor = SelectedColor.Green;
	}

	void PaintYellow(){
		mySpriteRenderer.color = new Color (1, 1, 0);
		myColor = SelectedColor.Yellow;
	}

//	public void TakeDamage(){
//
//		currentLife--;
//
//
//		if (currentLife > 0) {
//			ToggleInvinsibility ();
//		}
//
//	}

	public void Death(){
		currentLife = 0;
	}
		

}
	
