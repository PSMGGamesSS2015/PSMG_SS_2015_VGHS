using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Handle anything happening in a slot here
public class SlotScript : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler{

	public Item item;
	public int slotNumber;

	Image itemImage;
	Inventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
		itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		//check if there is an item in the slot and display icon
		if (slotNumber < inventory.Items.Count) {
			if (inventory.Items [slotNumber].itemName != null) {
				itemImage.enabled = true;
				itemImage.sprite = inventory.Items [slotNumber].itemIcon;
			} else {
				itemImage.enabled = false;
			}
		}
	}

	// Do sth. when item dragged
	public void OnDrag(PointerEventData data){
		if (inventory.Items [slotNumber].itemName != null) {
			inventory.dragItem(inventory.Items [slotNumber], slotNumber);
			inventory.Items[slotNumber] = new Item();
		}
	}

	// Do sth. when clicked on slot
	public void OnPointerDown(PointerEventData data){
		if (inventory.Items [slotNumber].itemName == null && inventory.draggingItem) {
			inventory.Items [slotNumber] = inventory.draggedItem;
			inventory.dropItem (false, "");
		}
		// trigger a theory combination
		else if (inventory.Items [slotNumber] != null && inventory.draggingItem) {
			inventory.dropItem (true, inventory.Items[slotNumber].itemName);
		}
	}

	public void OnPointerEnter(PointerEventData data){
		if (inventory.Items [slotNumber].itemDesc != null) {
			inventory.setupItemDescription (inventory.Items [slotNumber].itemDesc);
		}

	}

	public void OnPointerExit(PointerEventData data){
		inventory.setupItemDescription ("");
	}
}
