using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject michaelTrigger;
	public GameObject diningRoomTrigger1;
	public GameObject diningRoomTrigger2;
	public GameObject InteractionPanel;

	int dialogCount = 0;
	bool welcomeDialog = true;
	bool scar1Dialog = true;
	bool michael = true;
	bool diningRoom = true;

	// Setup Inventory when House Scene starts
	void Start () {
		guiController.addHint("dressHint");
		guiController.addHint("noteHint");
		guiController.forceThSetup();
		guiController.toggleSubtl ("welcome");
		guiController.manageInteraction("michael_scar");
	}
	
	// Update is called once per frame
	void Update () {
		// during welcoming
		if(welcomeDialog){
			toggleWelcomeDialog ();
		}

		getKeyInteractions ();
		checkCollisions();
		checkInteractionPanel ();

	}

	// handle key interactions here
	void getKeyInteractions(){

		// handle dialog counting for each Dialog
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(welcomeDialog != false){
				dialogCount++;
			} else{
				dialogCount = 0;
			}
		}

		// handle 'E' interactions
		if (Input.GetKeyDown (KeyCode.E)) {
			if(michaelTrigger.GetComponent<MichaelTrigger> ().michaelTriggered () && michael){
				guiController.toggleInteractionPanel();
			}
		}
	}

	// handle procedure of welcoming dialog here
	void toggleWelcomeDialog(){
		switch (dialogCount) {
		case 0:
			guiController.toggleSubtl ("welcome1");
			break;
		case 1:
			guiController.toggleSubtl ("welcome2");
			break;
		case 2:
			guiController.toggleSubtl ("welcome3");
			break;
		case 3:
			guiController.toggleSubtl ("welcome4");
			break;
		case 4:
			guiController.toggleSubtl ("welcome5");
			break;
		}
	}

	void toggleScar1Dialog(){
		switch (dialogCount) {
		case 0:
			guiController.toggleSubtl ("scar1_1");
			break;
		case 1:
			guiController.toggleSubtl ("scar1_2");
			break;
		}
	}

	//check if the player collides with an interactable object
	void checkCollisions(){
		//check if michael is triggered and interactable
		if (michaelTrigger.GetComponent<MichaelTrigger> ().michaelTriggered () && michael && guiController.isShowing() == false) {
			guiController.toggleInteractionHint (true);
		} 
		// toggle dining room triggering
		else if(diningRoom && (diningRoomTrigger1.GetComponent<DiningRoomTrigger1>().diningRoomTriggered() || diningRoomTrigger2.GetComponent<DiningRoomTrigger2>().diningRoomTriggered())){
			guiController.toggleSubtl("diningRoom");
			diningRoom = false;
		} else {
			guiController.toggleInteractionHint (false);
		}
	}

	// check for new interactions in interaction panel
	void checkInteractionPanel(){
		if (InteractionPanel.GetComponent<MichaelInteractions> ().scar1) {

		}
	}
}
