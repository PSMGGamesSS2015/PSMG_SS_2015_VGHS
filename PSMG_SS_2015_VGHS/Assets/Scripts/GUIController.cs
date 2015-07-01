using UnityEngine;
using System.Collections;
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
    public GameObject player;
	
    
    
    bool subtlIsShown;
	bool sinkIsActive;
	bool inventoryIsShown;

	// Use this for initialization
	void Start (){
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
			inventoryIsShown = false;
            player.GetComponent<FirstPersonController>().enabled = true;
            Cursor.visible = false;
		} else {
			inventory.SetActive(true);
			inventoryIsShown = true;
            player.GetComponent<FirstPersonController>().enabled = false;
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
			subtlIsShown = true;
		} else {
			subtitleObject.SetActive(false);
			subtlIsShown = false;
		}
	}

	//method that makes it able to check if an subtitle is shown currently
	public bool isShowing(){
		if (subtlIsShown || inventoryIsShown) {
			return true;
		} else {
			return false;
		}
	}
}
