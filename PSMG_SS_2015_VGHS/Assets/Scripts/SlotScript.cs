using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Handle anything happening in a slot here
public class SlotScript : MonoBehaviour, IPointerDownHandler{

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
		Debug.Log (transform.name);
	}
}
