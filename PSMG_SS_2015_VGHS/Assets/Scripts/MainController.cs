using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/* MAIN CONTROLLER
 * This is the games main controlling Script. 
 * It receives event based data from other scripts (like where collisions are triggered).
 * Its task is to store and process this data and to communicate the results to other scripts.
 * So every logic of the game is handled here.
 * Interactions depending on the inventory like dragging & dropping hints are handled by the SlotScript and Inventory Script
 */

public class MainController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
	public GameObject sink;
	public GameObject sinkTrigger;
	public GameObject jacketTrigger;
	public GameObject jacket;
	public GameObject jacketOn;
	public GameObject theory;
    bool isPaused = false;

	int sinkCounter = 0;
	bool noteFound = false;
	bool theory1Registered = false;


	// Use this for initialization
	void Start () {
        Cursor.visible = false;
		guiController.toggleSubtl ("entry");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        getKeyInteractions();
        checkInventory();
        checkGUI();
        checkCollisions();
	}

	//get all the key interactions
	void getKeyInteractions(){
		// open/close inventory with 'I'
		if (Input.GetKeyDown (KeyCode.I)) {
			guiController.toggleInventory();
		}

		// close a subtitle with space
		if (Input.GetKeyDown (KeyCode.Space) && guiController.isShowing ()) {
			guiController.toggleSubtl(null);
            player.GetComponent<FirstPersonController>().enabled = true;
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
                    player.GetComponent<FirstPersonController>().enabled = false;
				}
			}
		}
	}

	//check if a subtitle or the inventory is shown and handle player movement and interaction hint
	void checkGUI(){
        if (!isPaused)
        {
            if (guiController.isShowing())
            {
                player.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                player.GetComponent<FirstPersonController>().enabled = true;
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
            player.GetComponent<FirstPersonController>().enabled = false;
		}
	}


	public void ChangeLevel(int level){
        //Application.LoadLevel(level);
        GetComponent<SceneFader>().SwitchScene(level);
    }
}
