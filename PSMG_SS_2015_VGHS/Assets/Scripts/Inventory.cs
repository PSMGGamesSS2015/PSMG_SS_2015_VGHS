using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*INVENTORY CONTROLLER
 * initialize number, rows and coloumns of inventory slots
 */
public class Inventory : MonoBehaviour {

	public List <GameObject> Slots = new List<GameObject> (); 
	public List <Item> Items = new List<Item> (); 


	ItemDatabase database;

	public GameObject slots;

	public int slotNumX;
	public int slotNumY;
	public int slotDistX;
	public int slotDistY;
	public int x;
	public int y;
	public int xReset;


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
}
