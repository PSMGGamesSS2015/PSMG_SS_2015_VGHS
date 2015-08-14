using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
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
	public GameObject glassTableImpression;
	public GameObject adressBook;

	public bool test = false;


	List<string> dialogsPerformed = new List<string>();
	int pianoCount = 0;
	int dialogCount = 1;
	int actualHouseScene = 1;
	string actualDialog = "";
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
	bool emilyWhereabout = false;
	bool sidetableOpen = false;
	bool adressBookDialog = false;
	bool adressBookFound = false;
	bool scene1EndingDialog = false;
	Vector3 bedPos = new Vector3(1.4f, 4.5f, 16.3f);


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
			// check for possible interactions with michael and open interaction panel
			if(michaelTrigger.GetComponent<MichaelTrigger> ().michaelTriggered () && guiController.checkForPanelContent() && guiController.subtlShown !=true){
				guiController.toggleInteractionPanel(true);
			}
			// make sure not to check for objects that were destroyed
			switch(actualHouseScene){
				case 1:
					// handle piano dialogs
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
					// handle glasstable
					if(glassTableTrigger.GetComponent<GlassTableTrigger>().glassTableTriggered() && glassTableTriggered == false){
						glassTableTriggered = true;
						initDialog("glassTable");
					}
					break;
				default: break;
			}

			// handle family album 
			if(familyAlbumTrigger.GetComponent<FamilyAlbumTrigger>().albumTriggered()){
				switch (actualHouseScene){
				case 1: 
					initDialog("familyAlbum1_");
					break;
				default: break;
				}
			}
			// handle noteblock
			if(noteTrigger.GetComponent<NoteTrigger>().noteTriggered()){
				switch (actualHouseScene){
				case 1: 
					guiController.toggleSubtl("noteMonolog");
					break;
				default: break;
				}
			}
			// handle bookshelf in workroom
			if(bookshelfTrigger.GetComponent<BookshelfTrigger>().bookshelfTriggered()){
				switch (actualHouseScene){
				case 1: 
					guiController.toggleSubtl("bookshelf");
					break;
				default: break;
				}
			}

			// handle side table with adress book in bedroom
			if(sideTableTrigger.GetComponent<SideTableTrigger>().sideTableTriggered()){
				// open sidetable (!!!not implemented yet!!!)
				if (sidetableOpen == false){
					drawer.GetComponent<DrawerAnimator>().openDrawer();
					sidetableOpen = true;
					if(adressBookFound == false){
						initDialog("adressBook");
					}

				}
				// interact with adressbook
				else if(sidetableOpen){
					guiController.toggleAdressBook();
					drawer.GetComponent<DrawerAnimator>().closeDrawer();
					adressBookFound = true;
					sidetableOpen = false;
				}
			}
		}
	}

	//check if the player collides with an interactable object
	void checkCollisions(){
		switch (actualHouseScene) {
		case 1: 
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
			break;
		default: break;
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
		//do after-dialog-events here
		if (dialogCount > dialogNum) {
			dialogsPerformed.Add(actualSubtl.Substring(0, actualSubtl.Length-1));
			// add hint about scar during dialog
			if(actualSubtl.Substring(0, actualSubtl.Length-3).Equals("scar") && scarHint == false){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-3));
				scarHint = true;
			} 
			// checkup dialog about family
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("family")){
				family = true;
			}
			// checkup dialog about friends
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("friends")){
				friends = true;
			}
			// activate children interaction and insert hint about brother
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
			// add hint about the missing picture
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("picture")){
				insertIntoInventory("missingPicture");
				missingPictureFound = true;
			}
			// add hint about emilies whereabout
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("childsroom")){
				insertIntoInventory("emilyWhereabout");
				emilyWhereabout = true;
			}
			// register that dialog about the adress book is done
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("adressBook")){
				adressBookDialog = true;
			}
			// insert hint about pills after taking them for the first time
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("scene1Ending")){
				scene1EndingDialog = true;
				insertIntoInventory("pills");
				arrangeNextScene(actualHouseScene);
			}
			// reset dialog data for new dialog
			dialogCount = 1;
			actualDialog = "";
			dialog = false;
		} 
		// insert hint about picture during dialog (this is a special case where the hint is added during dialog)
		else if(actualSubtl.Equals("daughter2_2")){
			insertIntoInventory("picture");
			guiController.toggleSubtl (actualSubtl);
		}
		// toggle subtl. during dialog
		else {
			guiController.toggleSubtl (actualSubtl);
		}
	}

	//insert item into inventory
	void insertIntoInventory(string hintToAdd){
		guiController.toggleInventoryHint ();
		guiController.addHint (hintToAdd);
	}

	// handle stuff triggered by actions in the inventory here
	void checkInventory(){
		if (guiController.checkForNewHint ().Equals ("") == false) {
			// do sth. if a new hint was added that enables an event or interaction
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
		// check for second theory
		if(theory.GetComponent<Theory> ().theory2Found && theory2Registered == false){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory2");
			guiController.manageInteraction("michael_scar_2");
			theory2Registered = true;
		}
	}

	// check if all necessary interactions are done to end a scene
	void checkSceneEnding (){
		//Ending conditions for first Scene in house
		if(theory2Registered && missingPictureFound && glassTableTriggered && friends && family && adressBookFound && guiController.isShowing() == false && scene1EndingDialog == false && emilyWhereabout){
			initDialog("scene1Ending");
		}
		// move player to bed
		if (GetComponent<SceneFader> ().checkFade() && actualHouseScene > 1) {
			GetComponent<SceneFader> ().forceFadeIn();
		}
	}

	// delete or enable objects, trigger etc. as needed in next scene
	void arrangeNextScene(int scene){
		GetComponent<SceneFader> ().SwitchScene (3);
		player.transform.position = bedPos;
		switch (scene) {
		case 1:
			Destroy (glassTableImpression);
			Destroy(wintergardenTrigger);
			Destroy(glassTableTrigger);
			Destroy (pianoTrigger);
			Destroy(diningRoomTrigger1);
			Destroy (childsroomTrigger);
			Destroy (workroomTrigger);
			Destroy (guestroomTrigger);
			Destroy (bedroomTrigger);
			Destroy (adressBook);
			diningRoom = true;
			actualHouseScene++;
			break;
		default: break;
		}

	}
}
