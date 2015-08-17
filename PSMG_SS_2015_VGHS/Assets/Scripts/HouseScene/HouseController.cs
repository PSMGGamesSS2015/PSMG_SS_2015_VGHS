using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.CrossPlatformInput;
/* HOUSE CONTROLLER 1
 * Everything that happens during the first scene in the house is managed here
 */
public class HouseController : MonoBehaviour {

	public GUIController guiController;
	public GameObject player;
	public GameObject michael;
	public GameObject paula;
	public GameObject InteractionPanel;
	public GameObject theory;
	public GameObject drawer;
	public GameObject glassTableImpression;
	public GameObject adressBook;
	public GameObject playerCam;

	List<string> dialogsPerformed = new List<string>();
	Dictionary<string,int> keyDialogSizeMap = new Dictionary<string,int>();
	Vector3 bedPos = new Vector3(1.4f, 4.5f, 16.3f);
	Vector3 michaelScene2Pos = new Vector3(22f, -1f, 32f);
	RaycastHit hit;
	string actualDialog = "";
	int pianoCount = 0;
	int dialogCount = 1;
	int actualHouseScene = 1;
	float rayDist = 40;
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
	public bool paulaIntroductionDialog = false;
	public bool paulaPhoneDialog2 = false;
	bool janeSurprised = false;




