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
	public GameObject michaelInteraction;

	public bool subtlShown;
	public bool interactionPanelShown;
	bool inventoryShown;
	bool menuShown;


	// Use this for initialization
	void Start (){
        if (Screen.fullScreen == false)
        {
            checkbox.isOn = true;
        }
		Cursor.visible = false;
		inventory.GetComponent<Inventory> ().setupInventory ();

		if (Application.loadedLevel == 2) {
			michaelInteraction.GetComponent<MichaelInteractions>().setupInteractionSlots();
		}
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
		if (subtlShown || inventoryShown || menuShown || interactionPanelShown) {
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
		for(int i = 0; i < michaelInteraction.GetComponent<MichaelInteractions>().iSlots.Count; i++){
			if(michaelInteraction.GetComponent<MichaelInteractions>().iSlots[i].activeSelf){
				return true;
			}
		}
		return false;
	}

	public void closeInteractionInPanel(string key){
		for(int i = 0; i < michaelInteraction.GetComponent<MichaelInteractions>().iSlots.Count; i++){
			michaelInteraction.GetComponent<MichaelInteractions>().iSlots[i].GetComponent<InteractionSlots>().forceInteractionSlotClose(key);
		}

	}

	// show/unshow interaction panel
	public void toggleInteractionPanel(bool active){
		if (michaelInteraction.activeSelf || active == false) {
			michaelInteraction.SetActive (false);
			interactionPanelShown = false;
			Cursor.visible = false;
		} else {
			michaelInteraction.SetActive(true);
			interactionPanelShown = true;
			Cursor.visible = true;
		}
	}

	// manage shown interaction in interaction panel
	public void manageInteraction (string key){
		michaelInteraction.GetComponent<MichaelInteractions> ().setupInteraction (key);
	}

	public string manageDialogs(){
		return michaelInteraction.GetComponent<MichaelInteractions> ().triggeredInteraction;
	}
	
	// this is needed once when house scene was loaded and the inventory need to be set to the status of bath ending
	public void forceThSetup(){
		inventory.GetComponent<Inventory> ().setupTheory (1);
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
}
