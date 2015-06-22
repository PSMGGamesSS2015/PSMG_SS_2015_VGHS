using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/* MAIN CONTROLLER
 * This is the games main controlling Script. 
 * It receives event based data from other scripts (like where collisions are triggered).
 * Its task is to store and process this data and to communicate the results to other scripts.
 * So every logic of the game is handled here.
 */
public class MainController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
	public GameObject sink;
	public GameObject sinkTrigger;
	public GameObject jacketTrigger;
	public GameObject jacket;

	int sinkCounter = 0;
	bool noteFound = false;


	// Use this for initialization
	void Start () {
		guiController.showSubtl ("entry");
	}
	
	// Update is called once per frame
	void Update () {
		getKeyInteractions ();
		checkGUI ();
		checkCollisions ();
	}

	//get all the key interactions
	void getKeyInteractions(){
		//close a subtitle with space
		if (Input.GetKeyDown (KeyCode.Space) && guiController.isShowing ()) {
			guiController.unshowSubtl();
			if(sinkCounter == 1){
				jacket.SetActive(true);
			}
		}

		// handle 'E' interactions
		if (Input.GetKeyDown (KeyCode.E)) {
			// interacted with sink
			if (sink.GetComponent<SinkTrigger>().sinkTriggered() && sinkCounter < 2) {
				switch (sinkCounter){
				case 0: 
					guiController.showSubtl("mirror1");
					sinkCounter++;
					break;
				case 1:
					guiController.showSubtl("mirror2");
					guiController.showInventoryHint();
					sinkCounter++;
					break;
				default: break;
				}
			}
			// interacted with jacket
			else if(jacketTrigger.GetComponent<JacketTrigger>().jacketTriggered() && noteFound == false){
				guiController.showSubtl("paper");
				guiController.showInventoryHint();
				noteFound = true;
			}
		}
	}

	//check if a subtitle is shown and handle player movement and interaction hint
	void checkGUI(){
		if (guiController.isShowing ()) {
			player.GetComponent<FirstPersonController> ().enabled = false;
			guiController.GetComponent<GUIController> ().unshowInteractionHint ();	
		} else {
			player.GetComponent<FirstPersonController> ().enabled = true;
		}
	}

	//check if the player collides with an interactable object
	void checkCollisions(){
		if (sink.GetComponent<SinkTrigger> ().sinkTriggered () && guiController.isShowing () == false && sinkCounter < 2) {
			guiController.GetComponent<GUIController> ().showInteractionHint ();			
		}
		else if (jacketTrigger.GetComponent<JacketTrigger> ().jacketTriggered() && noteFound == false) {
			guiController.GetComponent<GUIController> ().showInteractionHint ();
		} else {
			guiController.GetComponent<GUIController> ().unshowInteractionHint ();
		}
	}
}
