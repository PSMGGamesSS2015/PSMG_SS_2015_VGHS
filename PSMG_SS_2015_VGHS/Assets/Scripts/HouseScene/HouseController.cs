using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject michaelTrigger;
	public GameObject pianoTrigger;
	public GameObject diningRoomTrigger1;
	public GameObject diningRoomTrigger2;
	public GameObject InteractionPanel;
	public GameObject theory;

	List<string> dialogsPerformed = new List<string>();
	int pianoCount = 0;
	int dialogCount = 1;
	bool welcomeDialog = true;
	bool scar1Dialog = true;
	bool michael = true;
	bool daughter = false;
	bool diningRoom = true;
	bool dialog = false;
	bool theory1Registered = true;
	bool theory2Registered = false;
	bool familyInteractionDone = false;
	bool missingPicture = false;
	string actualDialog = "";

	// Setup Inventory and Interactions when House Scene starts
	void Start () {
		guiController.addHint("dressHint");
		guiController.addHint("noteHint");
		guiController.forceThSetup();
		guiController.manageInteraction("michael_scar");
		initDialog ("welcome");
	}
	
	// Update is called once per frame
	void Update () {
		toggleDialogs ();
		getKeyInteractions ();
		checkCollisions();
		checkInteractionPanel ();
		checkInventory ();
	}

	// handle key interactions here
	void getKeyInteractions(){
		// handle dialog counting for each Dialog
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(dialog){
				dialogCount++;
			}
		}
		// handle 'E' interactions
		if (Input.GetKeyDown (KeyCode.E)) {
			if(michaelTrigger.GetComponent<MichaelTrigger> ().michaelTriggered () && guiController.checkForPanelContent() && guiController.subtlShown !=true){
				guiController.toggleInteractionPanel(true);
			}
			if(pianoTrigger.GetComponent<PianoTrigger>().pianoTriggered() && pianoCount <3 && guiController.subtlShown !=true){
				switch (pianoCount){
				case 0:
					initDialog ("piano");
					break;
				case 1:
					if(daughter == false){
						initDialog("daughter");
						//do this if interaction was set but not done yet
						if(familyInteractionDone){
							guiController.closeInteractionInPanel("Kinder?");
						}
					}
					else{
						initDialog("daughter2_");
						pianoCount++;
					}
					break;
				case 2:
					initDialog("daughter2_");
					break;
				default: break;
				}
				pianoCount++;

			}
		}
	}

	//check if the player collides with an interactable object
	void checkCollisions(){
		//check if michael is triggered and interactable
		if (michaelTrigger.GetComponent<MichaelTrigger> ().michaelTriggered () && guiController.checkForPanelContent() && guiController.isShowing() == false) {
			guiController.toggleInteractionHint (true);
		} 
		// toggle dining room triggering
		else if(diningRoom && (diningRoomTrigger1.GetComponent<DiningRoomTrigger1>().diningRoomTriggered() || diningRoomTrigger2.GetComponent<DiningRoomTrigger2>().diningRoomTriggered())){
			guiController.toggleSubtl("diningRoom");
			guiController.manageInteraction("michael_friends");
			if(familyInteractionDone == false){
				guiController.manageInteraction("michael_family");
				familyInteractionDone = true;
			}
			diningRoom = false;
		} 
		// check if piano is triggered
		else if(pianoTrigger.GetComponent<PianoTrigger>().pianoTriggered() && pianoCount < 3 &&guiController.isShowing() == false){
			guiController.toggleInteractionHint (true);
		}
		else {
			guiController.toggleInteractionHint (false);
		}
	}

	// check for new interactions in interaction panel
	void checkInteractionPanel(){
		if (guiController.manageDialogs().Equals("") == false && dialogsPerformed.Contains(guiController.manageDialogs()) == false) {
			initDialog(guiController.manageDialogs());
			guiController.toggleInteractionPanel(false);
		}
	}

	// initialize a dialog
	void initDialog(string dialogToInit){
		dialog = true;
		actualDialog = dialogToInit;
		guiController.toggleSubtl (dialogToInit+dialogCount);
	}

	// toggle the needed dialog
	void toggleDialogs(){
		int dialogMaxNum = 0;
		switch (actualDialog) {
		case "welcome":
			dialogMaxNum = 5;
			break;
		case "scar1_":
			dialogMaxNum = 2;
			break;
		case "scar2_":
			dialogMaxNum = 4;
			break;
		case "friends":
			dialogMaxNum = 2;
			break;
		case "family":
			dialogMaxNum = 4;
			break;
		case "daughter":
			dialogMaxNum = 7;
			break;
		case "piano":
			dialogMaxNum = 3;
			break;
		case "daughter2_":
			dialogMaxNum = 7;
			break;
		case "picture":
			dialogMaxNum = 4;
			break;

		default: break;
		}
		if(actualDialog.Equals("") == false){
			manageDialogSettings(dialogMaxNum, actualDialog+dialogCount);
		}
	}

	// manage if dialog has ended or not
	void manageDialogSettings(int dialogNum, string actualSubtl){
		if (dialogCount > dialogNum) {
			dialogsPerformed.Add(actualSubtl.Substring(0, actualSubtl.Length-1));
			// add hint during dialog
			if(actualSubtl.Substring(0, actualSubtl.Length-3).Equals("scar")){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-3));
			} 
			// activate children interaction and insert hin about brother
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("family")){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-1));
				if(daughter == false){
					guiController.manageInteraction("michael_daughter");
				}
			}
			// add hint about emily to inventory
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("daughter")){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-1));
				daughter = true;
			}
			// add hint about dianes daughter to inventory
			else if(actualSubtl.Substring(0, actualSubtl.Length-3).Equals("daughter")){
				insertIntoInventory("dianesDaughter");
				guiController.manageInteraction("michael_friends");
			}
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("picture")){
				insertIntoInventory("missingPicture");
			}
			dialogCount = 1;
			actualDialog = "";
			dialog = false;
		} 
		else if(actualSubtl.Equals("daughter2_2")){
			insertIntoInventory("picture");
			guiController.toggleSubtl (actualSubtl);
		}
		else {
			guiController.toggleSubtl (actualSubtl);
		}
	}

	//insert item into inventory
	void insertIntoInventory(string hintToAdd){
		guiController.toggleInventoryHint ();
		guiController.addHint (hintToAdd);
	}

	// handle Level changing stuff triggered by actions in the inventory here
	void checkInventory(){
		if (guiController.checkForNewHint ().Equals ("") == false) {
			switch(guiController.checkForNewHint()){
			case "missingPicture":
				if(missingPicture == false){
					missingPicture = true;
					guiController.toggleSubtl("missingPictureFound");
					guiController.toggleInventory();
					guiController.manageInteraction("michael_missingPicture");
				}
				break;
			}
		}
		if(theory.GetComponent<Theory> ().theory2Found && theory2Registered == false){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory2");
			guiController.manageInteraction("michael_scar_2");
			theory2Registered = true;
		}
	}
}
