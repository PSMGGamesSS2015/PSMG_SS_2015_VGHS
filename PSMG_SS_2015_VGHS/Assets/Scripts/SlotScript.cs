using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Handle anything happening in a slot here
public class SlotScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler{

	public Item item;
	Image itemImage;
	public int slotNumber;

	Inventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
		itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	 	if (inventory.Items[slotNumber].itemName != null) {
			itemImage.enabled = true;
			itemImage.sprite = inventory.Items[slotNumber].itemIcon;
		} 
		else {
			itemImage.enabled = false;
		}
	}

	// Do sth. when slot is clicked
	public void OnPointerDown(PointerEventData data){




	}

	// Do sth. when item dragged
	public void OnDrag(PointerEventData data){
		if (inventory.Items [slotNumber].itemName != null) {
			inventory.dragItem(inventory.Items [slotNumber], slotNumber);
			inventory.Items[slotNumber] = new Item();
		}
	}


	// Do sth. when item dropped
	public void OnDrop(PointerEventData data){
		// Just drop item to empty slot
		if (inventory.Items [slotNumber].itemName == null && inventory.draggingItem) {
			inventory.Items [slotNumber] = inventory.draggedItem;
			inventory.dropItem ();
		}

		// trigger a theory combination
		else if (inventory.Items [slotNumber] != null && inventory.draggingItem) {
			Debug.Log ("Oh mein Gott, könnte ich Pat getötet haben?!");
			inventory.reverseDrag ();
		}

	}


}
