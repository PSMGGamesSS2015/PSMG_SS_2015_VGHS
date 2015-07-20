using UnityEngine;
using System.Collections;

public class BookshelfTrigger : MonoBehaviour {

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
	
	public bool bookshelfTriggered(){
		return isTriggered;
	}
}