	// Setup Inventory and Interactions when House Scene starts
	void Start () {
		guiController.addHint("dressHint");
		guiController.addHint("noteHint");
		guiController.forceThSetup();
		guiController.manageInteraction("michael_scar", "Michael");
		setupKeyDialogSizeMap ();
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
		checkSight ();
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
						guiController.toggleInteractionPanel(true, "Michael");
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
					switch(actualHouseScene){
					case 1: // interaction during frist house scene
						initDialog("familyAlbum1_");
						break;
					case 2: // interaction during second house scene
						if(paulaPhoneDialog2 && paulaIntroductionDialog){
							guiController.toggleFamilyalbum();
							michael.SetActive(true);
							janeSurprised = true;
						}
						break;
					}

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
				case "Paula": // open interaction panel if interactions are available
					if(guiController.checkForPanelContent()){
						guiController.toggleInteractionPanel(true, "Paula");
					}
					break;
				default: break;
				}
			}else{
				guiController.toggleInteractionPanel(false, "");
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
					guiController.manageInteraction ("michael_friends", "Michael");
				}
				if (!familyInteractionDone) {
					guiController.manageInteraction ("michael_family", "Michael");
					familyInteractionDone = true;
				}
				diningRoomTriggered = true;	

				// check if player got near paula in second house scene to trigger phone dialog
				if(actualHouseScene == 2 && !paulaPhoneDialog1){
					guiController.toggleSubtl("paulaPhoneDialog1");
					paulaPhoneDialog1 = true;
				}
				break;
			case "Piano":
				if(pianoCount < 3){
					guiController.toggleInteractionHint (true);
				}
				break;
			case "Familyalbum": // check if familyalbum triggered
				switch(actualHouseScene){
				case 1:
					guiController.toggleInteractionHint (true);
					break;
				case 2:
					if(paulaPhoneDialog2 && paulaIntroductionDialog){
						guiController.toggleInteractionHint (true);
					}
					break;
				}
				break;
			case "Notepad": // check if noteblock triggered
				guiController.toggleInteractionHint (true);
				break;
			case "Conservatory": // check if jane entered wintergarden for first time
					initDialog("conservatory");
				break;
			case "Glasstable": // check if glasstable triggered
				if(!glasstableTriggered){
					guiController.toggleInteractionHint(true);
				}
				break;
			case "Childsroom": // check if childsroom entered for first time and if Michael already told about Emily
				if(!daughterFound){
					initDialog("daughter");
				} else{
					initDialog("childsroom");
					childsroomTriggered = true;
				}
				break;
			case "Guestroom": // check if guestroom entered
				initDialog("guestroom");
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
					initDialog("bedroom");
				break;
			case "Sidetable": // check if sidetable triggered
				guiController.toggleInteractionHint (true);
				break;
			case "Paula": // check if Paula is triggered and interactable
				if(guiController.checkForPanelContent ()){
					guiController.toggleInteractionHint(true);
				}else {
					guiController.toggleInteractionHint (false);
				}
				break;
			default: 
				guiController.toggleInteractionHint (false);
				break;
			}

		}else {
			guiController.toggleInteractionHint (false);
		}
	}

	// check for new interactions in interaction panel
	void checkInteractionPanel(){
		if (!guiController.manageDialogs().Equals("") && !dialogsPerformed.Contains(guiController.manageDialogs())) {
			initDialog(guiController.manageDialogs());
			guiController.toggleInteractionPanel(false, "");
		}
	}

	// initialize a dialog
	void initDialog(string dialogToInit){
		if (!dialogsPerformed.Contains(dialogToInit)){
			dialog = true;
			actualDialog = dialogToInit;
			guiController.toggleSubtl (dialogToInit+dialogCount);
		}
	}

	// toggle subtitles in actual dialog
	void toggleDialogs(){
		int dialogMaxNum = 0;

		if(!actualDialog.Equals("")){
			keyDialogSizeMap.TryGetValue(actualDialog, out dialogMaxNum);
			manageDialogSettings(dialogMaxNum, actualDialog+dialogCount);
		}
	}

	// manage if certain things happen after ending or during the dialog
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
					guiController.manageInteraction("michael_daughter", "Michael");
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
				guiController.manageInteraction("michael_friends", "Michael");
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
				StartCoroutine (onNextSceneStart ());
			}
			// dialog paula introduction needs to be added individually cause its too long (remove the old wrong entry first), add new interacion to panel
			else if (actualSubtl.Substring(0, actualSubtl.Length-2).Equals("paulaIntroduction")){
				dialogsPerformed.Remove("paulaIntroduction1");
				dialogsPerformed.Add ("paulaIntroduction");
				guiController.manageInteraction("paula_about2", "Paula");
			}
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("paulaIntroduction2_")){
				insertIntoInventory("paulasDaughter");
				paulaIntroductionDialog = true;
			}
			else if (actualSubtl.Substring(0, actualSubtl.Length-1).Equals("paulaPhoneCall")){
				paulaPhoneDialog2 = true;
			}
			// dialog mother needs to be added individually cause its too long (remove the old wrong entry first), add new interacion to panel
			else if (actualSubtl.Substring(0, actualSubtl.Length-2).Equals("mother")){
				dialogsPerformed.Remove("mother1");
				dialogsPerformed.Add ("mother");
				insertIntoInventory("crash");
			}
			switch(actualDialog){
			case "scare":
				guiController.manageInteraction("michael_mother", "Michael");
				guiController.manageInteraction("michael_father", "Michael");
				break;
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
					guiController.manageInteraction("michael_missingPicture", "Michael");
				}
				break;
			}
		}
		// check for second theory
		if(theory.GetComponent<Theory> ().theory2Found && theory2Registered == false){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory2");
			guiController.manageInteraction("michael_scar_2", "Michael");
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

	// trigger events that are based on sth. jane is able to see
	void checkSight(){
		// toggle scared dialog after familyalbum was closed for the first time here
		if (janeSurprised) {
			Ray lookRay = new Ray (playerCam.transform.position, playerCam.transform.forward);
			if (Physics.Raycast (lookRay, out hit, rayDist)) {
				if(hit.collider.tag.Equals("Michael")){
					initDialog("scare");
				}
			}
		}
	}

	// do stuff here that is needed when new scene starts
	IEnumerator onNextSceneStart(){
		actualHouseScene++;
		GetComponent<SceneFader> ().SwitchScene (3);
		yield return new WaitForSeconds (2);
		// fade scene in and put player to bed
		if (GetComponent<SceneFader> ().checkFade() && actualHouseScene > 1) {
			player.transform.position = bedPos;
			GetComponent<SceneFader> ().forceFadeIn();
		}
		switch (actualHouseScene) {
		case 2: // setup second house scene
			Destroy (glassTableImpression);
			GameObject.Find("DiningRoomTrigger1").SetActive(false);
			GameObject.Find("ConservatoryTrigger").SetActive(false);
			GameObject.Find("PianoTrigger").SetActive(false);
			GameObject.Find("ChildsroomTrigger").SetActive(false);
			GameObject.Find("WorkroomTrigger").SetActive(false);
			GameObject.Find("GuestroomTrigger").SetActive(false);
			GameObject.Find("BedroomTrigger").SetActive(false);
			GameObject.Find("FloorTrigger").SetActive(true);

			michael.transform.position = michaelScene2Pos;
			michael.SetActive(false);
			paula.SetActive(true);
			drawer.SetActive(false);
			isDizzy = true;
			guiController.toggleSubtl("dizzy1");
			secondSceneReady = true;
			guiController.manageInteraction("paula_about", "Paula");
			guiController.manageInteraction("paula_phoneCall", "Paula");
			break;
		default: break;
		}
	}

	// setup list with dialogs and their length
	void setupKeyDialogSizeMap(){
		keyDialogSizeMap.Add("welcome", 5);
		keyDialogSizeMap.Add("scar1_", 2);
		keyDialogSizeMap.Add("scar2_", 4);
		keyDialogSizeMap.Add("friends", 2);
		keyDialogSizeMap.Add("family", 4);
		keyDialogSizeMap.Add("daughter", 7);
		keyDialogSizeMap.Add("piano", 3);
		keyDialogSizeMap.Add("daughter2_", 7);
		keyDialogSizeMap.Add("picture", 4);
		keyDialogSizeMap.Add("familyAlbum1_", 2);
		keyDialogSizeMap.Add("conservatory", 3);
		keyDialogSizeMap.Add("glassTable", 5);
		keyDialogSizeMap.Add("childsroom", 3);
		keyDialogSizeMap.Add("guestroom", 4);
		keyDialogSizeMap.Add("bedroom", 2);
		keyDialogSizeMap.Add("adressBook", 2);
		keyDialogSizeMap.Add("scene1Ending", 3);
		keyDialogSizeMap.Add("paulaIntroduction", 9);
		keyDialogSizeMap.Add("paulaPhoneCall", 7);
		keyDialogSizeMap.Add("paulaIntroduction2_", 5);
		keyDialogSizeMap.Add("scare", 4);
		keyDialogSizeMap.Add("father", 5);
		keyDialogSizeMap.Add("mother",9);
	}
}
