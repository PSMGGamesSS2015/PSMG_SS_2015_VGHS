using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* GUI CONTROLLER
 * Handles anything that is needed to be shown on the canvas.
 */ 
public class GUIController : MonoBehaviour {
	
	public GameObject subtitleObject;
	public GameObject interactionHintObject;
	public GameObject inventoryObject;
	public GameObject inventoryHint;
	public GameObject inventory;
	
	bool subtlIsShown;
	bool sinkIsActive;

	// Use this for initialization
	void Start (){
		inventory.GetComponent<Inventory> ().setupInventory ();
	}

	//put hint into inventory
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

	//show inventory
	public void toggleInventory(){
		if (inventory.activeSelf) {
			inventory.SetActive (false);
		} else {
			inventory.SetActive(true);
		}

	}

	//show hint for Inventory
	public void showInventoryHint(){
		inventoryHint.SetActive (true);
	}

	//show a hint for possible 'E' interactions
	public void showInteractionHint(){
		interactionHintObject.SetActive(true);
	}

	//hide interaction hint for 
	public void unshowInteractionHint(){
			interactionHintObject.SetActive(false);	
	}

	// show a subtitle on the GUI
	public void showSubtl(string key){
		subtitleObject.SetActive(true);
		subtitleObject.GetComponent<Subtitle>().setKeyWord(key);
		subtlIsShown = true;
	}

	//disable subtitle on GUI
	public void unshowSubtl(){
		subtitleObject.SetActive(false);
		subtlIsShown = false;
	}

	//method that makes it able to check if an subtitle is shown currently
	public bool isShowing(){
		return subtlIsShown;
	}






}
