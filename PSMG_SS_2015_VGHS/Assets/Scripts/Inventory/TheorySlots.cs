using UnityEngine;
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
		case "theory2": 
			gameObject.GetComponentInChildren<Text> ().text = gameObject.transform.parent.GetComponent<Theory> ().theory2Text;
			break;
		case "theory3": 
			gameObject.GetComponentInChildren<Text> ().text = gameObject.transform.parent.GetComponent<Theory> ().theory3Text;
			break;
		case "theory4": 
			gameObject.GetComponentInChildren<Text> ().text = gameObject.transform.parent.GetComponent<Theory> ().theory4Text;
			break;
		case "theory5":
			gameObject.GetComponentInChildren<Text> ().text = gameObject.transform.parent.GetComponent<Theory> ().theory5Text;
			break;
		default: break;
		}
	}
}
