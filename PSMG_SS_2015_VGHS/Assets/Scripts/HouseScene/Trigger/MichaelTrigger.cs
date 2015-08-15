using UnityEngine;
using System.Collections;

//Look for collisions with michael
public class MichaelTrigger : MonoBehaviour {
	
	bool isTriggered;
	
	
	void OnTriggerEnter(Collider other){		
		if (other.tag == "PlayerCharacter"){
			isTriggered = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.tag == "PlayerCharacter"){
			isTriggered = false;
		}
	}
	
	public bool michaelTriggered(){
		return isTriggered;
	}
}
