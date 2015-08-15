﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;
/* HOUSE CONTROLLER 1
 * Everything that happens during the first scene in the house is managed here
 */
public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
	public GameObject michael;
	public GameObject paula;
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
	public GameObject floorTrigger;
	public GameObject InteractionPanel;
	public GameObject theory;
	public GameObject drawer;
	public GameObject glassTableImpression;
	public GameObject adressBook;
	public GameObject playerCam;


	List<string> dialogsPerformed = new List<string>();
	int pianoCount = 0;
	int dialogCount = 1;
	int actualHouseScene = 1;
	string actualDialog = "";
	bool scarHint = false;
	bool friends = false;
	public bool family = false;
	bool daughterFound = false;
	bool diningRoomTriggered = false;
	bool childsroomTriggered = false;
	bool wintergardenTriggered = true;
	bool conservatoryTriggered = false;
	bool guestroomTriggered = false;
	bool workroomTriggered = false;
	bool bedroomTriggered = false;
	public bool glasstableTriggered = false;
	bool dialog = false;
	public bool theory2Registered = false;
	bool familyInteractionDone = false;
	bool missingPicture = false;
	public bool missingPictureFound = false;
	public bool emilyWhereabout = false;
	bool sidetableOpen = false;
	bool adressBookDialog = false;
	public bool adressBookFound = false;
	bool scene1EndingDialog = false;
	bool isDizzy = false;
	bool secondSceneReady = false;
	bool paulaPhoneDialog1 = false;
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
		randomPlayerControl ();
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
			// interactions are only possible if nothing like subtitles or the inventory is shown
			if (guiController.isShowing () == false) {
				switch(gameObject.GetComponent<TriggerController> ().getTriggerTag()){
				case "Michael": // open interaction panel if interactions are available
					if(guiController.checkForPanelContent()){
						guiController.toggleInteractionPanel(true);
					}
					break;
				case "Piano": // manage interactions with piano
					if(pianoCount <3){
						switch (pianoCount){
						case 0:
							initDialog ("piano");
							break;
						case 1:
							if(daughterFound == false){
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
						}
						pianoCount++;
					}
					break;
				case "Glasstable": // interaction with glasstable
					if(!glasstableTriggered){
						glasstableTriggered = true;
						initDialog("glassTable");
					}
					break;
				case "Familyalbum": // interaction with familyalbum
					initDialog("familyAlbum1_");
					break;
				case "Notepad": // interaction with notepad
					guiController.toggleSubtl("noteMonolog");
					break;
				case "Bookshelf": // interaction with bookshelf in workroom
					guiController.toggleSubtl("bookshelf");
					break;
				case "Sidetable": // managing interaction with sidetable and adressbook
					if (!sidetableOpen){
						drawer.GetComponent<DrawerAnimator>().openDrawer();
						sidetableOpen = true;
						if(!adressBookFound){
							initDialog("adressBook");
						}	
					} else{
						guiController.toggleAdressBook();
						drawer.GetComponent<DrawerAnimator>().closeDrawer();
						adressBookFound = true;
						sidetableOpen = false;
					}
					break;
				default: break;
				}
			}else{
				guiController.toggleInteractionPanel(false);
			}
		}
	}

	//check if the player collides with an interactable object
	void checkCollisions(){
		//interaction hints may not be shown if sth else is shown on GUI
		if (!guiController.isShowing ()) {
			switch(gameObject.GetComponent<TriggerController> ().getTriggerTag()){
			case "Michael": //check if michael is triggered and interactable
				if(guiController.checkForPanelContent ()){
					guiController.toggleInteractionHint(true);
				}else {
					guiController.toggleInteractionHint (false);
				}
				break;
			case "Diningroom": // check if diningroom entered and initialize new interactions
				if(!diningRoomTriggered){
					guiController.toggleSubtl ("diningRoom");
					guiController.manageInteraction ("michael_friends");
					if (!familyInteractionDone) {
						guiController.manageInteraction ("michael_family");
						familyInteractionDone = true;
					}
					diningRoomTriggered = true;	
				}
				break;
			case "Piano":
				if(pianoCount < 3){
					guiController.toggleInteractionHint (true);
				}
				break;
			case "Familyalbum": // check if familyalbum triggered
				guiController.toggleInteractionHint (true);
				break;
			case "Notepad": // check if noteblock triggered
				guiController.toggleInteractionHint (true);
				break;
			case "Conservatory": // check if jane entered wintergarden for first time
				if(!conservatoryTriggered){
					initDialog("conservatory");
					conservatoryTriggered = true;
				}
				break;
			case "Glasstable": // check if glasstable triggered
				if(!glasstableTriggered){
					guiController.toggleInteractionHint(true);
				}
				break;
			case "Childsroom": // check if childsroom entered for first time and if Michael already told about Emily
				if(!childsroomTriggered){
					if(!daughterFound){
						initDialog("daughter");
					} else{
						initDialog("childsroom");
						childsroomTriggered = true;
					}
				}
				break;
			case "Guestroom": // check if guestroom entered
				if(!guestroomTriggered){
					initDialog("guestroom");
					guestroomTriggered = true;
				}
				break;
			case "Workroom": // check if guestroom entered
				if(!workroomTriggered){
					guiController.toggleSubtl("workroom");
					workroomTriggered = true;
				}
				break;
			case "Bookshelf": // check if bookshelf in workroom triggered
				guiController.toggleInteractionHint(true);
				break;
			case "Bedroom": // check if bedroom entered
				if(!bedroomTriggered){
					initDialog("bedroom");
					bedroomTriggered = true;
				}
				break;
			case "Sidetable": // check if sidetable triggered
				guiController.toggleInteractionHint (true);
				break;
			default: 
				guiController.toggleInteractionHint (false);
				break;
			}

		}else {
			guiController.toggleInteractionHint (false);
		}
		/*
		//make sure not to check for options only available in vertain scenes
		switch (actualHouseScene) {
		case 2:
			if (paulaPhoneDialog1 == false && (diningRoomTrigger2.GetComponent<DiningRoomTrigger2>().diningRoomTriggered () || floorTrigger.GetComponent<FloorTrigger>().floorTriggered())){
				guiController.toggleSubtl("paulaPhoneDialog1");
				paulaPhoneDialog1 = true;
			}
			break;
		default: break;
		}
		*/

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
		case "conservatory":
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
		}
		if(actualDialog.Equals("") == false){
			manageDialogSettings(dialogMaxNum, actualDialog+dialogCount);
		}
	}

	// manage if dialog has ended or not and if certain things happen after ending
	void manageDialogSettings(int dialogNum, string actualSubtl){
		//do after-dialog-events here
		if (dialogCount > dialogNum) {
			dialogsPerformed.Add(actualSubtl.Substring(0, actualSubtl.Length-1));
			// add hint about scar during dialog
			if(actualSubtl.Substring(0, actualSubtl.Length-3).Equals("scar") && scarHint == false){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-3));
				scarHint = true;
			} 
			// checkup dialog about family, insert information about visiting the brother and activate interaction for children if not already done at the piano
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("family")){
				family = true;
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-1));
				if(daughterFound == false){
					guiController.manageInteraction("michael_daughter");
				}
			}
			// checkup dialog about friends
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("friends")){
				friends = true;
			}
			// add hint about emily to inventory
			else if(actualSubtl.Substring(0, actualSubtl.Length-1).Equals("daughter")){
				insertIntoInventory(actualSubtl.Substring(0, actualSubtl.Length-1));
				daughterFound = true;
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
				//toggeling here next scene after this dialog makes sure, that nothing is destroyed while first scene still going on
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
		if(glasstableTriggered && family && adressBookFound && guiController.isShowing() == false && scene1EndingDialog == false && emilyWhereabout){
			initDialog("scene1Ending");
		}
	}

	// delete or enable objects, trigger etc. as needed in next scene
	void arrangeNextScene(int scene){
		GetComponent<SceneFader> ().SwitchScene (3);

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
			diningRoomTriggered = false;
			floorTrigger.SetActive(true);
			actualHouseScene++;
			break;
		default: break;
		}
		StartCoroutine (onNextSceneStart ());

	}

	// generate heavier player controlling while jane is dizzy
	void randomPlayerControl(){
		if (isDizzy) {

			player.GetComponent<FirstPersonController> ().randomControl ("Horizontal", "Vertical", 20);
			playerCam.GetComponent<BlurOptimized>().enabled = true;
		}else{
			player.GetComponent<FirstPersonController> ().randomControl ("Horizontal", "Vertical", 2);
			playerCam.GetComponent<BlurOptimized>().enabled = false;
		}
	}

	// do stuff here that is needed when new scene starts
	IEnumerator onNextSceneStart(){
		yield return new WaitForSeconds (2);
		// fade scene in
		if (GetComponent<SceneFader> ().checkFade() && actualHouseScene > 1) {
			michael.SetActive(false);
			paula.SetActive(true);
			player.transform.position = bedPos;
			GetComponent<SceneFader> ().forceFadeIn();
		}
		switch (actualHouseScene) {
		case 2:
			if(secondSceneReady == false){
				isDizzy = true;
				guiController.toggleSubtl("dizzy1");
				secondSceneReady = true;
			}
			break;
		default: break;
		}
	}
}
