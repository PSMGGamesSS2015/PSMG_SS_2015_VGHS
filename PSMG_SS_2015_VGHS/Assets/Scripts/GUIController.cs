using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


/* GUI CONTROLLER
 * Handles anything that is needed to be shown on the canvas.
 */ 
public class GUIController : MonoBehaviour {

	public GameObject subtitleObject;
	public GameObject interactionHintObject;
	public GameObject inventoryObject;
	public GameObject inventoryHint;
	public GameObject inventory;
	public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject qualityMenu;
    public GameObject resolutionMenu;
    public Toggle checkbox;
	public GameObject interactionController;
	public GameObject adressBook;
	public GameObject familyalbum;
	public GameObject quizPanel;
    public GameObject soundOptions;
    public Slider slider;

	public bool subtlShown;
	public bool interactionPanelShown;
	bool inventoryShown;
	bool adressBookShown;
	bool familyalbumShown;
	public bool albumClosedFirstTime = false;
	bool menuShown;
	bool quizIsShown;
	public string quizAnswer = "";


	// Use this for initialization
	void Start (){
        if (Screen.fullScreen == false)
        {
            checkbox.isOn = true;
        }
		Cursor.visible = false;
		inventory.GetComponent<Inventory> ().setupInventory ();

		if (Application.loadedLevel == 2) {
			interactionController.GetComponent<InteractionController>().setupInteractionSlots();
		}

        slider.value = AudioListener.volume;
	}

	// put hint into inventory
	public void addHint(string key){
		switch (key) {
		case "dressHint": 
			inventory.GetComponent<Inventory> ().addItem (0);
			break;
		case "noteHint":
			inventory.GetComponent<Inventory> ().addItem (1);
			break;
		case "scar":
			inventory.GetComponent<Inventory>().addItem (3);
			break;
		case "family":
			inventory.GetComponent<Inventory>().addItem (4);
			break;
		case "daughter":
			inventory.GetComponent<Inventory>().addItem (5);
			break;
		case "picture":
			inventory.GetComponent<Inventory>().addItem (6);
			break;
		case "dianesDaughter":
			inventory.GetComponent<Inventory>().addItem (7);
			break;
		case "missingPicture":
			inventory.GetComponent<Inventory>().addItem (8);
			break;
		case "emilyWhereabout":
			inventory.GetComponent<Inventory>().addItem (9);
			break;
		case "pills":
			inventory.GetComponent<Inventory>().addItem (10);
			break;
		case "paulasDaughter":
			inventory.GetComponent<Inventory>().addItem (11);
			break;
		case "crash":
			inventory.GetComponent<Inventory>().addItem (12);
			break;
		case "dizzy":
			inventory.GetComponent<Inventory>().addItem (13);
			break;
		default: break;
		}
	}

	// show/hide inventory
	public void toggleInventory(){
		if (inventory.activeSelf) {
			inventory.SetActive (false);
			inventoryShown = false;
            Cursor.visible = false;
		} else {
			inventory.SetActive(true);
			inventoryShown = true;
            Cursor.visible = true;
		}
	}

	// show hint for Inventory
	public void toggleInventoryHint(){
		//set inactive first in case its still shown from another interaction
		inventoryHint.SetActive (false);
		inventoryHint.SetActive (true);
	}

	public string checkForNewHint(){
		return inventory.GetComponent<Inventory> ().newHint;
	}

	// toggle hint for possible 'E' interactions
	public void toggleInteractionHint(bool active){
		if (active) {
			interactionHintObject.SetActive(true);	
		} else {
			interactionHintObject.SetActive(false);
		}
	}

	// show a subtitle on the GUI
	public void toggleSubtl(string key){
		if (key != null) {
			subtitleObject.SetActive (true);
			subtitleObject.GetComponent<Subtitle> ().setKeyWord (key);
			subtlShown = true;
		} else {
			subtitleObject.SetActive(false);
			subtlShown = false;
		}
	}

	// method that makes it able to check if something is shown on UI currently
	public bool isShowing(){
		if (subtlShown || inventoryShown || menuShown || interactionPanelShown || adressBookShown || familyalbumShown || quizIsShown) {
			return true;
		} else {
			return false;
		}
	}

