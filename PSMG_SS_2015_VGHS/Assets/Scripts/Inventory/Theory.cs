using UnityEngine;
using System.Collections;

/* THEORY CLASS
 * This class offers a method to check 
 * 1. If the combination of two hints is a theory
 * 2. If the theory was already found
 * Theories are ordered by number, so it returns the number of the found theory.
 */

public class Theory : MonoBehaviour {

	public string theory1Text = "Murdered Pat Rutherford?";
	public string theory2Text = "Hit Michael?";

	int theory1 = 1;
	int theory2 = 2;
	public int actualTheory;
	public int newHint;

	string hint1 = "dress";
	string hint2 = "note";
	string hint3 = "scar";
	string hint4 = "family";
	string hint5 = "daughter";
	string hint6 = "picture";

	public bool theory1Found = false;
	public bool theory2Found = false;

	// set up theory depending on the hints that were combined
	public int checkCombination(string first, string second){
		switch (first) {
		case "dress":
			if(second.Equals(hint2)){
				theory1Found = true;
				actualTheory = theory1;
			}
			if(second.Equals(hint3)){
				theory2Found = true;
				actualTheory = theory2;
			}
			break;
		case "note":
			if(second.Equals(hint1)){
				theory1Found = true;
				actualTheory = theory1;
			}
			break;
		case "scar":
			if(second.Equals(hint1)){
				theory2Found = true;
				actualTheory = theory2;
			}
			break;
		case "daughter":
			if(second.Equals(hint6)){
				actualTheory = 8;
			}
			break;
		case "picture":
			if(second.Equals(hint5)){
				actualTheory = 8;
			}
			break;
		default: break;
		}
		return actualTheory;
	}
}
