using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
	public int maxLife = 2;
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
	private AudioSource source;

	public SelectedColor myColor;
	public WhichPlayerAmI whichPlayerAmI;
	public GameObject lifeDisplay;
	public Sprite lifeDisplay2Hearts;
	public Sprite lifeDisplay1Heart;
	public Sprite lifeDisplayNoHearts;
	public AudioClip deathSound;
	public AudioClip hitSound;

	public bool startingScreen;
	public bool endingScreen;

	public bool winner;

	private bool winnerFlashingCicleEnded;

	private bool dead;
	private bool alreadyShot;

	public enum WhichPlayerAmI{
		Player1,
		Player2,
		Player3,
		Player4
	}

	void Awake(){
		
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		animator = GetComponentInChildren<Animator> ();
		mySpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		source = GetComponent<AudioSource> ();

		if (whichPlayerAmI == WhichPlayerAmI.Player1) {
			lifeDisplay = GameObject.Find ("LifeP1");
		} else if (whichPlayerAmI == WhichPlayerAmI.Player2) {
			lifeDisplay = GameObject.Find ("LifeP2");
		} else if (whichPlayerAmI == WhichPlayerAmI.Player3) {
			lifeDisplay = GameObject.Find ("LifeP3");
		} else {
			lifeDisplay = GameObject.Find ("LifeP4");
		}
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
		invulnerable = false;

		walkingDirection = 5;
		alreadyShot = false;

		if (!startingScreen && !endingScreen) {
			lifeDisplay.GetComponent<SpriteRenderer> ().sprite = lifeDisplay2Hearts;
		}

		if (startingScreen || endingScreen) {
			invulnerable = true;
		}

		if (endingScreen) {
			if (winner) {
				StartCoroutine (WinnerFlashing ());
			} else {
				mySpriteRenderer.color = new Color (1, 1, 1, 1);
				myColor = SelectedColor.None; 
			}
		}
	}

	void Update()
	{
		if (!dead) {
			
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

			if (!endingScreen) {

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
			}

			if (!startingScreen && !endingScreen) {
				if (invulnerableTimeStamp < Time.time) {
					invulnerable = false;
					mySpriteRenderer.enabled = true;
				}

				if (invulnerable) {
					Flash ();
				}
			}

			if (currentLife <= 0) {
				mySpriteRenderer.color = new Color (1, 1, 1, 1);
				lifeDisplay.GetComponent<SpriteRenderer> ().sprite = lifeDisplayNoHearts;
				source.PlayOneShot (deathSound);
				dead = true;

				animator.SetBool ("dead", true);
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
		GetComponent<Rigidbody>().transform.position += rightMovement; // move our transform's position right/left
		GetComponent<Rigidbody>().transform.position += upMovement; // Move our transform's position up/down

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
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,45,0)));
			bullet.GetComponent<PaintBullet> ().direction = 1;
		} else if(walkingDirection == 2){ // Right
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,45,0)));
			bullet.GetComponent<PaintBullet> ().direction = 2;
		} else if(walkingDirection == 3){ // Down
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,45,0)));
			bullet.GetComponent<PaintBullet> ().direction = 3;
		} else if(walkingDirection == 4){ // Left
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,225,0)));
			bullet.GetComponent<PaintBullet> ().direction = 4;
		} else if(walkingDirection == 5){ // UpRight
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,45,0)));
			bullet.GetComponent<PaintBullet> ().direction = 5;
		} else if(walkingDirection == 6){ // UpLeft
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,225,0)));
			bullet.GetComponent<PaintBullet> ().direction = 6;
		} else if(walkingDirection == 7){ // DownLeft
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,225,0)));
			bullet.GetComponent<PaintBullet> ().direction = 7;
		} else { // DownRight
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, firePointTransform.position, Quaternion.Euler(new Vector3(0,45,0)));
			bullet.GetComponent<PaintBullet> ().direction = 8;
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

	public void TakeDamage(){

		currentLife--;

		if (currentLife == 1) {
			lifeDisplay.GetComponent<SpriteRenderer> ().sprite = lifeDisplay1Heart;
		} 

		if (currentLife > 0) {
			source.PlayOneShot (hitSound);
			ToggleInvinsibility ();
		}

	}

	public void Death(){
		currentLife = 0;
	}

	public void Flash(){


		if(flashingCounter >= flashDelay){ 

			flashingCounter = 0;

			toggleFlashing = !toggleFlashing;

			if(toggleFlashing) {
				mySpriteRenderer.enabled = true;
			}
			else {
				mySpriteRenderer.enabled = false;
			}

		}
		else {
			flashingCounter++;
		}

	}


	private void ToggleInvinsibility(){
		invulnerable = true;
		invulnerableTimeStamp = Time.time + invulnerableSeconds;
	}

	public void Disappear(){

		if (whichPlayerAmI == WhichPlayerAmI.Player1) {
			gameManager.player1Dead = true;
		} else if (whichPlayerAmI == WhichPlayerAmI.Player2) {
			gameManager.player2Dead = true;
		} else if (whichPlayerAmI == WhichPlayerAmI.Player3) {
			gameManager.player3Dead = true;
		} else {
			gameManager.player4Dead = true;
		}

		Destroy (gameObject);
	}

	public IEnumerator WinnerFlashing(){

		while (true) {

			PaintRed ();
			yield return new WaitForSeconds (0.2f);
			PaintBlue ();
			yield return new WaitForSeconds (0.2f);
			PaintGreen ();
			yield return new WaitForSeconds (0.2f);
			PaintYellow ();
			yield return new WaitForSeconds (0.2f);
		}

	}

//	void OnCollisionEnter(Collision other){
//
//		if (other.gameObject.tag == "Boundarie") {
//			print ("Colisão com o limite da fase");
//
//		}
//	}

}

public enum SelectedColor
{
	Blue,
	Red,
	Green,
	Yellow,
	None
}
