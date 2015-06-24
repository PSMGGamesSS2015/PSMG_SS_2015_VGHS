using UnityEngine;
using System.Collections;

//Look for collision with jacket
public class JacketTrigger : MonoBehaviour {

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
	
	public bool jacketTriggered(){
		return isTriggered;
	}
}