using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.CrossPlatformInput;
/* HOUSE CONTROLLER
 * Everything that happens inside the house is managed here
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
	public GameObject box;
	public GameObject phoneWorkroom;

	public bool master = false;

	List<string> dialogsPerformed = new List<string>();
	Dictionary<string,int> keyDialogSizeMap = new Dictionary<string,int>();
	Vector3 bedPos = new Vector3(1.4f, 4.5f, 16.3f);
	Vector3 michaelScene2Pos = new Vector3(22f, -1f, 32f);
	Vector3 michaelScene3Pos = new Vector3(24f, 3f, 32f);
	Vector3 phoneScene4Pos = new Vector3 (4.5f, 6f, 7.5f);
	RaycastHit hit;
	string actualDialog = "";
	int pianoCount = 0;
	int dialogCount = 1;
	int actualHouseScene = 1;
	float rayDist = 40;
	bool scarHint = false;
	bool friends = false;
	bool family = false;
	bool daughterFound = false;
	bool diningRoomTriggered = false;
	bool childsroomTriggered = false;
	bool wintergardenTriggered = true;
	bool conservatoryTriggered = false;
	bool guestroomTriggered = false;
	bool workroomTriggered = false;
	bool bedroomTriggered = false;
	bool glasstableTriggered = false;
	bool dialog = false;
	bool theory2Registered = false;
	bool theory3Registered = false;
	bool theory4Registered = false;
	bool familyInteractionDone = false;
	bool missingPicture = false;
	bool missingPictureFound = false;
	bool emilyWhereabout = false;
	bool sidetableOpen = false;
	bool adressBookDialog = false;
	bool adressBookLost = false;
	bool adressBookFound = false;
	bool scene1EndingDialog = false;
	bool isDizzy = false;
	bool secondSceneReady = false;
	bool paulaPhoneDialog1 = false;
	bool paulaIntroductionDialog = false;
	bool paulaPhoneDialog2 = false;
	bool janeSurprised = false;
	bool lostBookFound = false;
	bool phoneTriggered = false;
	bool answerCorrect = false;
	bool answerBrotherCorrect = false;
	bool stopFollowing = false;
	bool pillsHidden = false;
	bool boxFound = false;
	bool pillAnswer = false;
	bool stuffFound = false;
	bool pillsFound = false;
	bool haldol = false;


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
		randomPlayerControl ();
		checkSight ();
		checkConditions ();
		checkQuizResult ();
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
					if(actualHouseScene == 4){
						initDialog ("amnesia");
					}
					if(actualHouseScene == 4 && pillsFound){
						guiController.togglePillQuiz();
					}else {
						guiController.toggleSubtl("bookshelf");
					}
					break;
				case "Sidetable": // managing interaction with sidetable and adressbook
					if (!sidetableOpen){
						drawer.GetComponent<DrawerAnimator>().openDrawer();
						sidetableOpen = true;
						initDialog("adressBook");

					} else{
						if(!adressBookLost){
							guiController.toggleAdressBook();
							adressBookFound = true;
						}
						if(actualHouseScene == 2 && adressBookLost){
							guiController.toggleSubtl("adressbookLost");
							lostBookFound = true;
							michael.GetComponent<FollowTarget>().target = player.transform;
							guiController.manageInteraction ("michael_adressBook", "Michael");
						}
						drawer.GetComponent<DrawerAnimator>().closeDrawer();
						sidetableOpen = false;
					}
					break;
				case "Phone": // interact with telephone
					if (dialogsPerformed.Contains ("pills") && !guiController.isShowing() && !dialogsPerformed.Contains("meloffCall")){
						initDialog("information");
						phoneTriggered = true;
					}
					if((actualHouseScene == 4 && !dialogsPerformed.Contains("paulaPhoneCall3_"))){
						initDialog("paulaPhoneCall3_");
					}
					if(theory4Registered && !dialogsPerformed.Contains("brotherCall")){
						initDialog("information3_");
					}else{
						guiController.toggleSubtl("phoneDefault");
					}
					break;
				case "Paula": // open interaction panel if interactions are available
					if(guiController.checkForPanelContent()){
						guiController.toggleInteractionPanel(true, "Paula");
					}
					break;
				case "Warderobe": // interactions with warderobe in bedroom
					if(!dialogsPerformed.Contains("freshWater") && dialogsPerformed.Contains("paulaPills")){
						guiController.toggleSubtl("hidePills");
						paula.GetComponent<FollowTarget>().target = player.transform;
						pillsHidden = true;
					}
					else if(dialogsPerformed.Contains("freshWater") && !haldol){
						guiController.togglePills();
						pillsFound = true;
					}
					else{
						guiController.toggleSubtl("warderobe");
					}
					break;
				case "Chest": // interacting with chest of drawers ins guestroom
					if(haldol){
						guiController.toggleSubtl("box");
						box.SetActive(true);
						boxFound = true;
					}
					break;
				case "Box": // interact with box
					guiController.toggleSubtl("box2");
					insertIntoInventory("personalStuff");
					box.SetActive(false);
					stuffFound = true;
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
                    DisableHighlighting("Michael");
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
                if (pianoCount >= 3)
                {
                    DisableHighlighting("piano");
                }
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
                if (glasstableTriggered)
                {
                    DisableHighlighting("table_glass_conservatory");
                }
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
				if(!bedroomTriggered && dialogsPerformed.Contains ("bedtime") && actualHouseScene == 2){
					guiController.toggleSubtl("bedroom3");
					bedroomTriggered = true;
				}
				if(dialogsPerformed.Contains("pills") && !dialogsPerformed.Contains("meloffCall") && !bedroomTriggered && actualHouseScene == 3){
					guiController.toggleSubtl("callDoc");
					bedroomTriggered = true;
				}
				if (actualHouseScene == 4){
					initDialog ("paulaPhoneCall2_");
				}
				break;
			case "Phone": // interactable in special cases
				guiController.toggleInteractionHint(true);
				break;
			case "Sidetable": // check if sidetable triggered
				guiController.toggleInteractionHint (true);
				break;
			case "Paula": // check if Paula is triggered and interactable
				if(guiController.checkForPanelContent ()){
					guiController.toggleInteractionHint(true);
				}else {
					guiController.toggleInteractionHint (false);
                    DisableHighlighting("Paula");
				}
				break;
			case "Warderobe": // show interactable when jane needs to hide the pills 
					guiController.toggleInteractionHint(true);
				break;
			case "Chest": // set interactible one time to find box 
				if(haldol && !boxFound){
					guiController.toggleInteractionHint(true);
				}
				break;
			case "Box": // collide with box
				if(!stuffFound){
					guiController.toggleInteractionHint(true);
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
		// make sure dialogs are performed only once (information dialog is special case)
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
			// add dialog to performed dialogs list if not already added
			if (!dialogsPerformed.Contains (actualSubtl.Substring (0, actualSubtl.Length - 1))) {
				dialogsPerformed.Add (actualSubtl.Substring (0, actualSubtl.Length - 1));
			}
			// add hint about scar during dialog
			if (actualSubtl.Substring (0, actualSubtl.Length - 3).Equals ("scar") && scarHint == false) {
				insertIntoInventory (actualSubtl.Substring (0, actualSubtl.Length - 3));
				scarHint = true;
			} 
			// checkup dialog about family, insert information about visiting the brother and activate interaction for children if not already done at the piano
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("family")) {
				family = true;
				insertIntoInventory (actualSubtl.Substring (0, actualSubtl.Length - 1));
				if (daughterFound == false) {
					guiController.manageInteraction ("michael_daughter", "Michael");
				}
			}
			// checkup dialog about friends
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("friends")) {
				friends = true;
			}
			// add hint about emily to inventory
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("daughter")) {
				insertIntoInventory (actualSubtl.Substring (0, actualSubtl.Length - 1));
				daughterFound = true;
			}
			// add hint about dianes daughter to inventory
			else if (actualSubtl.Substring (0, actualSubtl.Length - 3).Equals ("daughter")) {
				insertIntoInventory ("dianesDaughter");
				guiController.manageInteraction ("michael_friends", "Michael");
			}
			// add hint about the missing picture
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("picture")) {
				insertIntoInventory ("missingPicture");
				missingPictureFound = true;
			}
			// add hint about emilies whereabout
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("childsroom")) {
				insertIntoInventory ("emilyWhereabout");
				emilyWhereabout = true;
			}
			// register that dialog about the adress book is done
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("adressBook")) {
				adressBookDialog = true;
			}
			// insert hint about pills after taking them for the first time
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("scene1Ending")) {
				scene1EndingDialog = true;
				insertIntoInventory ("pills");
				//toggeling here next scene after this dialog makes sure, that nothing is destroyed while first scene still going on
				StartCoroutine (onNextSceneStart ());
			}
			// dialog paula introduction needs to be added individually cause its too long (remove the old wrong entry first), add new interacion to panel
			else if (actualSubtl.Substring (0, actualSubtl.Length - 2).Equals ("paulaIntroduction")) {
				dialogsPerformed.Remove ("paulaIntroduction1");
				dialogsPerformed.Add ("paulaIntroduction");
				guiController.manageInteraction ("paula_about2", "Paula");
			} else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("paulaIntroduction2_")) {
				insertIntoInventory ("paulasDaughter");
				paulaIntroductionDialog = true;
			} else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("paulaPhoneCall")) {
				paulaPhoneDialog2 = true;
			}
			// dialog mother needs to be added individually cause its too long (remove the old wrong entry first), add new hint to inventory
			else if (actualSubtl.Substring (0, actualSubtl.Length - 2).Equals ("mother")) {
				dialogsPerformed.Remove ("mother1");
				dialogsPerformed.Add ("mother");
			}
			// dialog lostAdressBook needs to be added individually cause its too long (remove the old wrong entry first)
			else if (actualSubtl.Substring (0, actualSubtl.Length - 2).Equals ("lostAdressBook")) {
				dialogsPerformed.Remove ("lostAdressBook1");
				dialogsPerformed.Add ("lostAdressBook");
			}
			// remove dialog to toggle it more than one time and open quiz panel
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("information")) {
				dialogsPerformed.Remove ("information");
				guiController.toggleQuizPanel ("doctor");
			}
			// remove dialog to toggle it more than one time
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("information2_")) {
				dialogsPerformed.Remove ("information2_");
			}
			// dialog meloffCall needs to be added individually cause its too long (remove the old wrong entry first)
			else if (actualSubtl.Substring (0, actualSubtl.Length - 2).Equals ("meloffCall")) {
				dialogsPerformed.Remove ("meloffCall1");
				dialogsPerformed.Add ("meloffCall");
			}
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("amnesia")) {
				insertIntoInventory("amnesia");
			}
			// remove dialog to toggle it more than one time and open quiz panel
			else if (actualSubtl.Substring (0, actualSubtl.Length - 1).Equals ("information3_")) {
				dialogsPerformed.Remove ("information3_");
				guiController.toggleQuizPanel ("brother");
			}
			// dialog brotherCall needs to be added individually cause its too long (remove the old wrong entry first)
			else if (actualSubtl.Substring (0, actualSubtl.Length - 2).Equals ("brotherCall")) {
				dialogsPerformed.Remove ("brotherCall1");
				dialogsPerformed.Add ("brotherCall");
			}
			// dialog haldol2_ needs to be added individually cause its too long (remove the old wrong entry first)
			else if (actualSubtl.Substring (0, actualSubtl.Length - 2).Equals ("haldol2_")) {
				dialogsPerformed.Remove ("haldol2_1");
				dialogsPerformed.Add ("haldol2_");
				insertIntoInventory ("crash");
			}
			switch (actualDialog) {
			case "scare":
				guiController.manageInteraction ("michael_mother", "Michael");
				guiController.manageInteraction ("michael_father", "Michael");
				break;
			}

			// reset dialog data for new dialog
			dialogCount = 1;
			actualDialog = "";
			dialog = false;
		} 
		// insert hint about picture during dialog (this is a special case where the hint is added during dialog)
		else if (actualSubtl.Equals ("daughter2_2")) {
			insertIntoInventory ("picture");
			guiController.toggleSubtl (actualSubtl);
		} 
		// insert hint about nightmares of paulas daughter
		else if (actualSubtl.Equals ("paulaPhoneCall3_4")) {
			insertIntoInventory ("nightmares");
			guiController.toggleSubtl (actualSubtl);
		}

		else if (actualSubtl.Equals ("paulaPills6")) {
			stopFollowing = true;
			paula.GetComponent<FollowTarget>().target = GameObject.Find("notepad").transform;
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
		if (!guiController.checkForNewHint ().Equals ("")) {
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
		if(theory.GetComponent<Theory> ().theory2Found && !theory2Registered){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory2");
			guiController.manageInteraction("michael_scar_2", "Michael");
			theory2Registered = true;
		}
		// check for third theory
		if(theory.GetComponent<Theory> ().theory3Found && !theory3Registered){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory3");
			guiController.manageInteraction("michael_dizzy", "Michael");
			theory3Registered = true;
		}
		// check for fourth theory
		if(theory.GetComponent<Theory> ().theory4Found && !theory4Registered){
			guiController.toggleInventory();
			guiController.toggleSubtl("theory4");
			theory4Registered = true;
		}
	}

	// check conditions to trigger a certain event
	void checkConditions(){
		// Ending conditions for first Scene in house
		if((glasstableTriggered && family && adressBookFound && !guiController.isShowing() && !scene1EndingDialog && emilyWhereabout && actualHouseScene == 1) || (master && actualHouseScene == 1)){
			initDialog("scene1Ending");
		}
		// conditions to remove adressbook during second house scene
		if(dialogsPerformed.Contains("mother") && dialogsPerformed.Contains("father") && dialogsPerformed.Contains ("scar2_") && dialogsPerformed.Contains ("friends") && actualHouseScene == 2 && !guiController.isShowing()){
			adressBook.SetActive(false);
			adressBookLost = true;
			initDialog("bedtime");
			if(dialogsPerformed.Contains("bedtime") && !lostBookFound){
				michael.GetComponent<FollowTarget>().target = paula.transform;
			}
		}
		// condition to end second house scene
		if ((dialogsPerformed.Contains ("lostAdressBook") && actualHouseScene == 2) || (master && actualHouseScene == 2)) {
			StartCoroutine (onNextSceneStart ());
		}
		// make michael walk into the kitchen in third house scene
		if (dialogsPerformed.Contains ("pills") && !answerCorrect) {
			michael.GetComponent<FollowTarget>().target = GameObject.Find("notepad").transform;
			michael.GetComponent<NavMeshAgent>().speed = 3.5f;
		}
		// start next Scene
		if (dialogsPerformed.Contains ("scene3Ending")&& actualHouseScene == 3) {
			StartCoroutine(onNextSceneStart());
		}
		// make Paula search for Jane after she found out about the nightmare and her amnesia
		if (dialogsPerformed.Contains ("paulaPhoneCall3_") && dialogsPerformed.Contains ("amnesia") && !stopFollowing) {
			paula.GetComponent<FollowTarget>().target = player.transform;
			paula.GetComponent<NavMeshAgent>().speed = 3.5f;
		}
		// make paula walk back into the kitchen after bringing another glass of water
		if (dialogsPerformed.Contains ("freshWater")) {
			paula.GetComponent<FollowTarget>().target = GameObject.Find("notepad").transform;
		}
		if (actualHouseScene == 3 && master) {
			StartCoroutine(onNextSceneStart());
		}
		master = false;
			
	}

	// generate heavier player controlling while jane is dizzy
	void randomPlayerControl(){
		if (isDizzy) {
			playerCam.GetComponent<BlurOptimized>().enabled = true;
			switch(actualHouseScene){
			case 2:
				player.GetComponent<FirstPersonController> ().randomControl ("Horizontal", "Vertical", 20);
				break;
			case 3:
				player.GetComponent<FirstPersonController> ().randomControl ("Horizontal", "Vertical", 40);
				playerCam.GetComponent<BlurOptimized>().blurIterations = 2;
				break;
			}
		}else{
			player.GetComponent<FirstPersonController> ().randomControl ("Horizontal", "Vertical", 2);
			playerCam.GetComponent<BlurOptimized>().enabled = false;
		}
	}

	// trigger events that are based on sth. jane is able to see
	void checkSight(){
		// toggle scared dialog after familyalbum was closed for the first time here
		Ray lookRay = new Ray (playerCam.transform.position, playerCam.transform.forward);
		if (janeSurprised) {
			if (Physics.Raycast (lookRay, out hit, rayDist)) {
				if(hit.collider.tag.Equals("Michael")){
					initDialog("scare");
				}
			}
		}
		if (answerCorrect) {
			if (Physics.Raycast (lookRay, out hit, rayDist)) {
				if(hit.collider.tag.Equals("Michael")){
					if(dialogsPerformed.Contains("meloffCall")){
						initDialog("scene3Ending");
					}
					else{
						initDialog ("janeCought");
					}

				}
			}
		}
		if(dialogsPerformed.Contains ("paulaPhoneCall3_") && dialogsPerformed.Contains ("amnesia")){
			if (Physics.Raycast (lookRay, out hit, rayDist)) {
				if(hit.collider.tag.Equals("Paula")){
					initDialog ("paulaPills");
				}
			}
		}
		if (pillsHidden && !dialogsPerformed.Contains ("freshWater")) {
			if (Physics.Raycast (lookRay, out hit, rayDist)) {
				if(hit.collider.tag.Equals("Paula")){
					initDialog ("freshWater");
				}
			}
		}		
	}
	// check quiz results
	void checkQuizResult(){
		// check if phone quiz while trying to call the doctor turned out correctly
		if (!guiController.quizAnswer.Equals ("") && !answerCorrect) {
			if(guiController.quizAnswer.Equals ("Dr. Meloff")){
				guiController.toggleQuizPanel("");
				initDialog("meloffCall");
				michael.GetComponent<FollowTarget>().target = player.transform;
				answerCorrect = true;
			}
			if(guiController.quizAnswer.Equals ("San Diego")){
				guiController.toggleQuizPanel("");
				initDialog ("brotherCall");
				michael.SetActive(true);
				guiController.manageInteraction("michael_visit", "Michael");
				answerCorrect = true;
				Debug.Log ("huhu");
			}
			else{
				initDialog("information2_");
				guiController.quizAnswer = "";
				guiController.toggleQuizPanel("");
			}
		}
		// check if pill quiz while trying to identify the pills turned out correctly
		if (!guiController.pillQuizAnswer.Equals ("")) {
			if(guiController.pillQuizAnswer.Equals("Haldol") && !pillAnswer){
				guiController.togglePillQuiz();
				guiController.pillQuizAnswer = "";
				pillAnswer = true;
				guiController.toggleSubtl("haldol");
				guiController.manageInteraction("michael_haldol", "Michael");
				haldol = true;
			}
			else{
				guiController.togglePillQuiz();
				guiController.pillQuizAnswer = "";
				guiController.toggleSubtl("wrongPills");
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
			GameObject.Find("FloorTrigger").SetActive(true);

			michael.transform.position = michaelScene2Pos;
			michael.SetActive(false);
			paula.SetActive(true);
			isDizzy = true;
			guiController.toggleSubtl("dizzy1");
			secondSceneReady = true;
			guiController.manageInteraction("paula_about", "Paula");
			guiController.manageInteraction("paula_phoneCall", "Paula");
			break;
		case 3:
			GameObject.Find("SidetableTrigger").SetActive(false);
			GameObject.Find("Familienalbum").SetActive(false);

			michael.GetComponent<NavMeshAgent>().speed = 0;
			michael.transform.position = michaelScene3Pos;
			paula.SetActive(false);
			bedroomTriggered = false;
			guiController.toggleSubtl("dizzy2");
			insertIntoInventory("dizzy");
			break;
		case 4: 
			michael.SetActive(false);
			paula.SetActive(true);
			guiController.toggleSubtl("dizzy3");
			GameObject.FindGameObjectWithTag("PhoneBedroom").SetActive(false);
			phoneWorkroom.SetActive(true);
			answerCorrect = false;
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
		keyDialogSizeMap.Add("bedtime", 3);
		keyDialogSizeMap.Add("lostAdressBook", 11);
		keyDialogSizeMap.Add("pills", 8);
		keyDialogSizeMap.Add("information", 3);
		keyDialogSizeMap.Add("information2_", 2);
		keyDialogSizeMap.Add("meloffCall", 12);
		keyDialogSizeMap.Add("scene3Ending", 5);
		keyDialogSizeMap.Add ("paulaPhoneCall2_", 2);
		keyDialogSizeMap.Add ("paulaPhoneCall3_", 5);
		keyDialogSizeMap.Add ("amnesia", 4);
		keyDialogSizeMap.Add ("paulaPills", 6);
		keyDialogSizeMap.Add ("freshWater", 4);
		keyDialogSizeMap.Add("information3_", 3);
		keyDialogSizeMap.Add("brotherCall", 11);
		keyDialogSizeMap.Add("janeCought", 5);
		keyDialogSizeMap.Add ("haldol2_", 11);
		keyDialogSizeMap.Add ("visit", 2);
	}

    void DisableHighlighting(string name)
    {
        GameObject myObject = GameObject.Find(name);
        myObject.GetComponent<HighlightObject>().enabled = false;
        myObject.GetComponent<Renderer>().material = myObject.GetComponent<HighlightObject>().GetObjectStartMaterial();
    }
}
