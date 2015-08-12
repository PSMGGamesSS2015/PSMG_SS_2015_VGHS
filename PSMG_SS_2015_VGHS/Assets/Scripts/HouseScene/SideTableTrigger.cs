using UnityEngine;
using System.Collections;

public class SideTableTrigger : MonoBehaviour {

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
	
	public bool sideTableTriggered(){
		return isTriggered;
	}
}
