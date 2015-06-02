﻿using UnityEngine;
using System.Collections;

public class Subtitles : MonoBehaviour {

	bool showSubtl = true;
	

	void OnGUI(){

		//show Janes first message when entering scene; disable movement while message is shown
		if (showSubtl) 
		{
			Time.timeScale = 0;
			GUI.Box (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 12, 700, 25), "Was passiert hier… ? Oh mein Gott, was ist hier los? Wasser… ich brauche Wasser! (Press 'Leer' to close window)");
		}

		//enable movement and hide message when player presses 'Space'
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Time.timeScale = 1;
			showSubtl = false;
		}
	}
}
