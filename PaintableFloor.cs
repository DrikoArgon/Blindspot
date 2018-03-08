using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableFloor : MonoBehaviour {


	private SpriteRenderer mySpriteRenderer;
	private AudioSource source;

	void Awake(){
		mySpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PaintBlue(){
		mySpriteRenderer.color = new Color (0, 0, 1);
		source.PlayOneShot (source.clip);
	}

	public void PaintRed(){
		mySpriteRenderer.color = new Color (1, 0, 0);
		source.PlayOneShot (source.clip);
	}

	public void PaintGreen(){
		mySpriteRenderer.color = new Color (0, 1, 0);
		source.PlayOneShot (source.clip);
	}

	public void PaintYellow(){
		mySpriteRenderer.color = new Color (1, 1, 0);
		source.PlayOneShot (source.clip);
	}
}
