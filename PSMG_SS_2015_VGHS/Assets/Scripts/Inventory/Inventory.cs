﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*INVENTORY CONTROLLER
 * initialize number, rows and coloumns of inventory slots
 */
public class Inventory : MonoBehaviour {

	public List <GameObject> Slots = new List<GameObject> (); 
	public List <GameObject> TheorySlots = new List<GameObject> (); 
	public List <Item> Items = new List<Item> (); 

	public GameObject canvas;
	public GameObject theoryInventory;
	public GameObject descText;

	ItemDatabase database;

	public GameObject slots;
	public GameObject theorySlots;

	public int slotNumX;
	public int slotNumY;
	public int slotDistX;
	public int slotDistY;
	public int x;
	public int y;

	public int thX;
	public int thY;

	public int xReset;

	int actualSlotNum;

	public string newHint = "";
	public bool draggingItem = false;
	public Item draggedItem;
	public GameObject draggedItemObject;

	void Start(){
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update(){
		// set dragged item to mouse
		if (draggingItem) {
			Vector3 pos = (Input.mousePosition - canvas.GetComponent<RectTransform>().localPosition);
			draggedItemObject.GetComponent<RectTransform>().localPosition = new Vector3(pos.x +35, pos.y - 35, pos.z);
		}
	}

	//setup the slots for hints to be saved
	public void setupInventory(){

		int slotAmount = 0;

		database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

		for(int i = 0; i < slotNumY; i++){
			for(int k = 0; k < slotNumX; k++){
				GameObject slot = (GameObject) Instantiate(slots);
				slot.GetComponent<SlotScript>().slotNumber = slotAmount;
				slot.transform.parent = this.gameObject.transform;
				slot.GetComponent<RectTransform>().localPosition = new Vector3 (x, y, 0);
				slot.name = "Slot"+i+"."+k;
				Slots.Add(slot);
				Items.Add(new Item());
				x += slotDistX;
				if(k == slotNumX-1){
					x = xReset;
					y -= slotDistY;
				}
				slotAmount++;
			}
		}
	}

	// put item into inventory by searching database with id
	public void addItem(int id){

		for(int i = 0; i < database.items.Count; i++){
			if(database.items[i].itemId == id){
				Item item = database.items[i];
				addToEmptySlot(item);
				break;
			}
		}
	}

	public void removeItem(int id){
		Debug.Log ("yep");
		for (int i = 0; i < database.items.Count; i++) {
			if (database.items [i].itemId == id) {
				Item item = database.items [i];
				removeItemFromSlot (item);
				break;
			}
		}
	}

	// make sure that an item is added to an empty slot
	void addToEmptySlot(Item item){
		if (!Items.Contains (item)) {
			for (int i = 0; i < Items.Count; i++) {
				if (Items [i].itemName == null) {
					Items [i] = item;
					break;
				}
			}
		}
	}

	void removeItemFromSlot (Item item){
		if (Items.Contains (item)) {
			for (int i = 0; i < Items.Count; i++) {
				if (Items [i].itemName.Equals(item.itemName)) {
					Items.Remove(item);
					break;
				}
			}
		}
	}

	// register the dragged item
	public void dragItem(Item item, int actualSlot){
		draggedItemObject.SetActive (true);
		draggedItemObject.GetComponent<Image> ().sprite = item.itemIcon;
		draggingItem = true;
		draggedItem = item;
		actualSlotNum = actualSlot;
	}

	// unset image to mouse, recieve if possible second Item and check for new Theory
	public void dropItem(bool combined, string secondItem){

		draggingItem = false;
		draggedItemObject.SetActive (false);
		// do sth. when hints were combined
		if (combined) {
			addItemToSlot ();
			setupTheory(theoryInventory.GetComponent<Theory>().checkCombination(draggedItem.itemName, secondItem));
		}
	}

	// put new theory to theory inventory
	public void setupTheory(int theoryNum){
		// check if theory is actually a hint
		if (theoryNum == 8) {
			newHint = "missingPicture";
		}
		//check if this theory already exists
		else if(TheorySlots.Exists (x => x.name.Equals("theory"+theoryNum))== false && theoryNum != 0){
			GameObject slot = (GameObject)Instantiate (theorySlots);
			slot.transform.parent = theoryInventory.transform;
			slot.GetComponent<RectTransform> ().localPosition = new Vector3 (thX, thY, 0);
			slot.name = "theory" + theoryNum;
			TheorySlots.Add (slot);
			thY -= slotDistY;
		}
	}

	public void setupItemDescription(string description){
		descText.GetComponent<Text> ().text = description;
	}

	// this method is needed when a drag ends and item needs to be put back
	void addItemToSlot(){
		for(int i = 0; i < Items.Count; i++){
			if(i == actualSlotNum){
				Items[i] = draggedItem;
				break;
			}
		}
	}
}
