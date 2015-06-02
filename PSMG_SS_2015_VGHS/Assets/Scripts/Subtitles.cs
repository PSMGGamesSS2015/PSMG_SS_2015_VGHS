using UnityEngine;
using System.Collections;

public class Subtitles : MonoBehaviour {

	bool showSubtl = true;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		//show Janes first message when entering scene; disable movement while message is shown
		if (showSubtl == true) {
			Time.timeScale = 0;
			GUI.Box (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 12, 700, 25), "Was passiert hier… ? Oh mein Gott, was ist hier los? Wasser… ich brauche Wasser! (Press 'E' to close window)");

		}

		//enable movement and hide message when player presses 'E'
		if (Input.GetKeyDown (KeyCode.E)) {
			Time.timeScale = 1;
			Debug.Log ("E pressed");
			showSubtl = false;
		}
	}
}
