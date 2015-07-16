using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject michaelTrigger;
	public GameObject diningRoomTrigger1;
	public GameObject diningRoomTrigger2;
	public GameObject InteractionPanel;

	List<string> dialogsPerformed = new List<string>();
	int dialogCount = 1;
	bool welcomeDialog = true;
	bool scar1Dialog = true;
	bool michael = true;
	bool diningRoom = true;
	bool dialog = false;
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
			diningRoom = false;
		} else {
			guiController.toggleInteractionHint (false);
		}
	}

	// check for new interactions in interaction panel
	void checkInteractionPanel(){
		if (guiController.manageDialogs() != "" && dialogsPerformed.Contains(guiController.manageDialogs()) == false) {
			initDialog(guiController.manageDialogs());
			guiController.toggleInteractionPanel(false);
		}
	}

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
		default: break;
		}
		if(actualDialog != ""){
			manageDialogSettings(dialogMaxNum, actualDialog+dialogCount);
		}
	}

	// manage if dialog has ended or not
	void manageDialogSettings(int dialogNum, string actualSubtl){
		if (dialogCount > dialogNum) {
			dialogsPerformed.Add(actualSubtl.Substring(0, actualSubtl.Length-1));
			if(actualSubtl.Substring(0, actualSubtl.Length-3) == "scar"){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-3));
			}
			dialogCount = 1;
			actualDialog = "";
			dialog = false;
		} else {
			guiController.toggleSubtl (actualSubtl);
		}
	}

	void insertIntoInventory(string hintToAdd){
		guiController.toggleInventoryHint ();
		guiController.addHint (hintToAdd);
	}

}
