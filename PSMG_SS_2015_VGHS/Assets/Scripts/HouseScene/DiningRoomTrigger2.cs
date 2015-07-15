using UnityEngine;
using System.Collections;

public class DiningRoomTrigger2 : MonoBehaviour {

	bool isTriggered;
	
	
	void OnTriggerEnter(Collider other){		
		if (other.tag == "PlayerCharacter"){
			Debug.Log ("diningRoom");
			isTriggered = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.tag == "PlayerCharacter"){
			isTriggered = false;
		}
	}
	
	public bool diningRoomTriggered(){
		return isTriggered;
	}
}
