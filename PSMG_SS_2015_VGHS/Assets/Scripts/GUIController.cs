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

	bool subtlShown;
	bool inventoryShown;
	bool menuShown;

	// Use this for initialization
	void Start (){
		Cursor.visible = false;
		inventory.GetComponent<Inventory> ().setupInventory ();
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
		if (subtlShown || inventoryShown || menuShown) {
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
}
