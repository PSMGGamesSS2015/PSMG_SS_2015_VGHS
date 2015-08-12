using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject michaelTrigger;
	public GameObject pianoTrigger;
	public GameObject diningRoomTrigger1;
	public GameObject diningRoomTrigger2;
	public GameObject familyAlbumTrigger;
	public GameObject noteTrigger;
	public GameObject bookshelfTrigger;
	public GameObject wintergardenTrigger;
	public GameObject glassTableTrigger;
	public GameObject childsroomTrigger;
	public GameObject guestroomTrigger;
	public GameObject workroomTrigger;
	public GameObject bedroomTrigger;
	public GameObject sideTableTrigger;
	public GameObject InteractionPanel;
	public GameObject theory;
	public GameObject drawer;


	List<string> dialogsPerformed = new List<string>();
	int pianoCount = 0;
	int dialogCount = 1;
	int actualHouseScene = 1;
	bool scarHint = false;
	bool friends = false;
	bool family = false;
	bool daughter = false;
	bool diningRoom = true;
	bool childsroom = true;
	bool wintergarden = true;
	bool guestroom = true;
	bool workroom = true;
	bool bedroom = true;
	bool glassTableTriggered = false;
	bool dialog = false;
	bool theory2Registered = false;
	bool familyInteractionDone = false;
	bool missingPicture = false;
	bool missingPictureFound = false;
	bool sidetableOpen = false;
	bool adressBookDialog = false;
	bool adressBookFound = false;
	bool scene1EndingDialog = false;
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
		checkSceneEnding ();
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
			if(actualHouseScene == 1 && familyAlbumTrigger.GetComponent<FamilyAlbumTrigger>().albumTriggered()){
				initDialog("familyAlbum1_");
			}
			if(actualHouseScene == 1 && noteTrigger.GetComponent<NoteTrigger>().noteTriggered()){
				guiController.toggleSubtl("noteMonolog");
			}
			if(actualHouseScene == 1 && bookshelfTrigger.GetComponent<BookshelfTrigger>().bookshelfTriggered()){
				guiController.toggleSubtl("bookshelf");
			}
			if(glassTableTrigger.GetComponent<GlassTableTrigger>().glassTableTriggered() && glassTableTriggered == false){
				glassTableTriggered = true;
				initDialog("glassTable");
			}
			if(sideTableTrigger.GetComponent<SideTableTrigger>().sideTableTriggered() && sidetableOpen == false && adressBookDialog == false){
				drawer.transform.Translate(0, 0, 0.7f);
				sidetableOpen = true;
				initDialog("adressBook");
			}
			if(sideTableTrigger.GetComponent<SideTableTrigger>().sideTableTriggered() && adressBookDialog){
				guiController.toggleAdressBook();
				adressBookFound = true;
			}
		}
	}

	//check if the player collides with an interactable object
	void checkCollisions(){
		//check if michael is triggered and interactable
		if (michaelTrigger.GetComponent<MichaelTrigger> ().michaelTriggered () && guiController.checkForPanelContent () && guiController.isShowing () == false) {
			guiController.toggleInteractionHint (true);
		} 
		// toggle dining room triggering
		else if (diningRoom && (diningRoomTrigger1.GetComponent<DiningRoomTrigger1> ().diningRoomTriggered () || diningRoomTrigger2.GetComponent<DiningRoomTrigger2> ().diningRoomTriggered ())) {
			guiController.toggleSubtl ("diningRoom");
			guiController.manageInteraction ("michael_friends");
			if (familyInteractionDone == false) {
				guiController.manageInteraction ("michael_family");
				familyInteractionDone = true;
			}
			diningRoom = false;
		} 
		// check if piano is triggered
		else if (pianoTrigger.GetComponent<PianoTrigger> ().pianoTriggered () && pianoCount < 3 && guiController.isShowing () == false) {
			guiController.toggleInteractionHint (true);
		}
		// check if family album is triggered
		else if (familyAlbumTrigger.GetComponent<FamilyAlbumTrigger> ().albumTriggered ()) {
			guiController.toggleInteractionHint (true);
		}
		// check if noteblock triggered
		else if (noteTrigger.GetComponent<NoteTrigger> ().noteTriggered () && guiController.isShowing () == false) {
			guiController.toggleInteractionHint (true);
		}
		// check if jane entered wintergarden for first time
		else if(wintergardenTrigger.GetComponent<WintergardenTrigger>().wintergardenTriggered() && wintergarden){
			initDialog("wintergarden");
			wintergarden = false;
		}
		// check if glass table was triggered
		else if(glassTableTrigger.GetComponent<GlassTableTrigger>().glassTableTriggered() && glassTableTriggered == false){
			guiController.toggleInteractionHint(true);
		}
		// check if player entered childsroom
		else if(childsroom && childsroomTrigger.GetComponent<ChildsroomTrigger>().childsroomTriggered()){
			initDialog("childsroom");
			childsroom = false;
		}
		// check if player entered guestroom
		else if(guestroom && guestroomTrigger.GetComponent<GuestroomTrigger>().guestroomTriggered()){
			initDialog("guestroom");
			guestroom = false;
		}
		// check if player entered workroom
		else if (workroom && workroomTrigger.GetComponent<WorkroomTrigger>().workroomTriggered()){
			guiController.toggleSubtl("workroom");
			workroom = false;
		}
		// check if player is nearby bookshelf in workroom
		else if (bookshelfTrigger.GetComponent<BookshelfTrigger>().bookshelfTriggered()&& guiController.isShowing () == false){
			guiController.toggleInteractionHint(true);
		}
		// check if player entered bedroom
		else if (bedroom && bedroomTrigger.GetComponent<BedroomTrigger>().bedroomTriggered()){
			initDialog("bedroom");
			bedroom = false;
		}
		// check if player is near sidetable
		else if (sideTableTrigger.GetComponent<SideTableTrigger>().sideTableTriggered() && guiController.isShowing () == false){
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
		case "familyAlbum1_":
			dialogMaxNum = 2;
			break;
		case "wintergarden":
			dialogMaxNum = 3;
			break;
		case "glassTable":
			dialogMaxNum = 5;
			break;
		case "childsroom":
			dialogMaxNum = 3;
			break;
		case "guestroom":
			dialogMaxNum = 4;
			break;
		case "bedroom":
			dialogMaxNum = 2;
			break;
		case "adressBook":
			dialogMaxNum = 2;
			break;
		case "scene1Ending":
			dialogMaxNum = 3;
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
				if (scarHint == false){
					insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-3));
					scarHint = true;
				}
			} 
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("family")){
				family = true;
			}
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("friends")){
				friends = true;
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
				missingPictureFound = true;
			}
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("childsroom")){
				insertIntoInventory("emilyWhereabout");
			}
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("adressBook")){
				adressBookDialog = true;
			}
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("scene1Ending")){
				scene1EndingDialog = true;
				insertIntoInventory("pills");
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

	// check if all necessary interactions are done to end a scene
	void checkSceneEnding (){
		if (theory2Registered && missingPictureFound && glassTableTriggered && friends && family && adressBookFound && guiController.isShowing () == false) {
			Debug.Log ("fertig");
		}
		if(theory2Registered && missingPictureFound && glassTableTriggered && friends && family && adressBookFound && guiController.isShowing() == false && scene1EndingDialog == false){
			initDialog("scene1Ending");
		}
	}
}