	// show/unshow pause menu
	public void togglePauseMenu(){
		if (pauseMenu.activeSelf) {
			menuShown = false;
			pauseMenu.SetActive (false);
			Cursor.visible = false;
		} else {
			menuShown = true;
			pauseMenu.SetActive(true);
			Cursor.visible = true;
		}
	}

	// check if panel contains interactions
	public bool checkForPanelContent(){
		for(int i = 0; i < interactionController.GetComponent<InteractionController>().iSlots.Count; i++){
			if(interactionController.GetComponent<InteractionController>().iSlots[i].activeSelf){
				return true;
			}
		}
		return false;
	}

	// force interaction close in interaction panel
	public void closeInteractionInPanel(string key){
		for(int i = 0; i < interactionController.GetComponent<InteractionController>().iSlots.Count; i++){
			interactionController.GetComponent<InteractionController>().iSlots[i].GetComponent<InteractionSlots>().forceInteractionSlotClose(key);
		}
	}

	// show/unshow interaction panel
	public void toggleInteractionPanel(bool active, string person){
		if (!active) {
			interactionController.SetActive (false);
			interactionPanelShown = false;
			Cursor.visible = false;
		} else {
			interactionController.SetActive(true);
			interactionController.GetComponent<InteractionController>().updateInteractionPanel(person);
			interactionPanelShown = true;
			Cursor.visible = true;
		}
	}

	// manage shown interaction in interaction panel
	public void manageInteraction (string key, string person){
		interactionController.GetComponent<InteractionController> ().setupInteraction (key, person);
	}

	// return the clicked interaction to choose right dialog
	public string manageDialogs(){
		return interactionController.GetComponent<InteractionController> ().triggeredInteraction;
	}
	
	// this is needed once when house scene was loaded and the inventory need to be set to the status of bath ending
	public void forceThSetup(){
		inventory.GetComponent<Inventory> ().setupTheory (1);
	}

	// show adress book when player interacts with book in sidetable
	public void toggleAdressBook(){
		if (adressBook.activeSelf == false) {
			adressBook.SetActive (true);
			adressBookShown = true;
			Cursor.visible = true;
		} else {
			adressBook.SetActive (false);
			adressBookShown = false;
			Cursor.visible = false;
		}
	}

	// show familyalbum when player interacts with book in bookshelf in living room 
	public void toggleFamilyalbum(){
		if (familyalbum.activeSelf == false) {
			familyalbum.SetActive (true);
			familyalbumShown = true;
			Cursor.visible = true;
		} else {
			if(!albumClosedFirstTime){
				albumClosedFirstTime = true;
			}
			familyalbum.SetActive (false);
			familyalbumShown = false;
			Cursor.visible = false;
		}
	}

	// show/unshow quiz 
	public void toggleQuizPanel(string context){
		if (!quizPanel.activeSelf) {
			quizPanel.SetActive(true);
			quizIsShown = true;
			Cursor.visible = true;
			switch (context){
			case "doctor":
				quizPanel.GetComponent<QuizController>().setupButtonText("Dr. Mellof", "Dr. Mehlhof", "Dr. Meloff", "Dr. Meltoff");
				break;
			}
		} else {
			quizPanel.SetActive(false);
			quizIsShown = false;
			Cursor.visible = false;
		}
	}

	public void checkQuizAnswer(string answer){
		quizAnswer = answer;
	}

    public void GoToOptionsMenu()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoFromOptionsBackToPauseMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void GoToQualityMenu()
    {
        optionsMenu.SetActive(false);
        qualityMenu.SetActive(true);
    }

    public void GoFromQualityToOptionsMenu()
    {
        qualityMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoToResolutionMenu()
    {
        optionsMenu.SetActive(false);
        resolutionMenu.SetActive(true);
    }

    public void GoFromResolutionToOptionsMenu()
    {
        resolutionMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoFromOptionsMenuToSoundMenu()
    {
        optionsMenu.SetActive(false);
        soundOptions.SetActive(true);
    }

    public void GoDromSoundOptionsToOptionsMenu()
    {
        optionsMenu.SetActive(true);
        soundOptions.SetActive(false);
    }

    public void SetVolume()
    {
        AudioListener.volume = slider.value;
    }
}
