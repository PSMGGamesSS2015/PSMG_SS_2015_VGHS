﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* THEORY SLOT CLASS
 * This script is programmatically attached to every new theory slot.
 */
public class TheorySlots : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// when a new theory slot is opened it displays the correct text depending on its name.
		switch(gameObject.name){
		case "theory1": 
			gameObject.GetComponentInChildren<Text> ().text = gameObject.transform.parent.GetComponent<Theory> ().theory1Text;
			break;
		default: break;
		}
	}
}
