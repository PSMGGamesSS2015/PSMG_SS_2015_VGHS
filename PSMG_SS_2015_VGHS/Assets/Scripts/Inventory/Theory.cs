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
	public string theory3Text = "Pills make me dizzy?";
	public string theory4Text = "Visiting Brother was a lie?";
	public string theory5Text = "Daughter died in car crash?";
	public string theory6Text = "Michael lied!";

	int theory1 = 1;
	int theory2 = 2;
	int theory3 = 3;
	int theory4 = 4;
	int theory5 = 5;
	int theory6 = 6;
	public int actualTheory;
	public int newHint;

	string hint1 = "dress";
	string hint2 = "note";
	string hint3 = "scar";
	string hint4 = "family";
	string hint5 = "daughter";
	string hint6 = "picture";
	string hint7 = "dizzy";
	string hint8 = "pills";
	string hint9 = "family";
	string hint10 = "personalStuff";
	string hint11 = "crash";
	string hint12 = "missingPicture";
	string hint13 = "death";
	string hint14 = "amnesia";

	public bool theory1Found = false;
	public bool theory2Found = false;
	public bool theory3Found = false;
	public bool theory4Found = false;
	public bool theory5Found = false;
	public bool theory6Found = false;

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
				actualTheory = 8; // special case: a hint is added, not a theory
			}
			break;
		case "picture":
			if(second.Equals(hint5)){
				actualTheory = 8; // special case: a hint is added, not a theory
			}
			break;
		case "dizzy":
			if(second.Equals(hint8)){
				theory3Found = true;
				actualTheory = 3;
			}
			break;
		case "pills":
			if(second.Equals(hint7)){
				theory3Found = true;
				actualTheory = 3;
			}
			break;
		case "family":
			if(second.Equals(hint10)){
				theory4Found = true;
				actualTheory = 4;
			}
			break;
		case "personalStuff":
			if(second.Equals(hint9)){
				theory4Found = true;
				actualTheory = 4;
			}
			break;
		case "crash":
			if(second.Equals(hint12)){
				theory5Found = true;
				actualTheory = 5;
			}
			break;
		case "missingPicture":
			if(second.Equals(hint11)){
				theory5Found = true;
				actualTheory = 5;
			}
			break;
		case "death":
			if(second.Equals(hint14)){
				theory6Found = true;
				actualTheory = 6;
			}
			break;
		case "amnesia":
			if(second.Equals(hint13)){
				theory6Found = true;
				actualTheory = 6;
			}
			break;
		default: break;
		}
		return actualTheory;
	}
}
