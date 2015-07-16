using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/* BATH CONTROLLER
 * This is the controlling script that handles all the logic performed during the bath scene (first level).
 */

public class BathController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
	public GameObject sink;
	public GameObject jacketTrigger;
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

			//special case: check what sink interaction has been made to toggle the jacket
			if(sinkCounter == 1){
				jacket.SetActive(true);
				jacketOn.SetActive(false);
			}

			//change level when final interaction was made
			if(theory1Registered){
				ChangeLevel(2);
			}
		}

		// handle 'E' interactions
		if (Input.GetKeyDown (KeyCode.E)) {
			// interactions are only possible if nothing like subtitles or the inventory is shown
			if(guiController.isShowing() == false){
				// interacted with sink
				if (sink.GetComponent<SinkTrigger>().sinkTriggered() && sinkCounter < 2) {
					switch (sinkCounter){
					case 0: 
						guiController.toggleSubtl("mirror1");
						sinkCounter++;
						break;
					case 1:
						guiController.toggleSubtl("mirror2");
						guiController.toggleInventoryHint();
						guiController.addHint("dressHint");
						sinkCounter++;
						break;
					default: break;
					}
				}
			
				// interacted with jacket
				else if(jacketTrigger.GetComponent<JacketTrigger>().jacketTriggered() && noteFound == false){
					guiController.toggleSubtl("paper");
					guiController.toggleInventoryHint();
					guiController.addHint("noteHint");
					noteFound = true;
				}
			}
		}
	}


	//check if the player collides with an interactable object
	void checkCollisions(){
		//handle sink collision (is only two times interactable)
		if (sink.GetComponent<SinkTrigger> ().sinkTriggered () && sinkCounter < 2 && guiController.isShowing () == false) {
			guiController.toggleInteractionHint (true);
		}
		//handle jacket collision (is only one time interactable)
		else if (jacketTrigger.GetComponent<JacketTrigger> ().jacketTriggered () && noteFound == false && guiController.isShowing () == false) {
			guiController.toggleInteractionHint (true);	
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

	public void ChangeLevel(int level){
        //Application.LoadLevel(level);
        GetComponent<SceneFader>().SwitchScene(level);
    }
}
