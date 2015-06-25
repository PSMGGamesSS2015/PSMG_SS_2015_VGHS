using UnityEngine;
using System.Collections;

public class Theory : MonoBehaviour {

	public string theory1Text = "Murdered Pat Rutherford?";

	int theory1 = 1;
	int actualTheory;

	string hint1 = "dress";
	string hint2 = "note";

	public bool theory1Found = false;

	public int getTheory(string first, string second){
		switch (first) {
		case "dress":
			if(second.Equals(hint2)){
				theory1Found = true;
				actualTheory = theory1;
			}
			break;
		case "note":
			if(second.Equals(hint1)){
				theory1Found = true;
				actualTheory = theory1;
			}
			break;
		default: break;
		}
		return actualTheory;
	}
}
