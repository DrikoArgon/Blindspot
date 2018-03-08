using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHit : MonoBehaviour {

	public SpriteRenderer mySpriteRenderer;
	public SelectedColor selectedColor;

	// Use this for initialization
	void Start () {
		mySpriteRenderer = GetComponent<SpriteRenderer> ();

		if (selectedColor == SelectedColor.Blue) {
			PaintBlue ();
		} else if (selectedColor == SelectedColor.Red) {
			PaintRed ();
		} else if (selectedColor == SelectedColor.Green) {
			PaintGreen ();
		} else if (selectedColor == SelectedColor.Yellow) {
			PaintYellow ();
		} else {
			mySpriteRenderer.color = new Color (1, 1, 1, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Disappear(){
		Destroy(gameObject);
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
