using UnityEngine;
using System.Collections;

//Look for collisions with sink
public class SinkTrigger : MonoBehaviour {

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

	public bool sinkTriggered(){
		return isTriggered;
	}
}
