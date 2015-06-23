using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*INVENTORY CONTROLLER
 * initialize number, rows and coloumns of inventory slots
 */
public class Inventory : MonoBehaviour {

	public List <GameObject> Slots = new List<GameObject> (); 
	public List <Item> Items = new List<Item> (); 

	public GameObject canvas;

	ItemDatabase database;

	public GameObject slots;

	public int slotNumX;
	public int slotNumY;
	public int slotDistX;
	public int slotDistY;
	public int x;
	public int y;
	public int xReset;

	int actualSlotNum;

	public bool draggingItem = false;
	public Item draggedItem;
	public GameObject draggedItemObject;

	// Update is called once per frame
	void Update(){
		if (draggingItem) {
			Vector3 pos = (Input.mousePosition - canvas.GetComponent<RectTransform>().localPosition);
			draggedItemObject.GetComponent<RectTransform>().localPosition = new Vector3(pos.x +35, pos.y - 35, pos.z);
		}
	}


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

	// make sure that an item is added to an empty slot
	void addToEmptySlot(Item item){

		for(int i = 0; i < Items.Count; i++){
			if(Items[i].itemName == null){
				Items[i] = item;
				break;
			}
		}
	}

	public void dragItem(Item item, int actualSlot){
		draggedItemObject.SetActive (true);
		draggedItemObject.GetComponent<Image> ().sprite = item.itemIcon;
		draggingItem = true;
		draggedItem = item;
		actualSlotNum = actualSlot;
		Debug.Log (actualSlot);
	}

	public void dropItem(){
		draggingItem = false;
		draggedItemObject.SetActive (false);
	}

	public void reverseDrag(){
		draggingItem = false;
		draggedItemObject.SetActive (false);
		addItemToSlot ();
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
