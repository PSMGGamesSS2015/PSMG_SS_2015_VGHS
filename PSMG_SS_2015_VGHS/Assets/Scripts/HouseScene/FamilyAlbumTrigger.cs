using UnityEngine;
using System.Collections;

public class FamilyAlbumTrigger : MonoBehaviour {

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
	
	public bool albumTriggered(){
		return isTriggered;
	}
}
