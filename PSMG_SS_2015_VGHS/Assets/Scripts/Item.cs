using UnityEngine;
using System.Collections;

// Setup Item resources in this class
public class Item {

	public string itemName;
	public string itemDesc;

	public int itemId;

	public Sprite itemIcon;

	public GameObject itemModel;

	public Item(string name, string desc, int id){

		itemName = name;
		itemDesc = desc;
		itemId = id;
		itemIcon = Resources.Load<Sprite> ("" + name);
	}

	public Item(){
	}

}
