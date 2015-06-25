using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheorySlots : MonoBehaviour {

	// Use this for initialization
	void Start () {
		switch(gameObject.name){
		case "theory1": 
			gameObject.GetComponentInChildren<Text> ().text = gameObject.transform.parent.GetComponent<Theory> ().theory1Text;
			break;
		default: break;
		}
	}
}
