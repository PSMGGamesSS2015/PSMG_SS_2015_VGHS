using UnityEngine;
using System.Collections;

public class TriggerScipt : MonoBehaviour {

	GameObject triggerController;

	// initialize controlling
	void Start(){
		triggerController = GameObject.FindGameObjectWithTag("Controlling");
	}

	// setup tag of trigger if entered
	void OnTriggerEnter(Collider other){		
		if (other.tag.Equals("PlayerCharacter")){
			triggerController.GetComponent<TriggerController>().setTriggerTag(gameObject.tag);
		}
	}

	// set empty tag if no trigger entered
	void OnTriggerExit(Collider other){
		if (other.tag.Equals("PlayerCharacter")){
			triggerController.GetComponent<TriggerController>().setTriggerTag("");
		}
	}


}
