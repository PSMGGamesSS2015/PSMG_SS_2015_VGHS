using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/* BATH CONTROLLER
 * This is the controlling script that handles all the logic performed during the bath scene (first level).
 */

public class BathController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
	public GameObject jacket;
	public GameObject jacketOn;
	public GameObject theory;

	int sinkCounter = 0;

	bool noteFound = false;
	bool theory1Registered = false;


	// Use this for initialization
	void Start () {
		guiController.toggleSubtl ("entry");
	}
	
	// Update is called once per frame
	void Update () {
        getKeyInteractions();
        checkInventory();
        checkCollisions();
	}

	//get all the key interactions
	void getKeyInteractions(){

		// special effects when space is hit
		if (Input.GetKeyDown (KeyCode.Space)) {
			// special case: check what sink interaction has been made to toggle the jacket
			if(sinkCounter == 1){
				jacket.SetActive(true);
				jacketOn.SetActive(false);
			}

			// change level when final interaction was made and last subtitle disappeard
			if(theory1Registered){
				ChangeLevel(2);
			}
		}

		// handle 'E' interactions
		if (Input.GetKeyDown (KeyCode.E)) {
			// interactions are only possible if nothing like subtitles or the inventory is shown
			if (guiController.isShowing () == false) {
				// do sth. depending on trigger
				switch (gameObject.GetComponent<TriggerController> ().getTriggerTag()) {
				case "Sink": // react to sink interaction depending on the interacted time
					if (sinkCounter < 2) {
						switch (sinkCounter) {
						case 0: 
							guiController.toggleSubtl ("mirror1");
							sinkCounter++;
							break;
						case 1:
							guiController.toggleSubtl ("mirror2");
							guiController.toggleInventoryHint ();
							guiController.addHint ("dressHint");
							sinkCounter++;
							break;
						}
						break;
					}
					break;
				case "Jacket": // handle interaction with jacket
					if(!noteFound){
						guiController.toggleSubtl ("paper");
						guiController.toggleInventoryHint ();
						guiController.addHint ("noteHint");
						noteFound = true;
					}
					break;
				}
			}
		}
	}


	// check if the player collides with an interactable object and show/unshow interaction hint
	void checkCollisions(){
		//interaction hints may not be shown if sth else is shown on GUI
		if (!guiController.isShowing ()) {
			switch(gameObject.GetComponent<TriggerController> ().getTriggerTag()){
			case "Sink":
				if(sinkCounter < 2){
					guiController.toggleInteractionHint (true);
				} else {
					guiController.toggleInteractionHint (false);
				}
				break;
			case "Jacket":
				if(!noteFound){
					guiController.toggleInteractionHint (true);
				} else {
					guiController.toggleInteractionHint (false);
				}	
				break;
			default: 
				guiController.toggleInteractionHint (false);
				break;
			}
		} else {
			guiController.toggleInteractionHint (false);
		}	
	}

	// handle Level changing stuff triggered by actions in the inventory here
	void checkInventory(){
		if(theory.GetComponent<Theory>().theory1Found && theory1Registered == false){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory1");
			theory1Registered = true;
		}
	}

	// initialize level change
	public void ChangeLevel(int level){
        GetComponent<SceneFader>().SwitchScene(level);
    }
}
